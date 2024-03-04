using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;
public class CodingRegionService : ICodingRegionService
{
    private readonly WebApplication1Context _context;

    public CodingRegionService(WebApplication1Context context) => _context = context;

    public async Task<CodingRegion> FindAsync(int id)
    {
        return await _context.CodingRegions.FindAsync(id);
    }

    public async Task<List<CodingRegion>> GetAllAsync() =>
        await _context.CodingRegions
        .Include(x => x.Sequence)
        .ToListAsync();

    public async Task<CodingRegion> CreateAsync(CodingRegion codingregion)
    {
        _context.Add(codingregion);
        await _context.SaveChangesAsync();
        return codingregion;
    }
    public async Task<CodingRegion> UpdateAsync(CodingRegion codingregion)
    {
        _context.Update(codingregion);
        await _context.SaveChangesAsync();
        return codingregion;
    }

}