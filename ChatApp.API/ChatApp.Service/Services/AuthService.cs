using System;
using System.Text;
using ChatApp.Service.Data;
using ChatApp.Service.Tools;
using System.Security.Claims;
using ChatApp.Service.Helpers;
using ChatApp.Service.Interfaces;
using System.Collections.Generic;
using ChatApp.Service.Models.User;
using ChatApp.Service.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;

        public AuthService(IOptions<AppSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        public OperationResponse Login(LoginRequest request)
        {
            var user = _userService.Get(request.Email);
            if (user == null)
                return new OperationResponse() { Success = false, Message = "User not found" };

            bool isPasswordCorrect = SecurePasswordHasher.Verify(request.Password, user.password);
            if (!isPasswordCorrect)
                return new OperationResponse() { Success = false, Message = "Invalid password" };

            var token = GenerateJwtToken(user);
            return new OperationResponse { Success = true, Data = new Dictionary<string, object> { { "token", token } } };
        }

        public OperationResponse Register(RegisterRequest request)
        {
            var user = _userService.Get(request.Email);
            if (user != null)
                return new OperationResponse() { Success = false, Message = "This email address is already being used" };

            // create user
            var model = new UserModel
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var createUserResult = _userService.CreateOrEdit(model);
            if (createUserResult == null)
                return new OperationResponse() { Success = false, Message = "Something went wrong, please try again later" };

            var token = GenerateJwtToken(createUserResult);
            return new OperationResponse() { Success = true, Data = new Dictionary<string, object> { { "token", token } } };
        }

        private string GenerateJwtToken(user model)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("user_id", model.id.ToString()), new Claim("user_name", model.name) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}