using WebApplication1.Data;
namespace WebApplication1.Services{
    public interface IGenomeService
    {
        Task<Genome> CreateAsync(Genome genome);
        Task<List<Genome>> GetAllAsync();
        Task<Genome> FindAsync(int id);
        Task<Genome> UpdateAsync(Genome genome);
        //Task DeleteAsync(int id);
    }

}
    
