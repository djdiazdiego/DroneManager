using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Validators;
using DroneManager.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Queries
{
    public sealed class DroneGetQueryValidator : BaseValidator<DroneGetQuery>
    {
        public DroneGetQueryValidator(IQueryRepository<Drone> queryRepository) : base()
        {
            RuleFor(p => p.Id)
                .MustAsync((id, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AnyAsync(p => p.Id == id, cancellationToken);
                }).WithMessage("Drone not found");
        }
    }
}
