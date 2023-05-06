using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class TopPointsStat
    {
        public TopPointsStat(string userID,int points) { 
        
            this.userID = userID;
            this.points = points;
        }

        [Key]
        public int topID { get; set; }
        [Required]
        public string userID { get; set; }

        public int points { get; set; }

        [JsonIgnore]
        public virtual WordleUser user { get; set; } = null!;

    }
}
