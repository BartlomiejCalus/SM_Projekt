using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Xml.Linq;
using Wordle.Data;

namespace Wordle.Controllers
{
    public class GameStatController : Controller
    {
        public WordleContext context;

        public GameStatController()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<WordleContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("WordleContextConnection"));
            context = new WordleContext(optionsBuilder.Options);
        }

    }
}
