using DroneManager.Core.Data.Configurations;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneManager.Infrastructure.Configurations
{
    public class DronModelConfiguration : BaseEnumerationConfiguration<DronModel>
    {
        public override void Configure(EntityTypeBuilder<DronModel> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(32);
        }
    }
}
