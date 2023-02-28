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
    public class MedicineController : ApiControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        public MedicineController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Medicine
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(Response<MedicineDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(int id, CancellationToken cancellationToken)
        {
            var query = new MedicineGetQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get all Medicines
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet, Route("all")]
        [ProducesResponseType(typeof(Response<IEnumerable<MedicineDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new MedicineGetAllQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create Medicine
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<MedicineDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] MedicineFileDTO dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<MedicineCreateCommand>(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update Medicine
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response<MedicineDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromForm] MedicineFileDTO dto, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<MedicineUpdateCommand>(dto);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Delete Medicine
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete, Route("id")]
        [ProducesResponseType(typeof(Response<MedicineDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, CancellationToken cancellationToken)
        {
            var command = new MedicineDeleteCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
