﻿using DroneManager.Core.Abstractions.Queries;
using DroneManager.Core.Wrappers;
using DroneManager.Domain.DTOs;

namespace DroneManager.Application.Queries
{
    public sealed class MedicineGetAllQuery : IQuery<Response<IEnumerable<MedicineDTO>>>
    {
        
    }
}
