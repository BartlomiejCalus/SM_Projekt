using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Wordle.Areas.Identity.Data;
using Wordle.Controllers;
using Wordle.Data;
using Wordle.Models;
using Wordle.Models.Events;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WordleContextConnection") ?? throw new InvalidOperationException("Connection string 'WordleContextConnection' not found.");

builder.Services.AddDbContext<WordleContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WordleUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WordleContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<WeaklyReset>();
builder.Services.AddHostedService<DailyReset>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.Run();

