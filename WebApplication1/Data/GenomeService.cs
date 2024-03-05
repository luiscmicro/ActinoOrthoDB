using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;
public class GenomeService : IGenomeService
{
    private readonly WebApplication1Context _context;

    public GenomeService(WebApplication1Context context) => _context = context;

    public async Task<Genome> FindAsync(int id)
    {
        return await _context.Genomes.FindAsync(id);
    }

    public async Task<List<Genome>> GetAllAsync() =>
        await _context.Genomes
        .Include(x => x.Name)
        .Include(x => x.Id)
        .ToListAsync();



    public async Task<Genome> CreateAsync(Genome genome)
    {
        _context.Add(genome);
        await _context.SaveChangesAsync();
        return genome;
    }
    public async Task<Genome> UpdateAsync(Genome genome)
    {
        _context.Update(genome);
        await _context.SaveChangesAsync();
        return genome;
    }

}








