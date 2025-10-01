using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient; // Make sure you installed this NuGet package

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Enable static files and routing
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
try
{
    var connStr = "Server=sql,1433;Database=master;User Id=sa;Password=Qaz@xsw12;Encrypt=False;TrustServerCertificate=True;Connect Timeout=5;";
    using var conn = new SqlConnection(connStr);
    conn.Open();
    Console.WriteLine("✅ SQL connection success!");
}
catch (Exception ex)
{
    Console.WriteLine("❌ SQL connection failed: " + ex.Message);
}


app.Run();
