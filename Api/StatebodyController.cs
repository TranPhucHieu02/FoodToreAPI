//using apiforapp.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace apiforapp.Api
//{
//    [Route("api/[controller]")]
//    public class StatebodyController : Controller
//    {
//        private readonly ApplicationDbcontext _db;
//        private readonly ILogger<StatebodyController> _logger;

//        public StatebodyController(ILogger<StatebodyController> logger, ApplicationDbcontext db)
//        {
//            _logger = logger;
//            _db = db;
//        }
//        [HttpGet]
//        public IActionResult GetStatebodies()
//        {
//            var statebodies = _db.Statebody.ToList();
//            return Ok(statebodies);
//        }

//        [HttpPost]
//        public IActionResult CreateStatebody([FromBody] Statebody statebody)
//        {
//            if (statebody == null)
//            {
//                return BadRequest();
//            }

//            _db.Statebody.Add(statebody);
//            _db.SaveChanges();

//            return CreatedAtAction(nameof(GetStatebodyById), new { id = statebody.idStatebody }, statebody);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetStatebodyById(int id)
//        {
//            var statebody = _db.Statebody.FirstOrDefault(s => s.idStatebody == id);

//            if (statebody == null)
//            {
//                return NotFound();
//            }

//            return Ok(statebody);
//        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateStatebody(int id, [FromBody] Statebody updatedStatebody)
//        {
//            if (updatedStatebody == null || updatedStatebody.idStatebody != id)
//            {
//                return BadRequest();
//            }

//            var existingStatebody = _db.Statebody.FirstOrDefault(s => s.idStatebody == id);

//            if (existingStatebody == null)
//            {
//                return NotFound();
//            }

//            existingStatebody.name = updatedStatebody.name;
//            existingStatebody.description = updatedStatebody.description;

//            _db.SaveChanges();

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteStatebody(int id)
//        {
//            var statebody = _db.Statebody.FirstOrDefault(s => s.idStatebody == id);

//            if (statebody == null)
//            {
//                return NotFound();
//            }

//            _db.Statebody.Remove(statebody);
//            _db.SaveChanges();

//            return NoContent();
//        }
//    }
//}
