using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class UserStat
    {
        public UserStat(string userId, int points, uint finishes,uint wins,uint checks,TimeSpan averagePlayTime,TimeSpan fastestWin)
        {
            this.userId = userId;
            this.points = points;
            this.finishes = finishes;
            this.wins = wins;
            this.checks = checks;
            this.averagePlayTime = averagePlayTime;
            this.fastestWin = fastestWin;
        }
        public UserStat() { }

        [Key]
        public int statsId { get; set; }
        [Required]
        public string userId { get; set; }

        public int points { get; set; }

        public uint finishes { get; set; }

        public uint wins { get; set; }

        public uint checks { get; set; }

        public int TodayPlays { get; set; }

        public TimeSpan averagePlayTime { get; set; }

        public TimeSpan fastestWin { get; set; }

        [JsonIgnore]
        public virtual WordleUser user { get; set; } = null!;
    }
}
