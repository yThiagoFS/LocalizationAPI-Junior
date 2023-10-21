using BaltaIoChallenge.WebApi.Models.v1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaltaIoChallenge.WebApi.Data.v1.Mappings
{
    public class IBGEMap : IEntityTypeConfiguration<IBGE>
    {
        public void Configure(EntityTypeBuilder<IBGE> builder)
        {
            builder.ToTable("Ibge");

            builder
                .HasIndex(x => x.City, "IX_IBGE_City")
                .IsDescending(false);

            builder
                .HasIndex(x => x.State, "IX_IBGE_State")
                .IsDescending(false);

            builder
                .HasIndex(x => x.Id, "IX_IBGE_Id")
                .IsDescending(false);

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("CHAR")
                .HasMaxLength(7)
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(x => x.State)
                .HasColumnType("CHAR")
                .HasMaxLength(2)
                .IsRequired();

            builder
                .Property(x => x.City)
                .HasColumnType("VARCHAR")
                .HasMaxLength(80)
                .IsRequired();
        }
    }
}
