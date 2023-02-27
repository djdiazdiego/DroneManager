using AutoMapper;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Queries
{
    public sealed class DroneAvailableQueryHandler : IQueryHandler<DroneAvailableQuery, Response<IEnumerable<DroneDTO>>>
    {
        private readonly IQueryRepository<Drone> _droneRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneRepository"></param>
        /// <param name="mapper"></param>
        public DroneAvailableQueryHandler(
            IQueryRepository<Drone> droneRepository,
            IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<DroneDTO>>> Handle(DroneAvailableQuery request, CancellationToken cancellationToken)
        {
            var drones = await _droneRepository.GetQuery()
                .Where(p => p.StatusId == Domain.Enums.DroneStatusValues.IDLE && p.BatteryCapacity > 25 && 
                            ((!p.Medicines.Any() && p.Weight > 0) || (!p.Medicines.Any() && p.Medicines.Sum(x => x.Weight) < p.Weight)))
                .ToArrayAsync(cancellationToken);

            var response = _mapper.Map<IEnumerable<DroneDTO>>(drones);

            return new Response<IEnumerable<DroneDTO>>(response);
        }
    }
}
