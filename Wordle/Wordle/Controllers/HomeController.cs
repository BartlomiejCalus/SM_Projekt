﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using Wordle.Models;
using Wordle.Models.Game;
using Wordle.Models.Punctation;
using System;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CodeActions;

namespace Wordle.Controllers
{
    public class HomeController : Controller
    {
        punctation p1 = new punctation();
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public IActionResult Play(string generatedWord)
        {
            Ranked ranked = new Ranked(_memoryCache);
            var serverResponse = ranked.Play(generatedWord);
            //Console.WriteLine("GITARA");
            return Json(serverResponse);
        }

        [HttpPost]
        public IActionResult Start()
        {
            p1.startTime();
            _memoryCache.Set(User.FindFirstValue(ClaimTypes.NameIdentifier) + "p1", p1, TimeSpan.FromMinutes(60));
            return Ok();
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
                    entity.points = p;
                    entity.finishes = entity.finishes + 1;
                    if(row<=5) entity.wins = entity.wins + 1;
                    entity.checks = (uint)row;
                    entity.averagePlayTime = (entity.averagePlayTime + p1.durationSpan) / entity.finishes;
                    if(p1.durationSpan<entity.fastestWin) entity.fastestWin = p1.durationSpan;
                    stat.SaveChanges();
                }
                catch(InvalidOperationException ex)
                {
                    UserStat userStat;
                    if (row <= 5)
                    {
                        userStat = new UserStat(userId, points, 1, 1, (uint)row+1, p1.durationSpan, p1.durationSpan);
                    }
                    else
                    {
                        userStat = new UserStat(userId, points, 1, 0, (uint)row+1, p1.durationSpan, p1.durationSpan);
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