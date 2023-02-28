using AutoMapper;
using DroneManager.Core.Abstractions.Commands;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Models;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using DroneManager.FileStorage;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Commands
{
    public sealed class MedicineDeleteCommandHandler : ICommandHandler<MedicineDeleteCommand, Response<MedicineDTO>>
    {
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly IRepository<StorageFileData> _storageFileRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineRepository"></param>
        /// <param name="storageFileRepository"></param>
        /// <param name="fileStorageService"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public MedicineDeleteCommandHandler(
            IRepository<Medicine> medicineRepository,
            IRepository<StorageFileData> storageFileRepository,
            IFileStorageService fileStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _medicineRepository = medicineRepository;
            _storageFileRepository = storageFileRepository;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<MedicineDTO>> Handle(MedicineDeleteCommand request, CancellationToken cancellationToken)
        {
            var medicine = await _medicineRepository.GetQuery()
                .Include(p => p.StorageFileData)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            var response = _mapper.Map<MedicineDTO>(medicine);

            if (medicine.StorageFileDataId != null)
            {
                var storageFileDataId = medicine.StorageFileDataId;
                var key = medicine.StorageFileData.Key;

                await _fileStorageService.DeleteFile(key, cancellationToken);

                medicine.StorageFileDataId = null;

                _medicineRepository.Update(medicine);

                var storageFileData = await _storageFileRepository.FindAsync(new object[] { storageFileDataId }, cancellationToken);

                _storageFileRepository.Remove(storageFileData);
            }

            _medicineRepository.Remove(medicine);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response<MedicineDTO>(response);
        }
    }
}
