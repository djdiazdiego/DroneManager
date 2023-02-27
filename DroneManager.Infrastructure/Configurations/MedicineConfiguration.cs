using DroneManager.Core.Data.Configurations;
using DroneManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DroneManager.Infrastructure.Configurations
{
    public class MedicineConfiguration : BaseEntityConfiguration<Medicine>
    {
        public override void Configure(EntityTypeBuilder<Medicine> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasMaxLength(256);

            builder.Property(p => p.Code).HasMaxLength(256);

            builder.HasOne(p => p.StorageFileData)
                .WithOne()
                .HasForeignKey(typeof(Medicine), nameof(Medicine.StorageFileDataId))
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
