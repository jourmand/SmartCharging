using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Domain.GroupItemAggregate;

namespace SmartCharging.Infrastructures.Data.GroupItemAggregate.Configuration;

public class ChargeStationConfig : IEntityTypeConfiguration<ChargeStation>
{
    public void Configure(EntityTypeBuilder<ChargeStation> builder)
    {
        builder.ToTable("ChargeStations");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder
            .HasMany(g => g.Connectors)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata
            .PrincipalToDependent
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}