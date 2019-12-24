using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasctruture.Mappings
{
    public class EnterpriseMap : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> enter)
        {
            enter.ToTable("Enterprise");
            enter.HasKey(x => x.Id);
            enter.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}
