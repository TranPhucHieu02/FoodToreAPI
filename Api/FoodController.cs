using apiforapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiforapp.Api
{
    [Route("api/[controller]")]
    public class FoodController : Controller
    {
        private readonly ApplicationDbcontext _db;
        private readonly ILogger<HealthController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FoodController(ILogger<HealthController> logger, ApplicationDbcontext db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("GetAllFoods")]
        public IActionResult GetAllFoods()
        {
            List<Food> lsFoods = new List<Food>();
            lsFoods = _db.Foods.ToList();
            return Ok(lsFoods);
        }
        [HttpGet("GetFoodsToPage")]
        public IActionResult GetFoodsToPage(int page) {
            int startingIndex;
            int numberOfItemsToTake = 10;

            if (page == 1)
            {
               startingIndex = page-1;

            }
            else
            {
                startingIndex=page*20;
            }

            var lsFoods = _db.Foods.Skip(startingIndex).Take(numberOfItemsToTake).ToList();

            return Ok(lsFoods);
        }
        [HttpGet("Search")]
        public IActionResult SearchFoods(string search)
        {
            List<Food> lsFoods = new List<Food>();
            lsFoods = _db.Foods.Where(p=>p.Name.Contains(search)==true).ToList();
            return Ok(lsFoods);
        }
    }
}
