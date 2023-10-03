//using apiforapp.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace apiforapp.Api
//{
//    [Route("api/[controller]")]
//    public class RoleController : Controller
//    {
//        private readonly ApplicationDbcontext _db;
//        private readonly ILogger<RoleController> _logger;

//        public RoleController(ILogger<RoleController> logger, ApplicationDbcontext db)
//        {
//            _logger = logger;
//            _db = db;
//        }

//        [HttpGet]
//        public IActionResult GetRoles()
//        {
//            var roles = _db.Roles.ToList();
//            return Ok(roles);
//        }

//        [HttpPost]
//        public IActionResult CreateRole([FromBody] Role role)
//        {
//            if (role == null)
//            {
//                return NotFound();
//            }

//            _db.Roles.Add(role);
//            _db.SaveChanges();

//            return CreatedAtAction(nameof(GetRoleById), new { id = role.idRole }, role);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetRoleById(int id)
//        {
//            var role = _db.Roles.FirstOrDefault(r => r.idRole == id);

//            if (role == null)
//            {
//                return NotFound();
//            }

//            return Ok(role);
//        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateRole(int id, [FromBody] Role updatedRole)
//        {
//            if (updatedRole == null || updatedRole.idRole != id)
//            {
//                return BadRequest();
//            }

//            var existingRole = _db.Roles.FirstOrDefault(r => r.idRole == id);

//            if (existingRole == null)
//            {
//                return NotFound();
//            }

//            existingRole.name = updatedRole.name;
//            existingRole.description = updatedRole.description;

//            _db.SaveChanges();

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteRole(int id)
//        {
//            var role = _db.Roles.FirstOrDefault(r => r.idRole == id);

//            if (role == null)
//            {
//                return NotFound();
//            }

//            _db.Roles.Remove(role);
//            _db.SaveChanges();

//            return NoContent();
//        }
//    }
//}
