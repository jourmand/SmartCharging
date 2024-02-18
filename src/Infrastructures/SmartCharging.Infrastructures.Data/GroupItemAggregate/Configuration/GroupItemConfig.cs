using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Domain.GroupItemAggregate;

namespace SmartCharging.Infrastructures.Data.GroupItemAggregate.Configuration;

public class GroupItemConfig : IEntityTypeConfiguration<GroupItem>
{
    public void Configure(EntityTypeBuilder<GroupItem> builder)
    {
        builder.ToTable("Groups");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(c => c.CapacityInAmps, d =>
        {
            d.Property(e => e.Value)
            .IsRequired()
            .HasColumnName("CapacityInAmps");
        });

        builder
            .HasMany(g => g.ChargeStations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata
            .PrincipalToDependent
            .SetPropertyAccessMode(PropertyAccessMode.Field); 
    }
}

