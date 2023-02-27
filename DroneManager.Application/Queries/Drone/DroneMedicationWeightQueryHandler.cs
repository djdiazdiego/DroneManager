using AutoMapper;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Queries
{
    public sealed class DroneMedicationWeightQueryHandler : IQueryHandler<DroneMedicationWeightQuery, Response<DroneMedicationWeightDTO>>
    {
        private readonly IQueryRepository<Drone> _droneRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneRepository"></param>
        /// <param name="mapper"></param>
        public DroneMedicationWeightQueryHandler(
            IQueryRepository<Drone> droneRepository,
            IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<Response<DroneMedicationWeightDTO>> Handle(DroneMedicationWeightQuery request, CancellationToken cancellationToken)
        {
            var response = await _droneRepository.GetQuery()
                .Include(p => p.Medicines).Select(p => new DroneMedicationWeightDTO
                {
                    Id = p.Id,
                    Weight = p.Weight,
                    MedicationWeight = p.Medicines.Any() ? p.Medicines.Sum(m => m.Weight) : 0
                }).FirstOrDefaultAsync(cancellationToken);

            return new Response<DroneMedicationWeightDTO>(response);
        }
    }
}
