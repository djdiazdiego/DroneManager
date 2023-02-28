using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Models;
using DroneManager.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DroneManager.FileStorage;

public class FileStorageService : IFileStorageService
{
    private readonly string _path;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<StorageFileData> _storageFileDataRepository;
    private readonly ISqlGuidGenerator _sqlGuidGenerator;


    public FileStorageService(
        IWebHostEnvironment hostingEnvironment,
        IRepository<StorageFileData> storageFileDataRepository,
        IUnitOfWork unitOfWork,
        ISqlGuidGenerator sqlGuidGenerator)
    {
        _path = Path.Combine($"{hostingEnvironment.WebRootPath}", "data", "images");
        _storageFileDataRepository = storageFileDataRepository;
        _unitOfWork = unitOfWork;
        _sqlGuidGenerator = sqlGuidGenerator;
    }

    public async Task<StorageFileData> UploadFile(IFormFile file, CancellationToken cancellationToken = default)
    {
        var key = _sqlGuidGenerator.NewGuid().ToString();
        var extension = Path.GetExtension(file.FileName).Remove(0, 1);
        var size = file.Length;
        var isImage = IsImage(file);

        StorageFileData storageFileData;

        using (var fileStream = file.OpenReadStream())
        {
            storageFileData = await UploadFile(fileStream, $"{key}.{extension}", cancellationToken);
        }

        try
        {
            storageFileData.FileName = Path.GetFileNameWithoutExtension(file.FileName);
            storageFileData.Extension = extension;
            storageFileData.Size = size;
            storageFileData.IsImage = isImage;

            _storageFileDataRepository.Add(storageFileData);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return storageFileData;
        }
        catch (Exception)
        {
            await DeleteFile($"{key}.{extension}", cancellationToken);

            throw;
        }
    }

    public Task<bool> DeleteFile(string key, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            File.Delete(GetFilePath(key));
            return true;
        });
    }

    public Task<byte[]?> DownloadFile(string key, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var fileInfo = new FileInfo(GetFilePath(key));
            if (fileInfo != null && !fileInfo.Exists)
            {
                return File.ReadAllBytes(fileInfo.FullName);
            }

            return null;
        }, cancellationToken);
    }

    public Task<StorageFileData?> GetFile(string key, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            var fileInfo = new FileInfo(GetFilePath(key));
            return fileInfo == null || !fileInfo.Exists
                ? null
                : new StorageFileData
                {
                    Key = key,
                    IsImage = true,
                    Size = fileInfo.Length,
                    Url = $"data/images/{key}"
                };
        }, cancellationToken);
    }



    private async Task<StorageFileData> UploadFile(Stream stream, string key, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

        using (var fileStream = File.Create(GetFilePath(key)))
        {
            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(fileStream, cancellationToken);
        }

        return new StorageFileData
        {
            Key = key,
            Url = $"data/images/{key}",
        };
    }


    private string GetFilePath(string key) => Path.Combine(_path, key);

    private static bool IsImage(IFormFile file) => file.ContentType.Contains("image");
}
