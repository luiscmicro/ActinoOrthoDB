using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
   
    public class CodingRegion{
        public int Id {get; set;}
        public int GenomeId {get; set;}
        public int Location {get;set;}
        public string Sequence {get; set;}
    }



    public class WelcomeModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet(int id)
        {
            Message = $"OnGet executed with id = {id}";
        }

        public void OnPost()
        {
            Message = "OnPost executed";
        }

    }
}
