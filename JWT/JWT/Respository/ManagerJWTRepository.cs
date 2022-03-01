using JWT.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Respository
{
    public class ManagerJWTRepository : IManagerJWTRepository
    {
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
        {
            { "user1","password1"},
            { "user2","password2"},
            { "user3","password3"},
        };
        public IConfiguration _configuration { get; set; }
        public ManagerJWTRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Tokens Authenticate(Account account)
        {
            if (!UsersRecords.Any(x => x.Key == account.Username && x.Value == account.Password))
            {
                return null;
            }
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]); // get key token in app setting 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                     new Claim(ClaimTypes.Name, account.Username)
                  }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature) // key được mã hóa bằng Hmách256
            };
            var tokenHandler = new JwtSecurityTokenHandler(); // tạo ra 1 Handler jwt 
            var token = tokenHandler.CreateToken(tokenDescriptor); // tạo token với mô tả như trên 
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
