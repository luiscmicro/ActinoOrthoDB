using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 
namespace WebApplication1.Data.Configuration;
public class GenomeConfiguration : IEntityTypeConfiguration<Genome>
{
    public void Configure(EntityTypeBuilder<Genome> builder)
    {
        builder.Property(x => x.Name )
            .HasMaxLength(200);
    }
}

