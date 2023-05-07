using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wordle.Areas.Identity.Data;

namespace Wordle.Models
{
    public class TopWithoutVirtual
    {
        public TopWithoutVirtual() { }

        [Key]
        public int topID { get; set; }
        [Required]
        public string nickname { get; set; }

        public int points { get; set; }
    }
}
