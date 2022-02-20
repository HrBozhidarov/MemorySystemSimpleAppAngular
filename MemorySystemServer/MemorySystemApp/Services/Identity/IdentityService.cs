namespace MemorySystemApp.Services.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using MemorySystemApp.Data;
    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Models.Identity;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityService : IIdentityService
    {
        private const string DefaultProfileUrl = "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png";

        private readonly MemorySystemDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ApplicationSettings applicationSettings;

        public IdentityService(
            MemorySystemDbContext db,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<ApplicationSettings> options)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.applicationSettings = options.Value;
        }

        public async Task<Result<LoginModel>> Login(LoginUserRequestModel model)
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

        public async Task<Result<User>> Register(RegisterUserRequestModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            if (await this.userManager.FindByEmailAsync(model.Email) != null ||
                await this.userManager.FindByNameAsync(model.Username) != null)
            {
                return Result<User>.Error("Email or username already exist.");
            }

            if (!string.IsNullOrWhiteSpace(model.ProfileUrl) && !Uri.IsWellFormedUriString(model.ProfileUrl, UriKind.RelativeOrAbsolute))
            {
                return Result<User>.Error("Invalid profile url");
            }
            else if (string.IsNullOrWhiteSpace(model.ProfileUrl))
            {
                model.ProfileUrl = DefaultProfileUrl;
            }

            var isRoleExist = await this.roleManager.RoleExistsAsync(Constant.User);
            if (!isRoleExist)
            {
                var role = new Role
                {
                    Name = Constant.User,
                };

                await this.roleManager.CreateAsync(role);
            }

            var user = Mapper.Map<User>(model);

            var identityResult = await this.userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                return Result<User>.Error(identityResult.Errors.Select(e => e.Description).First());
            }

            await this.userManager.AddToRoleAsync(user, Constant.User);

            return Result<User>.Success(user);
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
