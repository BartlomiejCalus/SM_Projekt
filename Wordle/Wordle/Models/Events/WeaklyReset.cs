using Microsoft.EntityFrameworkCore;
using Wordle.Controllers;
using Wordle.Data;

namespace Wordle.Models.Events
{
    public class WeaklyReset : BackgroundService
    {
        private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromDays(7) - DateTime.Now.TimeOfDay);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(await _periodicTimer.WaitForNextTickAsync(stoppingToken)&& !stoppingToken.IsCancellationRequested)
            {
                await Reset();
            }
        }

        private static async Task Reset()
        {
            using (var stat = new GameStatController().context)
            {
                var userToDelete = new List<UserStat>();
                userToDelete = stat.UserStat.ToList();
                foreach (var user in userToDelete)
                {
                    Console.WriteLine(user.userId.ToString());
                }
                stat.UserStat.RemoveRange(userToDelete);
                await stat.SaveChangesAsync();
            }
        }
    }
}
