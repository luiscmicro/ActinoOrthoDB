using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication1.Data;
using WebApplication1.ValidationAttributes;

namespace WebApplication1.Pages.OrthoGroupManager.OrthoGroupUI
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

        [BindProperty]
        public OrthoGroup OrthoGroup { get; set; }

        public List<CodingRegion> cds {get; set;}

        [BindProperty]                                                      
        [Required]                                                          
        [UploadFileExtensions(Extensions = ".fa")]                         
        public IFormFile Upload { get; set; }                               
        [TempData]                                                          
        public string OGFastaFile { get; set; }

        public static async Task<string> ReadFormFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                string v = await Task.FromResult((string)"EMPTY");
                return v;
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
                            string[] faa_header_underscore = line.Split("_");

                            cds_temp.genomeFile = System.IO.Path.GetFileNameWithoutExtension(faa_header_underscore[0].Remove(0,1));
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
                            cds_temp.OrthoGroupId=OrthoGroup.Id;
                            string[] faa_header_underscore = line.Split("_");
                            cds_temp.genomeFile = System.IO.Path.GetFileNameWithoutExtension(faa_header_underscore[0].Remove(0,1));
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
            var FaaFileString = await fileUploaded;
            
            OrthoGroup.Name = System.IO.Path.GetFileNameWithoutExtension(Upload.FileName);
            OrthoGroup.Proteins = ParseFaaFile(FaaFileString);

            _context.OrthoGroup.Add(OrthoGroup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
