using API.Data;
using Library.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly MFADbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateController(MFADbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(e => e.UserName == request.UserName);
            if (user == null)
            {
                return NotFound();
            }
            if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Ok();
            }
            return Unauthorized();
        }


        [HttpPost("verify-login")]
        [AllowAnonymous]
        public ActionResult VerifyLogin([FromBody] VerifyLoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(e => e.UserName == request.UserName);
            if (user == null)
            {
                return NotFound();
            }
            if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password) && user.CodeExpireTime > DateTime.UtcNow && user.AuthenCode == request.Code)
            {
                user.Password = string.Empty;
                return Ok(user);
            }
            return Unauthorized();
        }

        public static Random Random = new Random();

        [HttpPost("generate-code")]
        [AllowAnonymous]
        public ActionResult GenerateCode([FromBody] string token)
        {
            var userName = VerifyToken(token);
            if (userName == null) return Unauthorized();
            var user = _context.Users.FirstOrDefault(e => e.UserName == userName);
            if (user == null)
            {
                return NotFound();
            }
            if (user.CodeExpireTime <= DateTime.UtcNow)
            {
                var code = Random.Next(0, 999999).ToString("D6");
                user.CodeExpireTime = DateTime.UtcNow.AddMinutes(1);
                user.AuthenCode = code;
                _context.SaveChanges();
            }
            return Ok(new CodeResponse(user.AuthenCode, user.CodeExpireTime));
        }

        [HttpPost("credential")]
        [Authorize]
        public ActionResult GenerateCredential(string userName)
        {
            var secretKey = _configuration["SecretKey"] ?? "secret_key";
            var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(userName));
            using HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var crypto = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var signature = new StringBuilder();
            foreach (byte theByte in crypto)
            {
                signature.Append(theByte.ToString("x2"));
            }
            var token = payload + '.' + signature;
            return Ok(token);
        }

        [HttpPost("verify-token")]
        [AllowAnonymous]
        public string? VerifyToken([FromBody] string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var splitToken = token.Split('.', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (splitToken.Length != 2) return null;

            var secretKey = _configuration["SecretKey"] ?? "secret_key";
            var payload = splitToken[0];

            var signature = splitToken[1];

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var crypto = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var hash = new StringBuilder();
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                if (hash.ToString() != signature) return null;
            }

            return Encoding.UTF8.GetString(Convert.FromBase64String(payload));
        }
    }
}
