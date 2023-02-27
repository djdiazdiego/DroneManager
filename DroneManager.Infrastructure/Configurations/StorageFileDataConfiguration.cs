using DroneManager.Core.Data.Configurations;
using DroneManager.Core.Models;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneManager.Infrastructure.Configurations
{
    public class StorageFileDataConfiguration : BaseEntityConfiguration<StorageFileData>
    {
        public override void Configure(EntityTypeBuilder<StorageFileData> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
