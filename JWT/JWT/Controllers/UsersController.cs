using JWT.Models;
using JWT.Respository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IManagerJWTRepository _managerJWTRepository; 
        public UsersController(IManagerJWTRepository managerJWTRepository)
        {
            _managerJWTRepository = managerJWTRepository;
        }
        [HttpGet]
        public List<string> GetAll()
        {
            var listUsername = new List<string>
            {
                "Nguyên văn A",
                "Nguyên văn B",
                "Nguyên văn C",
            };
            return listUsername;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Account usersdata)
        {
            var token = _managerJWTRepository.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
