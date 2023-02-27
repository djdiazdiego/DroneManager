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
                }).WithMessage("Drone not found")
                 .MustAsync((id, cancellationToken) =>
                 {
                     return queryRepository.GetQuery()
                        .AnyAsync(p => p.Id == id && p.BatteryCapacity > 25, cancellationToken);
                 }).WithMessage("battery less than or equal to 25%");

            RuleFor(p => p)
              .MustAsync(async (command, cancellationToken) =>
              {
                  var droneWeight = await queryRepository.GetQuery()
                    .Include(p => p.Medicines)
                    .Select(p => new
                    {
                        weight = p.Weight,
                        medicineWeight = p.Medicines.Any() ? p.Medicines.Sum(x => x.Weight) : 0
                    }).FirstOrDefaultAsync(cancellationToken);

                  var newWeight = await medicineQueryRepository.GetQuery()
                    .Where(p => command.MedicineIds.Contains(p.Id))
                    .SumAsync(p => p.Weight, cancellationToken);

                  return droneWeight.weight < (droneWeight.medicineWeight + newWeight);
              }).WithMessage("the weight of the Medicines exceeds the load of the Drone")
              .OverridePropertyName(nameof(Drone));

        }
    }
}
