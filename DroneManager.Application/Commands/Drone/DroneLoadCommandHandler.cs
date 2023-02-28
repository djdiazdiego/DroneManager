using AutoMapper;
using DroneManager.Core.Abstractions.Commands;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Commands
{
    public sealed class DroneLoadCommandHandler : ICommandHandler<DroneLoadCommand, Response<DroneLoadDTO>>
    {
        private readonly IRepository<Drone> _droneRepository;
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="droneRepository"></param>
        /// <param name="medicineRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public DroneLoadCommandHandler(
            IRepository<Drone> droneRepository,
            IRepository<Medicine> medicineRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _droneRepository = droneRepository;
            _medicineRepository = medicineRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DroneLoadDTO>> Handle(DroneLoadCommand request, CancellationToken cancellationToken)
        {
            var medicines = await _medicineRepository.GetQuery()
                .Where(p => request.MedicineIds.Contains(p.Id) && p.DroneId != request.Id)
                .ToArrayAsync(cancellationToken);

            foreach (var medicine in medicines)
            {
                medicine.DroneId = request.Id;
            }

            _medicineRepository.UpdateRange(medicines);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = await _droneRepository.GetQuery()
                .Include(p => p.Medicines)
                .Select(p => new DroneLoadDTO
                {
                    Id = p.Id,
                    MedicineIds = p.Medicines.Select(m => m.Id).ToList()
                }).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return new Response<DroneLoadDTO>(response);
        }
    }
}
