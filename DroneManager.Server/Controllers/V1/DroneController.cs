using AutoMapper;
using DroneManager.Application.Commands;
using DroneManager.Application.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DroneManager.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DroneController : ApiControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public DroneController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(Response<DroneDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(int id, CancellationToken cancellationToken)
        {
            var query = new DroneGetQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get all Drones
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<DroneDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new DroneGetAllQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create Drone
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<DroneDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] DroneDTO dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DroneCreateCommand>(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update Drone
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response<DroneDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] DroneDTO dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DroneUpdateCommand>(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete Drone
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("id")]
        [ProducesResponseType(typeof(Response<DroneDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, CancellationToken cancellationToken)
        {
            var command = new DroneDeleteCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Load Drone
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut, Route("load")]
        [ProducesResponseType(typeof(Response<DroneLoadDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoadDrone([FromBody] DroneLoadDTO dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DroneLoadCommand>(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get available Drones
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("available")]
        [ProducesResponseType(typeof(Response<DroneBatteryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Available(CancellationToken cancellationToken)
        {
            var query = new DroneAvailableQuery { };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Check Drone battery
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("check-battery/{id}")]
        [ProducesResponseType(typeof(Response<DroneBatteryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckBattery(int id, CancellationToken cancellationToken)
        {
            var query = new DroneBatteryQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Check Drone medication weight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("medication-weight/{id}")]
        [ProducesResponseType(typeof(Response<DroneMedicationWeightDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MedicationWeight(int id, CancellationToken cancellationToken)
        {
            var query = new DroneMedicationWeightQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
