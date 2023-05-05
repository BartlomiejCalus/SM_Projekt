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
                var entity = stat.UserStat.Where(u => u.points>0).ToList();
                entity.ForEach(u => u.points = 0);
                await stat.SaveChangesAsync();
            }
        }
    }
}
