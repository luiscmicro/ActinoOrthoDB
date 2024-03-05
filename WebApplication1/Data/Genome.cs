using WebApplication1.Pages;

namespace WebApplication1.Data
{
 public class Genome 
    {
        public int Id {get; set;}
        public string? Name {get;set;}
        public List<CodingRegion> Proteins {get;set;} = new List<CodingRegion>();
    }
}