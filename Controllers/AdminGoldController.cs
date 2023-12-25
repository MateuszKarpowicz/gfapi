using Microsoft.AspNetCore.Mvc;
using GFapi.Models;
using GFapi.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GFapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        // Metoda do logowania
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == loginRequest.Username);

            // Tu porównujemy surowe hasło z zahashowanym hasłem
            if (admin != null && VerifyPasswordHash(loginRequest.Password, admin.PasswordHash))
            {
                return Ok(new { message = "Login successful" });
            }

            return Unauthorized();
        }

            // Metoda do dodawania nowego admina
          [HttpPost("register")]
           public async Task<IActionResult> Register([FromBody] AdminGold newAdmin)
         {
        newAdmin.PasswordHash = HashPassword(newAdmin.PasswordHash); // Hashowanie hasła
        _context.Admins.Add(newAdmin);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Admin created" });
         }



        // Metody pomocnicze
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == storedHash;
        }
    }
}
