using AutoMapper;
using DroneManager.Core.Abstractions.Commands;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;

namespace DroneManager.Application.Commands
{
    public sealed class DroneDeleteCommandHandler : ICommandHandler<DroneDeleteCommand, Response<DroneDTO>>
    {
        private readonly IRepository<Drone> _droneRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public DroneDeleteCommandHandler(
            IRepository<Drone> droneRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _droneRepository = droneRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DroneDTO>> Handle(DroneDeleteCommand request, CancellationToken cancellationToken)
        {
            var drone = await _droneRepository.FindAsync(new object[] { request.Id }, cancellationToken);

            _droneRepository.Remove(drone);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<DroneDTO>(drone);

            return new Response<DroneDTO>(response);
        }
    }
}
