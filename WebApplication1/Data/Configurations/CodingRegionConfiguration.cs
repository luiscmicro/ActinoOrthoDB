using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace WebApplication1.Data.Configuration;
public class CodingRegionConfiguration : IEntityTypeConfiguration<CodingRegion>
{
    public void Configure(EntityTypeBuilder<CodingRegion> builder)
    {
        
        builder.Property(x => x.Header)
            .HasMaxLength(1000);
    }
}

