using Lexus.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services to the application.
builder.Services.AddControllersWithViews();

// Register the database context and configure PostgreSQL as the database provider.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Handle unexpected errors in production.
    app.UseExceptionHandler("/Home/Error");

    // Enable HTTP Strict Transport Security (HSTS).
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Enable authorization middleware.
app.UseAuthorization();

// Map static assets such as CSS, JavaScript, and images.
app.MapStaticAssets();

// Configure the default MVC route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Start the application.
app.Run();