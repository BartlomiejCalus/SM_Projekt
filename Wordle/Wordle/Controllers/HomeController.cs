﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using Wordle.Models;
using Wordle.Models.Game;
using Wordle.Models.Punctation;
using System;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CodeActions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static Wordle.Models.ArrayRequest.WordsArray;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Wordle.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        punctation p1 = new punctation();
        private readonly IMemoryCache _memoryCache;
        
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public string GetUserNickname()
        {
            UserStatWithoutVirtual userStat1 = new UserStatWithoutVirtual();

            using (var stat = new GameStatController().context)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    string nick = stat.Users.FirstOrDefault(item => item.Id == userId).Nickname;
                    return nick;
                }
                catch
                {
                    return "Not logged in";
                }
            }
        }

        [HttpPost]
        public IActionResult Play(string generatedWord)
        {
            Ranked ranked = _memoryCache.Get<Ranked>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked");
            if(ranked == null)
            {
                ranked = new Ranked(_memoryCache);
                _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked", ranked, TimeSpan.FromMinutes(60));
            }
            var serverResponse = ranked.Play(generatedWord);
            return Json(serverResponse);
        }

  
        /// /////////////////
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult nextRoundCasual()
        {
            Casual casual = _memoryCache.Get<Casual>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "casual");
            int round = casual.nextRound();
            return Json(round);
        }

        //[AllowAnonymous]
        [HttpPost]
        public IActionResult PlayCasual(string generatedWord)
        {
            //Console.WriteLine("Słowo:", generatedWord);
            Casual casual = _memoryCache.Get<Casual>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "casual");
            if (casual == null)
            {
                casual = new Casual(_memoryCache);
                _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "casual", casual, TimeSpan.FromMinutes(60));
            }
            //Console.WriteLine("Słowo:", generatedWord);
            var serverResponseCasual = casual.Play(generatedWord);
            return Json(serverResponseCasual);
        }

        /// /////////////
        [HttpPost]
        public async Task<IActionResult> StartCasual()
        {
            p1.startTime();
            //var AllUsers = getUsersFromDB();
            //var OneUser = getUserFromDB();
            //var Description = await GetDescriptionAsync("hello");
            Casual casual = _memoryCache.Get<Casual>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "casual");
            if (casual == null)
            {
                casual = new Casual(_memoryCache);
                _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "casual", casual, TimeSpan.FromMinutes(60));
            }
            _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "p1", p1, TimeSpan.FromMinutes(60));
            return Ok();
        }

        [HttpGet]


        public IActionResult Rank()
        {
            TimeSpan ts1 = new TimeSpan(1, 30, 0); // 1 hour and 30 minutes
            TimeSpan ts2 = new TimeSpan(1, 30, 0); // 1 hour and 30 minutes
            List<UserStatWithoutVirtual> gracze = getUsersFromDB();
            //var gracze = new List<UserStat>() { };
            //string userId, int points, uint finishes,uint wins,uint checks,TimeSpan averagePlayTime,TimeSpan fastestWin
            return Json(gracze);
        }

        //DODANE DLA STATYSTYK
        [HttpGet]
      
        public IActionResult Stats()
        {
            var AllUsers = GetUserFromDB();
            var attemptsData = new int[] { 10, 20, 15, 8, 7 };
            var gracze2 = new List<UserStat2>() {

                new UserStat2("fasgag", 110, 65, "1m 10s", 5, attemptsData),
            };
            return Json(AllUsers);
        }
        //DOTĄD
        //DODANE DLA STATYSTYK
        public class UserStat2
        {
            public string UserName { get; set; }
            public int GamesPlayed { get; set; }
            public int WinPercentage { get; set; }
            public string FastestWin { get; set; }
            public int CurrentStreak { get; set; }
            public int[] Attempts { get; set; }

            public UserStat2(string userName, int gamesPlayed, int winPercentage, string fastestWin, int currentStreak, int[] attempts)
            {
                UserName = userName;
                GamesPlayed = gamesPlayed;
                WinPercentage = winPercentage;
                FastestWin = fastestWin;
                CurrentStreak = currentStreak;
                Attempts = attempts;
            }
        }
        //DOTĄD




    [HttpPost]
        public async Task<IActionResult> Start()
        {
            p1.startTime();
            //var AllUsers = getUsersFromDB();
            //var OneUser = getUserFromDB();
            //var Description = await GetDescriptionAsync("hello");
            Ranked ranked = _memoryCache.Get<Ranked>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked");
            if (ranked == null)
            {
                ranked = new Ranked(_memoryCache);
                _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked", ranked, TimeSpan.FromMinutes(60));
            }
            _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "p1", p1, TimeSpan.FromMinutes(60));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Dupa2(string word)
        {
            //Console.WriteLine(word);
            DictionaryApi dictionaryApi = new DictionaryApi();
            string definition = await dictionaryApi.GetDefinition(word);
            //Console.WriteLine(definition);
            return Json(definition);
        }
        public IActionResult Dupa(string row)
        {
            //Console.WriteLine(row);

            string result;
            DictionaryApi dictionaryApi = new DictionaryApi();
            Task<string> task = dictionaryApi.GetDefinition(row);
            task.Wait();
            result = task.Result;
            //Console.WriteLine(result);
            return Json(result);
        }

        [HttpPost]
        public IActionResult getWord()
        {
            Ranked ranked = _memoryCache.Get<Ranked>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked");
            return Json(ranked.wordInfo.word);
        }

        [HttpPost]
        public IActionResult getWordCasual()
        {
            Casual casual = _memoryCache.Get<Casual>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "casual");
            return Json(casual.wordInfo.word);
        }


        [HttpPost]
        public List<UserStatWithoutVirtual> getUsersFromDB() 
        {
            UserStatWithoutVirtual userStat = new UserStatWithoutVirtual();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using (var stat = new GameStatController().context)
              {
                var userStats = stat.UserStat.OrderByDescending(item => item.points).Take(100).ToList();

                List<UserStatWithoutVirtual> userStatsList = new List<UserStatWithoutVirtual>();

                    try
                    {
                        foreach (var item in userStats)
                        {
                        UserStatWithoutVirtual userStatWithoutVirtual = new UserStatWithoutVirtual

                            {
                                statsId = item.statsId,
                                nickname = stat.Users.FirstOrDefault(u => u.Id == item.userId).Nickname,
                                points = item.points,
                                finishes = item.finishes,
                                wins = item.wins,
                                checks = item.checks,
                                averagePlayTime = item.averagePlayTime,
                                fastestWin = item.fastestWin
                            };

                            userStatsList.Add(userStatWithoutVirtual);
                        }

                    }
                    catch
                    {
                        Console.WriteLine("Wystąpił błąd");
                    }   
                  

                  return userStatsList;
              }       
           
        }



        [HttpPost]
        public IActionResult nextRound()
        {
            Ranked ranked = _memoryCache.Get<Ranked>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked");
            int round = ranked.nextRound();
            ranked.saveCurrentRound(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked", ranked, TimeSpan.FromMinutes(60));
            return Json(round);
        }
        [HttpGet]
        public IActionResult getRound()
        {
            Ranked ranked = _memoryCache.Get<Ranked>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "ranked");
            return Json(ranked.getSavedRound(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [HttpPost]
        public List<UserStatWithoutVirtual> GetUserFromDB()
        {
            UserStatWithoutVirtual userStat1 = new UserStatWithoutVirtual();

            using (var stat = new GameStatController().context)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userStats = stat.UserStat.Where(item => item.userId == userId).ToList();

                List<UserStatWithoutVirtual> userStatsList = new List<UserStatWithoutVirtual>();

                foreach (var item in userStats)
                {
                    var userStatWithoutVirtual = new UserStatWithoutVirtual
                    {
                        statsId = item.statsId,
                        nickname = stat.Users.FirstOrDefault(u => u.Id == item.userId).Nickname,
                        points = item.points,
                        finishes = item.finishes,
                        wins = item.wins,
                        checks = item.checks,
                        averagePlayTime = item.averagePlayTime,
                        fastestWin = item.fastestWin
                    };

                    userStatsList.Add(userStatWithoutVirtual);
                }

                return userStatsList;
            }
        }

        [HttpPost]
        public IActionResult getTopFromDB()
        {

            using (var stat = new GameStatController().context)
            {

                var userStats = stat.TopPointsStat.OrderByDescending(item => item.points).Take(3).ToList();

                List<TopWithoutVirtual> topList = new List<TopWithoutVirtual>();

                foreach (var item in userStats)
                {
                    var topWithoutVirtual = new TopWithoutVirtual
                    {
                        topID = item.topID,
                        nickname = stat.Users.FirstOrDefault(u => u.Id == item.userID).Nickname,
                        points = item.points,
                    };

                    topList.Add(topWithoutVirtual);
                }

                return Json(topList);
            }
        }
        [HttpPost]
        public IActionResult End([FromBody]int row)
        {
            p1 = _memoryCache.Get<punctation>(User.FindFirstValue(ClaimTypes.NameIdentifier) + "p1");
            p1.endTime();;
            var points=p1.Stats(row);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            using (var stat = new GameStatController().context)
            {
                try
                {
                    var entity = stat.UserStat.First(a => a.userId == userId);
                    var p = points + entity.points;
                    if (row <= 5) entity.points = p;
                    entity.finishes = entity.finishes + 1;
                    if(row<=5) entity.wins = entity.wins + 1;
                    entity.checks =entity.checks+ (uint)row;
                    entity.averagePlayTime = (entity.averagePlayTime + p1.durationSpan) / entity.finishes;
                    if(p1.durationSpan<entity.fastestWin) entity.fastestWin = p1.durationSpan;
                    stat.SaveChanges();
                }
                catch(InvalidOperationException ex)
                {
                    UserStat userStat;
                    if (row <= 5)
                    {
                        userStat = new UserStat(userId, points, 1, 1, (uint)row, p1.durationSpan, p1.durationSpan);
                    }
                    else
                    {
                        userStat = new UserStat(userId, points, 1, 0, (uint)row, p1.durationSpan, p1.durationSpan);
                    }
                    stat.UserStat.Add(userStat);
                    try
                    {
                        stat.SaveChanges();
                    }
                    catch
                    {
                        Console.WriteLine("User already exists");
                    }
                }
            }
            _memoryCache.Remove(User.FindFirstValue(ClaimTypes.NameIdentifier) + "p1");
            return Json(points);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Ranking()
        {
            return View();
        }
        public IActionResult RankGame()
        {
            return View();
        }
        public IActionResult CasualGame()
        {
            return View();
        }
        public IActionResult Options()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}