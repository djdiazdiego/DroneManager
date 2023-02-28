using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Validators;
using DroneManager.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DroneManager.Application.Commands
{
    public sealed class MedicineUpdateCommandValidator : BaseValidator<MedicineUpdateCommand>
    {
        public MedicineUpdateCommandValidator(IQueryRepository<Medicine> queryRepository) : base()
        {
            RuleFor(p => p.Id)
                .MustAsync((id, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AnyAsync(p => p.Id == id, cancellationToken);
                }).WithMessage("Medicine not found");

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .Must((name) =>
                {
                    return Regex.IsMatch(name, @"^[a-zA-Z0-9_-]+$");
                }).WithMessage("Allowed only letters, numbers, '-' and '_'");

            RuleFor(p => p.Weight)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Code)
                .NotNull()
                .NotEmpty()
                .Must((code) =>
                {
                    return Regex.IsMatch(code, @"^[A-Z0-9_]+$");
                }).WithMessage("A-1llowed only capital letters, numbers and '_'")
                .MustAsync((command, code, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AllAsync(p => p.Code != code || p.Id == command.Id, cancellationToken);
                }).WithMessage("A medicine already exists with the same Code");
        }
    }
}
