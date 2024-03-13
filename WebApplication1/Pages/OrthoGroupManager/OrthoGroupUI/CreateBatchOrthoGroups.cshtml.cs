using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;

namespace WebApplication1.Pages
{
    public class CreateBatchOrthoGroupsModel : PageModel
    
    {
        private readonly WebApplication1.Data.WebApplication1Context _context;
        public CreateBatchOrthoGroupsModel(WebApplication1.Data.WebApplication1Context context)
        {
            _context = context;
        }
        [BindProperty]                                                          
        public string UploadFilePath { get; set; }

        [TempData]                                                          
        public string GenomeFastaFile { get; set; }


        public void OnGet()
        {

        }
        public static async Task<string> ReadFormFileAsync(string file)
        {
            if (file == null || file.Length == 0)
            {
                string v = await Task.FromResult((string)"EMPTY");
                return v;
            }
    
            using (var reader = new StreamReader(file))
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
                            //cds_temp.OrthoGroupId=OrthoGroup.Id;
                            //cds_temp.GenomeId = Genome.Id;
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
                            
                        }
                        
                    }else{
                        cds_temp.AddToSequence(line);
                    }
                        
                }

            }
            return list;
        }



        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Hello this is a the path:" + UploadFilePath);
            Console.WriteLine(UploadFilePath);
            //int count = System.IO.Directory.GetFiles(UploadFilePath).Length;
            //Console.WriteLine("File Count:" + count);
            string [] fileEntries = Directory.GetFiles(UploadFilePath);
            foreach(string fileName in fileEntries){
                Task<string> fileUploaded = ReadFormFileAsync(fileName);
                var FaaFileString = await fileUploaded;
                OrthoGroup og = new OrthoGroup();
                og.Name = System.IO.Path.GetFileNameWithoutExtension(fileName);
                og.Proteins = ParseFaaFile(FaaFileString);
                _context.OrthoGroup.Add(og);
                await _context.SaveChangesAsync();
            }



            
             //   ProcessFile(fileName);
            //Directory.GetFiles(UploadFilePath).Length;
            
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //Task<string> fileUploaded = ReadFormFileAsync(Upload);
            //var FaaFileString = await fileUploaded;
            
            //OrthoGroup.Name = System.IO.Path.GetFileNameWithoutExtension(Upload.FileName);
            //OrthoGroup.Proteins = ParseFaaFile(FaaFileString);

            //_context.OrthoGroup.Add(OrthoGroup);
            //await _context.SaveChangesAsync();
            //await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
