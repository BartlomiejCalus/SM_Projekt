using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wordle.Models;

namespace Wordle.Areas.Identity.Data;

// Add profile data for application users by adding properties to the WordleUser class
public class WordleUser : IdentityUser
{
    public WordleUser()
    {
        GameStats = new List<GameStat>();
    }

    public string Nickname { get; set; }

    public virtual ICollection<GameStat> GameStats { get; set; }

    public UserStat? UserStat { get; set; }
}

