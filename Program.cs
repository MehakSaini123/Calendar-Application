using System.Configuration;
using Microsoft.SqlServer;
using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


/*Connectionstring*/
builder.Services.AddDbContext<Calendar.Models.CalendarContext>(options =>
{
    options.UseSqlServer("Server = localhost\\SQLEXPRESS;Database = CalendarEvents; Trusted_Connection= True ; TrustServerCertificate = True; ");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calendar}/{action=Index}/{id?}");

app.Run();
