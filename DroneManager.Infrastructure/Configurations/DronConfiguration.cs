using DroneManager.Core.Data.Configurations;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneManager.Infrastructure.Configurations
{
    public class DronConfiguration : BaseEntityConfiguration<Dron>
    {
        public override void Configure(EntityTypeBuilder<Dron> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.SerialNumber).HasMaxLength(100);

            builder.Property(p => p.BatteryCapacity).HasPrecision(5, 2);

            builder.HasOne(p => p.Model)
                .WithMany()
                .HasForeignKey(p => p.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
