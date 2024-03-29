using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;
// using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebApplication1Context>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("WebApplication1Context"));
});


builder.Services.AddRazorPages();

builder.Services.AddScoped<ICodingRegionService, CodingRegionService>();
builder.Services.AddScoped<IGenomeService, GenomeService>();

//builder.Services.AddScoped<ICodingRegionService,CodingRegionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
