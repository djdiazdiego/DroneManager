using AutoMapper;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Queries
{
    public sealed class DroneGetAllQueryHandler : IQueryHandler<DroneGetAllQuery, Response<IEnumerable<DroneDTO>>>
    {
        private readonly IQueryRepository<Drone> _droneRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneRepository"></param>
        /// <param name="mapper"></param>
        public DroneGetAllQueryHandler(
            IQueryRepository<Drone> droneRepository,
            IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<DroneDTO>>> Handle(DroneGetAllQuery request, CancellationToken cancellationToken)
        {
            var drones = await _droneRepository.GetQuery().ToArrayAsync(cancellationToken);
            var response = _mapper.Map<IEnumerable<DroneDTO>>(drones);

            return new Response<IEnumerable<DroneDTO>>(response);
        }
    }
}
