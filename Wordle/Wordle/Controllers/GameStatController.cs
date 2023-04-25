using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Wordle.Data;

namespace Wordle.Controllers
{
    public class GameStatController : Controller
    {
        public WordleContext context;
        public GameStatController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WordleContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wordle;Trusted_Connection=True;MultipleActiveResultSets=true");
            context = new WordleContext(optionsBuilder.Options);
        }

    }
}
