using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wordle.Areas.Identity.Data;
using Wordle.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WordleContextConnection") ?? throw new InvalidOperationException("Connection string 'WordleContextConnection' not found.");

builder.Services.AddDbContext<WordleContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WordleUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<WordleContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

/*
 
 static async Task Main()
    {
        var client = new RestClient("https://random-word-api.herokuapp.com");
        var request = new RestRequest("/word?length=6", Method.Get);

        RestResponse restResponse = await client.GetAsync(request);
        string responseContent = restResponse.Content;

        JArray jsonArray = JArray.Parse(responseContent);
        string[] wordsArray = jsonArray.ToObject<string[]>();

        Console.WriteLine("Zwrócone s³owa:");
        foreach (string word in wordsArray)
        {
            Console.WriteLine(word);
        }

    }
 
 */
