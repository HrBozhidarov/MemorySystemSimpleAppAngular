namespace MemorySystem.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using MemorySystem.Data;
    using MemorySystem.Data.Models;
    using MemorySystem.Services.Models;
    using MemorySystemApp;
    using MemorySystemApp.Models.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class AccountService : IAccountService
    {
        private readonly MemorySystemDbContext db;
        private readonly UserManager<User> userManager;
        private readonly ApplicationSettings applicationSettings;

        public AccountService(
            MemorySystemDbContext db,
            UserManager<User> userManager,
            IOptions<ApplicationSettings> options)
        {
            this.db = db;
            this.userManager = userManager;
            this.applicationSettings = options.Value;
        }

        public async Task<Result<LoginModel>> Login(LoginUserModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Result<LoginModel>.Error("Username or password are invalid");
            }

            var validationResult = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!validationResult)
            {
                return Result<LoginModel>.Error("Username or password are invalid");
            }

            return Result<LoginModel>.Success(
                new LoginModel
                {
                    ProfileUrl = user.ProfileUrl,
                    Token = this.GenerateJwtToken(user),
                    Role = (await this.db.UserRoles.Include(r => r.Role).FirstOrDefaultAsync(r => r.UserId == user.Id))?.Role?.Name,
                });
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
