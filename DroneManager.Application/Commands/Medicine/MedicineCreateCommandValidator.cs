using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Abstractions.Validators;
using DroneManager.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DroneManager.Application.Commands
{
    public sealed class MedicineCreateCommandValidator : BaseValidator<MedicineCreateCommand>
    {
        public MedicineCreateCommandValidator(IQueryRepository<Medicine> queryRepository) : base()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .Must((name) =>
                {
                    return Regex.IsMatch(name, @"^[a-zA-Z0-9_-]+$");
                }).WithMessage("Allowed only letters, numbers, '-' and '_'");

            RuleFor(p => p.Weight)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Code)
                .NotNull()
                .NotEmpty()
                .Must((code) =>
                {
                    return Regex.IsMatch(code, @"^[A-Z0-9_]+$");
                }).WithMessage("Allowed only capital letters, numbers and '_'")
                .MustAsync((code, cancellationToken) =>
                {
                    return queryRepository.GetQuery().AllAsync(p => p.Code != code, cancellationToken);
                }).WithMessage("A medicine already exists with the same Code");
        }
    }
}
