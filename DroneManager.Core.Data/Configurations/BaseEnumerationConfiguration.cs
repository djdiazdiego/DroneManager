using DroneManager.Core.Abstractions.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneManager.Core.Data.Configurations
{
    public abstract class BaseEnumerationConfiguration<TEnumeration> : BaseEntityConfiguration<TEnumeration>
        where TEnumeration : class, IEnumeration
    {
        public override void Configure(EntityTypeBuilder<TEnumeration> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name);
        }
    }
}
