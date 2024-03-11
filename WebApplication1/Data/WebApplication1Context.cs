using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Configuration;
//using WebApplication1.Pages;
using WebApplication1.Data;
using WebApplication1.Services;

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
            builder.ApplyConfiguration(new CodingRegionConfiguration());

            //builder.Entity<Genome>().HasMany(s => s.Proteins).WithOne(s=>s.GenomeId);
            
        }


    }
}