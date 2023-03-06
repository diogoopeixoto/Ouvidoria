using Microsoft.EntityFrameworkCore;
using Ouvidoria.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
});
builder.Services.AddMvc();

string apiUrl = builder.Configuration.GetSection("APIUrl").Value;
ApiUrl.SetUrlBase(apiUrl);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LayoutInicial}/{action=Index}/{id?}");

app.Run();