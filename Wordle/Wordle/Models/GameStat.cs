using System.ComponentModel.DataAnnotations;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class GameStat
    {
        public GameStat(int statsId, DateTime quessTime, DateTime startTime, int tries, int points)
        {
            this.statsId = statsId;
            this.userId = "579bee2f-4c4e-4ace-9eb5-a4bcd639d673";
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
