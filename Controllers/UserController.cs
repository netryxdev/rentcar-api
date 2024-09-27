using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rentcar_api.Data;
using rentcar_api.Models;
using rentcar_api.Models.DTOs;
using rentcar_api.Services.JWT;

namespace rentcar_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IJwtService _jwtService; // Injeção do IJwtService

        public UserController(AppDbContext appDbContext, IJwtService jwtService)
        {
            _db = appDbContext;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            // Verifica se o email já está registrado
            if (await _db.Users.AnyAsync(u => u.nm_email == registerDto.nm_email))
            {
                return BadRequest("Usuário já registrado com este e-mail.");
            }

            // Hash da senha
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.nm_senha);

            var newUser = new User
            {
                nm_user = registerDto.nm_user,
                nm_email = registerDto.nm_email,
                nm_senha = hashedPassword
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.nm_email == loginDto.nm_email);
            if (user == null)
            {
                return Unauthorized("Usuário ou senha incorretos.");
            }

            // Verifica se a senha é válida
            if (!BCrypt.Net.BCrypt.Verify(loginDto.nm_senha, user.nm_senha))
            {
                return Unauthorized("Usuário ou senha incorretos.");
            }

            // Gera o token JWT
            var token = _jwtService.GenerateToken(user);

            return Ok(new { Token = token });
        }

    }
}
