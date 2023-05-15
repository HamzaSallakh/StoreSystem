using Domain.BaseEntity;
using Domain.Model;
using Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public SignInManager<ApplicationUser> SignInManager { get; }
        public UserManager<ApplicationUser> UserManager { get; }
        public IJwtAuthentication<JwtAuthModel> Jwt { get; }

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtAuthentication<JwtAuthModel> jwt)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            Jwt = jwt;
        }
        [HttpPost("Register")]
        public async Task<StanderdJson> Register(RegisterModel model)
        {
            var Stan = new StanderdJson()
            {
                Code = BadRequest().StatusCode,
                Message = "Error",
                Data = new NullColumns(),
                Success = false
            };
            if (!ModelState.IsValid)
            {
                return Stan;
            }

            ApplicationUser users = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await UserManager.CreateAsync(users, model.Password);
            if (result.Succeeded)
            {
                Stan.Code = Ok().StatusCode;
                Stan.Message = "Register is success";
                Stan.Data = new NullColumns();
                Stan.Success = true;
                return Stan;
            }
            return Stan;

        }


        [HttpPost("Login")]
        public async Task<StanderdJson> Login(LoginModel model) {
            var Stan = new StanderdJson()
            {
                Code = BadRequest().StatusCode,
                Message = "Error",
                Data = new NullColumns(),
                Success = false
            };
            if (!ModelState.IsValid)
            {
                return Stan;
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var dataUser = await UserManager.FindByEmailAsync(model.Email);
                var JwtAuth = new JwtAuthModel
                {
                    Email = model.Email,
                    UserName = dataUser.UserName,
                    Id = dataUser.Id
                };
                var token = Jwt.GetJsonWebToken(JwtAuth);

                if (dataUser.RefreshToken.Any(x => x.IsActive))
                {
                    var activeRefreshToken = dataUser.RefreshToken.FirstOrDefault(x => x.IsActive);
                    JwtAuth.RefreshToken = activeRefreshToken.Token;
                    JwtAuth.RefreshTokenExpiration = activeRefreshToken.ExpiersOn;
                }
                else
                {
                    var RefreshToken = Jwt.GenerateRefreshToken();
                    JwtAuth.RefreshToken = RefreshToken.Token;
                    JwtAuth.RefreshTokenExpiration = RefreshToken.ExpiersOn;
                    dataUser.RefreshToken.Add(RefreshToken);
                    await UserManager.UpdateAsync(dataUser);
                }

                //if (!string.IsNullOrEmpty(JwtAuth.RefreshToken))
                //{
                //    SetRefreshToken(JwtAuth.RefreshToken, JwtAuth.RefreshTokenExpiration);
                //}

                var newData = new LoginResultModel { Token = token, UserName = dataUser.UserName, RefreshToken = JwtAuth.RefreshToken };
                Stan.Data = newData;
                Stan.Code = Ok().StatusCode;
                Stan.Success = true;
                Stan.Message = "Success";
                return Stan;

            }
            return Stan;


        }


        [HttpPost("Logout")]
        public async Task<StanderdJson> Logout()
        {
            var Stan = new StanderdJson()
            {
                Code = BadRequest().StatusCode,
                Message = "Error",
                Data = new NullColumns(),
                Success = false
            };
            if (!ModelState.IsValid)
            {
                return Stan;
            }
            // Get the current user's identity
            var user = await UserManager.GetUserAsync(User);

            // Clear the refresh tokens associated with the user
            if (user != null)
            {

                //var Token = new JwtSecurityToken(
                //audience: Configuration["Jwt:Audience"],
                //issuer: Configuration["Jwt:issuer"],
                //claims: claims,
                //expires: Convert.ToDateTime(DateTime.Now.AddSeconds(6000)),
                //signingCredentials: Credentials
                //);
                user.RefreshToken.Clear();
                await UserManager.UpdateAsync(user);
            }

            // Sign out the user
            await SignInManager.SignOutAsync();

            Stan.Code = Ok().StatusCode;
            Stan.Message = "logout is success";
            Stan.Data = new NullColumns();
            Stan.Success = true;
            return Stan;

        }

        [HttpPost("RefreshToken")]
        public async Task<StanderdJson> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var refreshToken = model.Token ?? Request.Cookies["RefreshToken"];
            var result = await RefreshTokenAsync(refreshToken);
            if (!result.IsAuthentication)
            {
                return new StanderdJson
                {
                    Code = BadRequest().StatusCode,
                    Message = BadRequest("Batata"),
                    Data = new NullColumns(),
                    Success = false
                };
            }

            return new StanderdJson
            {
                Code = Ok().StatusCode,
                Message = "",
                Data = new LoginResultModel
                {
                    Token = result.Token,
                    RefreshToken = result.RefreshToken,
                    UserName = result.UserName
                },
                Success = true
            };

            //SetRefreshToken(result.Token, result.RefreshTokenExpiration);



        }

        async Task<JwtAuthModel> RefreshTokenAsync(string Token)
        {
            var authModel = new JwtAuthModel();
            var result = await UserManager.Users.SingleOrDefaultAsync(x => x.RefreshToken.Any(t => t.Token == Token));
            if (result == null)
            {
                authModel.IsAuthentication = false;
                authModel.Massege = "Invalid Token";
                return authModel;
            }

            var refreshToken = result.RefreshToken.Single(x => x.Token == Token);
            if (!refreshToken.IsActive)
            {
                authModel.IsAuthentication = false;
                authModel.Massege = "Inactive Token";
                return authModel;

            }
            authModel.Email = result.Email;
            authModel.Id = result.Id;
            authModel.UserName = result.UserName;
            refreshToken.RevokeOn = DateTime.UtcNow;

            var newRefreshToken = Jwt.GenerateRefreshToken();
            result.RefreshToken.Add(newRefreshToken);
            await UserManager.UpdateAsync(result);

            var jwtToken = Jwt.GetJsonWebToken(authModel);
            authModel.IsAuthentication = true;
            authModel.Token = jwtToken;
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiersOn;
            return authModel;


        }

        void SetRefreshToken(string RefreshToken, DateTime expires)
        {
            var cookiOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };
            Response.Cookies.Append("RefreshToken", RefreshToken, cookiOption);
        }







    }
}
