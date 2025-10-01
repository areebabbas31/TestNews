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
    var testConnectionString = "Server=sql,1433;Database=master;User Id=sa;Password=Qaz@xsw12;Encrypt=False;TrustServerCertificate=True";

    using var conn = new SqlConnection(testConnectionString);
    conn.Open();
    Console.WriteLine(" Connected to SQL Server from inside container.");
}
catch (Exception ex)
{
    Console.WriteLine(" Could not connect to SQL Server.");
    Console.WriteLine(ex.ToString());
}

app.Run();
