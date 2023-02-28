using AutoMapper;
using DroneManager.Core.Exceptions;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Enums;
using DroneManager.Server.Controllers.V1;
using DroneManager.Test.Setup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DroneManager.Test.IntegrationTests
{
    public class MedicineControllerTest
    {
        private SetupServices _setupServices;

        [SetUp]
        public void Setup()
        {
            _setupServices = new SetupServices();
        }

        [TearDown]
        public async Task TearDown()
        {
            await ((IAsyncDisposable)_setupServices).DisposeAsync();
        }

        [Test]
        public async Task CreateMedicineOk()
        {
            var provider = _setupServices.Provider();
            var mapper = provider.GetRequiredService<IMapper>();
            var mediator = provider.GetRequiredService<IMediator>();
            var setupFile = new SetupIFormFile();

            var dto = new MedicineFileDTO
            {
                Name = "Name",
                Code = "CODE",
                Weight = 10,
                File = setupFile.GetFile()
            };

            var controller = new MedicineController(mediator, mapper);
            var response = await controller.Post(dto, default);

            if (response is OkObjectResult okObject)
                Assert.That(okObject.Value, Is.InstanceOf<Response<MedicineDTO>>());
            else
                Assert.Fail();
        }

        [Test]
        public async Task CreateMedicineValidationError()
        {
            var provider = _setupServices.Provider();
            var mapper = provider.GetRequiredService<IMapper>();
            var mediator = provider.GetRequiredService<IMediator>();
            var setupFile = new SetupIFormFile();

            var dto = new MedicineFileDTO
            {
                Name = "Name",
                Code = "CODE",
                Weight = 10,
                File = setupFile.GetFile()
            };

            var controller = new MedicineController(mediator, mapper);
            await controller.Post(dto, default);

            //
            // valid code
            dto.Code = "CODE_I";


            // Name: null
            dto.Name = null;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // Name: empty
            dto.Name = "";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // Name: allowed only letters, numbers, '-' and '_'
            dto.Name = "Name ";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            //
            // Add valid name
            dto.Name = "Name_II";


            // Weight: less than to 0
            dto.Weight = -1;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            //
            // Add valid weight
            dto.Weight = 100;


            // Code: null
            dto.Code = null;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // Code: empty
            dto.Name = "";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // Code: allowed only capital letters, numbers and '_'
            dto.Name = "Name ";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // Code: a medicine already exists with the same Code
            dto.Code = "CODE";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));
        }


        //
        // Repeat the process for the rest of the controller methods
        //

    }
}