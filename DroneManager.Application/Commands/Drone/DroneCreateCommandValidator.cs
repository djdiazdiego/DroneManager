using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Validators;
using DroneManager.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Commands
{
    public sealed class DroneCreateCommandValidator : BaseValidator<DroneCreateCommand>
    {
        public DroneCreateCommandValidator(IQueryRepository<Drone> queryRepository) : base()
        {

            RuleFor(p => p.SerialNumber)
                .NotNull()
                .NotEmpty()
                .MustAsync((value, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AllAsync(p => p.SerialNumber != value, cancellationToken);
                }).WithMessage("A drone already exists with the same Serial Number")
                .MaximumLength(100);

            RuleFor(p => p.Weight)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(500);

            RuleFor(p => p.BatteryCapacity)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
        }
    }
}
