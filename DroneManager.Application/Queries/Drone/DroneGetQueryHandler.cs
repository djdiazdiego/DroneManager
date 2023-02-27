using AutoMapper;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Application.Queries
{
    public sealed class DroneGetQueryHandler : IQueryHandler<DroneGetQuery, Response<DroneDTO>>
    {
        private readonly IQueryRepository<Drone> _droneRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneRepository"></param>
        /// <param name="mapper"></param>
        public DroneGetQueryHandler(
            IQueryRepository<Drone> droneRepository,
            IMapper mapper)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
        }

        public async Task<Response<DroneDTO>> Handle(DroneGetQuery request, CancellationToken cancellationToken)
        {
            var drone = await _droneRepository.FindAsync(new object[] { request.Id }, cancellationToken);
            var response = _mapper.Map<DroneDTO>(drone);

            return new Response<DroneDTO>(response);
        }
    }
}
