using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SatisStokTakipAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SatisStokTakipContext _context;

        public LoginController(SatisStokTakipContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(UserLoginDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == model.PasswordHash);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
