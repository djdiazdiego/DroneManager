using AutoMapper;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Queries
{
    public sealed class MedicineGetQueryHandler : IQueryHandler<MedicineGetQuery, Response<MedicineDTO>>
    {
        private readonly IQueryRepository<Medicine> _medicineRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineRepository"></param>
        /// <param name="mapper"></param>
        public MedicineGetQueryHandler(
            IQueryRepository<Medicine> medicineRepository,
            IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }

        public async Task<Response<MedicineDTO>> Handle(MedicineGetQuery request, CancellationToken cancellationToken)
        {
            var medicine = await _medicineRepository.GetQuery()
                .Include(p => p.StorageFileData)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            var response = _mapper.Map<MedicineDTO>(medicine);

            return new Response<MedicineDTO>(response);
        }
    }
}
