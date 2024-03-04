using WebApplication1.Data;
namespace WebApplication1.Services{
    public interface ICodingRegionService
    {
        Task<CodingRegion> CreateAsync(CodingRegion property);
        Task<List<CodingRegion>> GetAllAsync();
        Task<CodingRegion> FindAsync(int id);
        Task<CodingRegion> UpdateAsync(CodingRegion property);
        Task DeleteAsync(int id);
    }

}
    
