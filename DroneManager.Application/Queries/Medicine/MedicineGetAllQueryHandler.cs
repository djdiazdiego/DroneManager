using AutoMapper;
using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Queries
{
    public sealed class MedicineGetAllQueryHandler : IQueryHandler<MedicineGetAllQuery, Response<IEnumerable<MedicineDTO>>>
    {
        private readonly IQueryRepository<Medicine> _medicineRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineRepository"></param>
        /// <param name="mapper"></param>
        public MedicineGetAllQueryHandler(
            IQueryRepository<Medicine> medicineRepository,
            IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<MedicineDTO>>> Handle(MedicineGetAllQuery request, CancellationToken cancellationToken)
        {
            var medicines = await _medicineRepository.GetQuery()
                .Include(p => p.StorageFileData)
                .ToArrayAsync(cancellationToken);
            var response = _mapper.Map<IEnumerable<MedicineDTO>>(medicines);

            return new Response<IEnumerable<MedicineDTO>>(response);
        }
    }
}
