using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.ValidationAttributes;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Build.Tasks;

namespace WebApplication1.Pages.GenomeManager.GenomeUI
{
    public class EditModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public EditModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Genome Genome { get; set; }
        
        [BindProperty]
        public List<CodingRegion> CDS {get;set;}

        [BindProperty]                                                      
        [Required]                                                          
        [UploadFileExtensions(Extensions = ".csv")]                         
        public IFormFile Upload { get; set; }
        public static async Task<string> ReadFormFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return await Task.FromResult((string)null);
            }
    
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genome = await _context.Genomes.FirstOrDefaultAsync(m => m.Id == id);
            CDS = await _context.CodingRegions.ToListAsync();
            //Console.WriteLine("are all records here?"+ CDS.Count);

            

            if (Genome == null)
            {
                return NotFound();
            }
            return Page();
        }

        private List<CodingRegion> ParseCSVFile(string faafile, List<CodingRegion> _cds){
            var str = faafile;
            //Console.WriteLine("We are about to parse file###############################");
            List<CodingRegion> list = new List<CodingRegion>();
            //CodingRegion cds_temp = new CodingRegion();
            //Console.WriteLine("How many items in _cds?"+_cds.Count);
            //foreach(var item in _cds){
            //    Console.WriteLine("item:"+item.Header);
            //}

            using (System.IO.StringReader reader = new System.IO.StringReader(faafile)) {
                while(true){
                    var line = reader.ReadLine();
                    if(string.IsNullOrEmpty(line))
                        break;
                    
                    if(line.StartsWith("Name")){
                        //Console.WriteLine("Header Line");
                    }else{

                        string[] cvs_line = line.Split(",");
                        string searching_h = ">"+cvs_line[1];

                           //Console.WriteLine("Uploaded Header"+searching_h);
                            //Console.WriteLine("How many proteins?"+ _cds.);
                            var result = _cds.Find(s=> s.Header==searching_h);
                            
                            //var result = _cds.Where(x => x.Header == searching_h);
                            
                            if( result == null){
                                //Console.WriteLine("Header not found"+searching_h);
                            }else{
                                //Console.WriteLine("Header found"+result.Header);
                                
                                CodingRegion? found = result;
                                found.LocusTag=cvs_line[2];
                                //Console.WriteLine("Header found"+found.Header);
                                list.Add(found);
                             }
                        //cds_temp.AddToSequence(line);
                    }
                }
            }
            return list;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            CDS = await _context.CodingRegions.ToListAsync();
            //Console.WriteLine("are all records here?"+ CDS.Count);
            //Console.WriteLine("We want to parse some stuff but checking modelStat first");
            if (!ModelState.IsValid)
            {
                //Console.WriteLine("Model state is apparently not valid");
                return Page();
            }
            _context.Attach(Genome).State = EntityState.Modified;
            Task<string> fileUploaded = ReadFormFileAsync(Upload);
            //Console.WriteLine("We want to parse some stuff ");
            var FaaFileString = await fileUploaded;
            
            List<CodingRegion> cds= ParseCSVFile(FaaFileString,CDS);
            //Console.WriteLine("Size: "+cds.Count);
            Genome.Proteins=cds;

            //Console.WriteLine("Genome Model has: "+Genome.Proteins.Count);
            //Console.WriteLine("Genome ID: "+Genome.Proteins.FirstOrDefault().Header);
            //Console.WriteLine("ID: "+Genome.Proteins.FirstOrDefault().Id);
            //Console.WriteLine("ID: "+Genome.Proteins.FirstOrDefault().Header);
            //Console.WriteLine("ID: "+Genome.Proteins.FirstOrDefault().Location);
            //Genome.Proteins=cds;
            //Console.WriteLine("First protein: "+Genome.Proteins[0].LocusTag);
            //_context.Attach(Genome).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenomeExists(Genome.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GenomeExists(int id)
        {
            return _context.Genomes.Any(e => e.Id == id);
        }
    }
}
