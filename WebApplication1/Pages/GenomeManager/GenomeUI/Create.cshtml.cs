using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.ValidationAttributes;
using System.Diagnostics;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace WebApplication1.Pages.GenomeManager.GenomeUI
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;

        public CreateModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public List<CodingRegion> cds {get; set;}

        [BindProperty]
        public Genome Genome { get; set; }

        [BindProperty]                                                      
        [Required]                                                          
        [UploadFileExtensions(Extensions = ".faa")]                         
        public IFormFile Upload { get; set; }                               
        [TempData]                                                          
        public string GenomeFastaFile { get; set; }

        //public Task<List<string>> FaFile(this IFormFile file) => file.ReadAsListAsync();

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
        private List<CodingRegion> ParseFaaFile(string faafile){
            var str = faafile;
            List<CodingRegion> list = new List<CodingRegion>();
            CodingRegion cds_temp = new CodingRegion();
            using (System.IO.StringReader reader = new System.IO.StringReader(faafile)) {
                while(true){
                    var line = reader.ReadLine();

                    if(string.IsNullOrEmpty(line))
                        break;
                    
                    if(line.StartsWith(">")){
                        //Console.WriteLine("New Protein reset cds_temp");
                        if(string.IsNullOrEmpty(cds_temp.Header)){
                            //Console.WriteLine("That was the first protein");
                            cds_temp.Header = line;
                            string[] faa_header = line.Split(".");
                            //Console.WriteLine("Protein position in Genome"+faa_header[3]);
                            try{
                                cds_temp.Location=Int32.Parse(faa_header[3]);
                            }
                            catch (FormatException){
                                Console.WriteLine($"Unable to parse '{faa_header[3]}'");
                            }

                            
                        }else{
                            list.Add(cds_temp);
                            cds_temp = new CodingRegion();
                            cds_temp.GenomeId = Genome.Id;
                            cds_temp.Header = line;
                            string[] faa_header = line.Split(".");
                            //Console.WriteLine("Protein position in Genome"+faa_header[3]);
                            try{
                                cds_temp.Location=Int32.Parse(faa_header[3]);
                            }
                            catch (FormatException){
                                Console.WriteLine($"Unable to parse '{faa_header[3]}'");
                            }
                            
                        }
                        
                    }else{
                        cds_temp.AddToSequence(line);
                    }
                        
                }

            }
            return list;
        }




        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

             Task<string> fileUploaded = ReadFormFileAsync(Upload);
             Console.WriteLine("Some message here");
             var FaaFileString = await fileUploaded;

             Genome.Proteins = ParseFaaFile(FaaFileString);

            _context.Genomes.Add(Genome);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
