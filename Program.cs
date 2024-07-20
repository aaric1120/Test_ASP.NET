using System.Text.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddRazorPages();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.AllowAnyOrigin() // Or specify allowed origins
              .AllowAnyMethod() // Or specify allowed methods
              .AllowAnyHeader(); // Or specify allowed headers
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMyDataRepository, MyDataRepository>();
builder.Services.AddScoped<IMyDataService, MyDataService>();

var app = builder.Build();

app.UseCors("MyPolicy");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//var httpsPort = builder.Configuration.GetValue<int>("HttpsPort", 7080);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// app.MapRazorPages();
/*app.UseEndpoints( endpoints => 
{
    
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    
    endpoints.MapRazorPages();
}
);
*/

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapFallbackToFile("index.html"); // Serve the React app

/*app.MapMethods("/api/data", new[] { "OPTIONS" }, (HttpContext context) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Allow", "GET, POST, OPTIONS");
    context.Response.Headers.Add("Content-Type", "text/plain");
    context.Response.StatusCode = 204; // No Content
    return Task.CompletedTask;
    
});

app.MapPost("/api/data", async (HttpContext context) => 
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    var data = JsonSerializer.Deserialize<MyData>(body);

    if (data != null) {
        Console.WriteLine(data.Name);
    }
    return Results.Ok(new {message = "Data received and processed", data});
});
*/

app.Run();
