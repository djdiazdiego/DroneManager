// <auto-generated />
using System;
using DroneManager.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DroneManager.Infrastructure.Migrations
{
    [DbContext(typeof(DbContextWrite))]
    partial class DbContextWriteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("DroneManager")
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DroneManager.Core.Models.StorageFileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsImage")
                        .HasColumnType("bit");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StorageFileData", "DroneManager");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.Drone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BatteryCapacity")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.HasIndex("StatusId");

                    b.ToTable("Drone", "DroneManager");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.DroneModel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("DroneModel", "DroneManager");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.DroneStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("DroneStatus", "DroneManager");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DroneId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("StorageFileDataId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DroneId");

                    b.HasIndex("StorageFileDataId")
                        .IsUnique()
                        .HasFilter("[StorageFileDataId] IS NOT NULL");

                    b.ToTable("Medicine", "DroneManager");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.Drone", b =>
                {
                    b.HasOne("DroneManager.Domain.Models.DroneModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DroneManager.Domain.Models.DroneStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Model");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.Medicine", b =>
                {
                    b.HasOne("DroneManager.Domain.Models.Drone", "Drone")
                        .WithMany("Medicines")
                        .HasForeignKey("DroneId");

                    b.HasOne("DroneManager.Core.Models.StorageFileData", "StorageFileData")
                        .WithOne()
                        .HasForeignKey("DroneManager.Domain.Models.Medicine", "StorageFileDataId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Drone");

                    b.Navigation("StorageFileData");
                });

            modelBuilder.Entity("DroneManager.Domain.Models.Drone", b =>
                {
                    b.Navigation("Medicines");
                });
#pragma warning restore 612, 618
        }
    }
}
