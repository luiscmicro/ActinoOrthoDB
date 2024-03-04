using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Configuration;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context(DbContextOptions options) : base(options)
        {
 
        }    
        public DbSet<Genome> Genomes { get; set; }
        public DbSet<CodingRegion> CodingRegions { get; set; }
        protected override void OnModelCreating (ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new GenomeConfiguration());
        }


    }
}