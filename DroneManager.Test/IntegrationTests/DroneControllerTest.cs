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
    public class DroneControllerTest
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
        public async Task CreateDroneOk()
        {
            var provider = _setupServices.Provider();
            var mapper = provider.GetRequiredService<IMapper>();
            var mediator = provider.GetRequiredService<IMediator>();

            var dto = new DroneDTO
            {
                SerialNumber = "ValidSerial",
                BatteryCapacity = 95,
                Weight = 300,
                ModelId = DroneModelValues.Lightweigh,
                StatusId = DroneStatusValues.IDLE
            };

            var controller = new DroneController(mediator, mapper);
            var response = await controller.Post(dto, default);

            if (response is OkObjectResult okObject)
                Assert.That(okObject.Value, Is.InstanceOf<Response<DroneDTO>>());
            else
                Assert.Fail();
        }

        [Test]
        public async Task CreateDroneValidationError()
        {
            var provider = _setupServices.Provider();
            var mapper = provider.GetRequiredService<IMapper>();
            var mediator = provider.GetRequiredService<IMediator>();

            var controller = new DroneController(mediator, mapper);

            var dto = new DroneDTO
            {
                SerialNumber = "ValidSerial",
                BatteryCapacity = 95,
                Weight = 300,
                ModelId = DroneModelValues.Lightweigh,
                StatusId = DroneStatusValues.IDLE
            };

            await controller.Post(dto, default);

            // SerialNumber: null
            dto.SerialNumber = null;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // SerialNumber: empty
            dto.SerialNumber = "";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // SerialNumber: a drone already exists with the same serial number
            dto.SerialNumber = "ValidSerial";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // SerialNumber: maximum length exceeded
            dto.SerialNumber = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            
            // Add valid serial number
            dto.SerialNumber = "ValidSerial_II";


            // Weight: less than to 0
            dto.Weight = -1;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // Weight: greater than to 500
            dto.Weight = 501;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));


            // Add valid weight
            dto.Weight = 100;


            // BatteryCapacity: less than to 0
            dto.BatteryCapacity = -1;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));

            // BatteryCapacity: greater than to 101
            dto.BatteryCapacity = 501;
            Assert.CatchAsync<ValidationException>(async () => await controller.Post(dto, default));
        }


        //
        // Repeat the process for the rest of the controller methods
        //

    }
}