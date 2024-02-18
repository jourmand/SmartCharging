using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Core.Domain.GroupItemAggregate;

namespace SmartCharging.Infrastructures.Data.GroupItemAggregate.Configuration;

public class ConnectorConfig : IEntityTypeConfiguration<Connector>
{
    public void Configure(EntityTypeBuilder<Connector> builder)
    {
        builder.ToTable("Connectors");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.OwnsOne(c => c.MaxCurrentInAmps, d =>
        {
            d.Property(e => e.Value)
            .IsRequired()
            .HasColumnName("MaxCurrentInAmps");
        });
    }
}