using System.ComponentModel.DataAnnotations;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class UserStat
    {
        public UserStat(string userId, int points)
        {
            this.userId = userId;
            this.points = points;
        }

        [Key]
        public int statsId { get; set; }
        [Required]
        public string userId { get; set; }

        public int points { get; set; }

        public virtual WordleUser user { get; set; } = null!;
    }
}
