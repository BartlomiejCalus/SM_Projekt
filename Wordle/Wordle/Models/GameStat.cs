using System.ComponentModel.DataAnnotations;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class GameStat
    {
        public GameStat(string userId, DateTime quessTime, DateTime startTime, int tries, int points)
        {
            this.userId = userId;
            this.quessTime = quessTime;
            this.startTime = startTime;
            this.tries = tries;
            this.points = points;
        }

        [Key]
        public int statsId { get; set; }
        [Required]
        public string userId { get; set; }
        
        public DateTime quessTime { get; set; }

        public DateTime startTime { get; set; }

        public int tries { get; set; }

        public int points { get; set; }

        public virtual WordleUser user { get; set; }
    }
}
