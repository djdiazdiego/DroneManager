using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Validators;
using DroneManager.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DroneManager.Application.Commands
{
    public sealed class DroneUpdateCommandValidator : BaseValidator<DroneUpdateCommand>
    {
        public DroneUpdateCommandValidator(IQueryRepository<Drone> queryRepository) : base()
        {
            RuleFor(p => p.Id)
                .MustAsync((id, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AnyAsync(p => p.Id == id, cancellationToken);
                }).WithMessage("Drone not found");

            RuleFor(p => p.SerialNumber)
                .NotNull()
                .NotEmpty()
                .MustAsync((command, value, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AllAsync(p => p.SerialNumber != value || p.Id == command.Id, cancellationToken);
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
