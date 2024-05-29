﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly db_urunTakipContext _db_uruntakipContext;
        public HomeController(
            ILogger<HomeController> logger,
            db_urunTakipContext db_uruntakipContext)
        {
            _logger = logger;
            this._db_uruntakipContext = db_uruntakipContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
