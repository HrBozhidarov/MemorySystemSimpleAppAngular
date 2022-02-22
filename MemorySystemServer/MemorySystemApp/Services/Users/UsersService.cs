﻿namespace MemorySystemApp.Services.Users
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Models.Users;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUserService
    {
        private const string DefaultProfileUrl = "https://cdn1.iconfinder.com/data/icons/technology-devices-2/100/Profile-512.png";

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UsersService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<Result<User>> Register(RegisterUserRequestModel model)
        {
            var errorResult = await this.ValidateRegisterModelAsync(model);
            if (errorResult.IfHaveError)
            {
                return Result<User>.Error(errorResult.ErrorMessage);
            }

            await this.CreateUserRoleIfDoesNotExistsAsync();

            model.ProfileUrl = string.IsNullOrWhiteSpace(model.ProfileUrl) ? DefaultProfileUrl : model.ProfileUrl;
            var user = Mapper.Map<User>(model);

            var identityResult = await this.userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                return Result<User>.Error(identityResult.Errors.Select(e => e.Description).First());
            }

            await this.userManager.AddToRoleAsync(user, Constant.User);

            return Result<User>.Success(user);
        }

        private async Task<Result> ValidateRegisterModelAsync(RegisterUserRequestModel model)
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
