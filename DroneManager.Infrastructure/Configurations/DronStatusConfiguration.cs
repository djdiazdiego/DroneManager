using DroneManager.Core.Data.Configurations;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneManager.Infrastructure.Configurations
{
    public class DronStatusConfiguration : BaseEnumerationConfiguration<DroneStatus>
    {
        public override void Configure(EntityTypeBuilder<DroneStatus> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(32);
        }
    }
}
