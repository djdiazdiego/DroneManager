using AutoMapper;
using DroneManager.Core.Abstractions.Commands;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using DroneManager.FileStorage;

namespace DroneManager.Application.Commands
{
    public sealed class MedicineCreateCommandHandler : ICommandHandler<MedicineCreateCommand, Response<MedicineDTO>>
    {
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineRepository"></param>
        /// <param name="fileStorageService"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        public MedicineCreateCommandHandler(
            IRepository<Medicine> medicineRepository,
            IFileStorageService fileStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _medicineRepository = medicineRepository;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<MedicineDTO>> Handle(MedicineCreateCommand request, CancellationToken cancellationToken)
        {
            var storageFileData = request.File != null ? await _fileStorageService.UploadFile(request.File, cancellationToken) : null;

            var medicine = _mapper.Map<Medicine>(request);

            if (storageFileData != null)
                medicine.StorageFileDataId = storageFileData.Id;

            _medicineRepository.Add(medicine);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<MedicineDTO>(medicine);

            return new Response<MedicineDTO>(response);
        }
    }
}
