using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Pages.OrthoGroupManager
{
    public class SearchByCDSModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public SearchByCDSModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        public IList<Genome> Genomes { get;set; }
        public IList<CodingRegion> CDS { get;set; }
        
        public SelectList CDSList { get; set; }


        [BindProperty, Display(Name = "GenomeId")]
        public int GenomeId { get; set; }

        public async Task OnGetAsync()
        {
            Genomes = await _context.Genomes.ToListAsync();
            CDS= await _context.CodingRegions.ToListAsync();
            CDSList = await GetGenomesOptions();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Hello we are on Post");
            Genomes = await _context.Genomes.ToListAsync();
            CDS= await _context.CodingRegions.ToListAsync();
            CDSList = await GetGenomesOptions();

            

            return Page();
        }

        private async Task<SelectList> GetGenomesOptions() {
            var proteins = CDS;
            return new SelectList(proteins, nameof(CodingRegion.Id), nameof(CodingRegion.LocusTag));
        }

        


    }
}
