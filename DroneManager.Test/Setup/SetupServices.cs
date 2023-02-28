using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Behaviours;
using DroneManager.Core.Data.Helpers;
using DroneManager.Core.Models;
using DroneManager.Core.Services;
using DroneManager.FileStorage;
using DroneManager.Infrastructure;
using DroneManager.Infrastructure.Contexts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;

namespace DroneManager.Test.Setup
{
    public class SetupServices : IAsyncDisposable
    {
        private const string ConnectionString = "Data Source=InMemoryDb;Mode=Memory;Cache=Shared";

        private readonly SqliteConnection _sqliteConnection;
        private readonly IServiceCollection _services;
        private readonly ServiceProvider _provider;

        public SetupServices()
        {
            _sqliteConnection = new SqliteConnection(ConnectionString);
            _sqliteConnection.Open();

            _services = new ServiceCollection();

            _services.AddMemoryCache();

            _services.AddDbContext<DbContextWrite>(options => options.UseSqlite(_sqliteConnection), ServiceLifetime.Scoped);
            _services.AddDbContext<DbContextRead>(options => options.UseSqlite(_sqliteConnection), ServiceLifetime.Scoped);

            _services.AddRepositories();

            _services.AddValidatorsFromAssembly(LoadApplicationAssemblies());

            _services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(LoadApplicationAssemblies());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            _services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<DbContextWrite>());
            _services.AddAutoMapper(LoadMapperAssemblies());

            _services.AddSingleton<ISqlGuidGenerator, SequentialGuidGeneratorService>();

            _services.AddTransient(provider =>
            {
                var repository = provider.GetRequiredService<IRepository<StorageFileData>>();
                var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
                var sqlGuidGenerator = provider.GetRequiredService<ISqlGuidGenerator>();

                var storageService = new Mock<IFileStorageService>();

                storageService.Setup(m => m.UploadFile(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
                    .Returns<IFormFile, CancellationToken>(async (file, ct) =>
                    {
                        var key = sqlGuidGenerator.NewGuid().ToString();
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName).Remove(0, 1); ;
                        var size = file.Length;
                        var isImage = file.ContentType.Contains("image");

                        var storageFileData = new StorageFileData()
                        {
                            Key = $"{key}.{extension}",
                            Url = $"data/image/{key}",
                            FileName = fileName,
                            Extension = extension,
                            Size = size,
                            IsImage = isImage
                        };

                        repository.Add(storageFileData);
                        await unitOfWork.SaveChangesAsync(ct);

                        return storageFileData;
                    });

                storageService.Setup(m => m.DeleteFile(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(true));

                return storageService.Object;
            });

            _provider = _services.BuildServiceProvider();

            Task.Run(LoadSeedsAsync).Wait();
        }

        public IServiceProvider Provider() => _provider;

        private async Task LoadSeedsAsync()
        {
            var dbContextWrite = _provider.GetRequiredService<DbContextWrite>();
            await dbContextWrite.Database.EnsureCreatedAsync(default);

            await ApplyChangesHelper.ApplyApplicationSeedsAsync(_provider, default);
        }

        public async ValueTask DisposeAsync()
        {
            await _provider.DisposeAsync();
            _services.Clear();

            await _sqliteConnection.CloseAsync();
            await _sqliteConnection.DisposeAsync();
        }

        private static Assembly LoadApplicationAssemblies() => Assembly.Load("DroneManager.Application");

        private static Assembly[] LoadMapperAssemblies()
        {
            var core = Assembly.Load("DroneManager.Core");
            var domain = Assembly.Load("DroneManager.Domain");
            var application = Assembly.Load("DroneManager.Application");

            return new Assembly[] { core, domain, application };
        }
    }
}
