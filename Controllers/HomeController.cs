using apiforapp.Models;
using apiforapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace apiforapp.Controllers
{
    public class HomeController : Controller

    {
        private readonly ApplicationDbcontext _db;
        private readonly ILogger<HomeController> _logger;

       


        public HomeController(ILogger<HomeController> logger,ApplicationDbcontext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}