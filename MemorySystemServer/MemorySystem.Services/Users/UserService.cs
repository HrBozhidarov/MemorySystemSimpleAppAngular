namespace MemorySystem.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using MemorySystem.Data.Models;
    using MemorySystem.Services.Models;
    using MemorySystemApp;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private const string DefaultProfileUrl = "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png";

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Result> Register(RegisterUserModel model)
        {
            var errorResult = await this.ValidateRegisterModelAsync(model);
            if (errorResult.IfHasError)
            {
                return Result.Error(errorResult.ErrorMessage);
            }

            await this.CreateUserRoleIfDoesNotExistsAsync();

            model.ProfileUrl = string.IsNullOrWhiteSpace(model.ProfileUrl) ? DefaultProfileUrl : model.ProfileUrl;
            var user = Mapper.Map<User>(model);

            var identityResult = await this.userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                return Result.Error(identityResult.Errors.Select(e => e.Description).First());
            }

            await this.userManager.AddToRoleAsync(user, Constant.User);

            return Result.Success;
        }

        private async Task<Result> ValidateRegisterModelAsync(RegisterUserModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            if (await this.userManager.FindByEmailAsync(model.Email) != null ||
                await this.userManager.FindByNameAsync(model.Username) != null)
            {
                return Result.Error("Email or username already exist.");
            }

            if (!string.IsNullOrWhiteSpace(model.ProfileUrl) && !Uri.IsWellFormedUriString(model.ProfileUrl, UriKind.RelativeOrAbsolute))
            {
                return Result.Error("Invalid profile url");
            }

            return Result.Success;
        }

        private async Task CreateUserRoleIfDoesNotExistsAsync()
        {
            var isRoleExist = await this.roleManager.RoleExistsAsync(Constant.User);
            if (!isRoleExist)
            {
                var role = new Role
                {
                    Name = Constant.User,
                };

                await this.roleManager.CreateAsync(role);
            }
        }
    }
}
