using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class CodingRegion{
        public int Id {get; set;}
        public int GenomeId {get; set;}
        public int Location {get;set;}
        public string? Sequence {get; set;}
        public string? Header {get; set;}
        
        public int AddToSequence(string sequence){
            if(sequence.Length >0){
                Sequence += sequence;
                return 1;
            }else{
                return 0;
            }
            
        }
    }
}
