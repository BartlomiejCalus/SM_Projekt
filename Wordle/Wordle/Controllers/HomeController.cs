﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using Wordle.Models;
using Wordle.Models.Game;
using Wordle.Models.Punctation;
using System;

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
            return Ok();
        }

        [HttpPost]
        public IActionResult End([FromBody]int row)
        {
            p1.endTime();
            p1.Stats(row);
            Console.WriteLine(row);
            return Ok();
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