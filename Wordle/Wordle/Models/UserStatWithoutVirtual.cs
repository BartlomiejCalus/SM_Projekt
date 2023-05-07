using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class UserStatWithoutVirtual
    {
        public UserStatWithoutVirtual() { }

        [Key]
        public int statsId { get; set; }
        [Required]
        public string userId { get; set; }

        public int points { get; set; }

        public uint finishes { get; set; }

        public uint wins { get; set; }

        public uint checks { get; set; }

        public TimeSpan averagePlayTime { get; set; }

        public TimeSpan fastestWin { get; set; }

    }
}
