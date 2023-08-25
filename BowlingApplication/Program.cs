using BowlingApplication;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "scripts")),
    RequestPath = "/scripts"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "styles")),
    RequestPath = "/styles"
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "bowlingGame",
        pattern: "BowlingGame.cshtml",
        defaults: new {controller = "BowlingGame", action = "Index"});
    
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllers();
});

app.MapRazorPages();
Service.Players = new();
Service.CurrentIndex = 0;
Service.PlayerHasRolled = false;
app.Run();
