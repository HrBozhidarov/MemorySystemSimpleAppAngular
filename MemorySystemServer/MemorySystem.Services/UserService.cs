namespace MemorySystem.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;
    using AutoMapper;
    using MemorySystem.Data;
    using MemorySystem.Data.Models;
    using MemorySystem.Infrastructure.AutomapperSettings;
    using MemorySystem.Services.Models;
    using MemorySystemApp;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private const string DefaultProfileUrl = "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png";

        private readonly MemorySystemDbContext db;

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, MemorySystemDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        public async Task<Result<UserModel>> ProfileAsync(string userId)
        {
            var user = await this.db.Users.Where(u => u.Id == userId).To<UserModel>().FirstOrDefaultAsync();
            if (user == null)
            {
                return Result<UserModel>.Error($"User with id {userId} not found!");
            }

            return Result<UserModel>.Success(user);
        }

        // Only for testing scenario
        public async Task<Result> EditProfileAsync(string userId, UserModel model)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result.Error($"User with id {userId} not found!");
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, model.Password);

                user.Email = model.Email;
                user.UserName = model.Username;
                user.ProfileUrl = model.ProfileUrl;

                await this.userManager.UpdateAsync(user);

                scope.Complete();
            }
            catch
            {
                scope.Dispose();

                return Result.Error("Something wrong!");
            }

            return Result.Success;
        }

        public async Task<Result> CreateProfileAsync(UserModel model)
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

        private async Task<Result> ValidateRegisterModelAsync(UserModel model)
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
