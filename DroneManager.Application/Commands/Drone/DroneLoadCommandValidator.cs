using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Validators;
using DroneManager.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Commands
{
    public sealed class DroneLoadCommandValidator : BaseValidator<DroneLoadCommand>
    {
        public DroneLoadCommandValidator(IQueryRepository<Drone> queryRepository, IQueryRepository<Medicine> medicineQueryRepository) : base()
        {
            RuleFor(p => p.Id)
                .MustAsync((id, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AnyAsync(p => p.Id == id, cancellationToken);
                }).WithMessage("Drone not found");
        }
    }
}
