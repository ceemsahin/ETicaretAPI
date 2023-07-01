﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.DTOs.Facebook;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IUserService _userService;
        readonly IMailService _mailService;

        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };

                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;

                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogin

                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);


                return token;


            }
            throw new Exception("Invalid external authentication");


        }





        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
        }

        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {

            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");




            FacebookAccessTokenResponse_DTO? facebookAccessTokenResponse_DTO = JsonSerializer.Deserialize<FacebookAccessTokenResponse_DTO>(accessTokenResponse);


            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse_DTO?.AccessToken}");


            FacebookUserAccessTokenValidation_DTO? facebookUserAccessTokenValidation_DTO = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation_DTO>(userAccessTokenValidation);



            if (facebookUserAccessTokenValidation_DTO?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");


                FacebookUserInfoResponse_DTO? userInfoResponse_DTO = JsonSerializer.Deserialize<FacebookUserInfoResponse_DTO>(userInfoResponse);


                var info = new UserLoginInfo("FACEBOOK", facebookUserAccessTokenValidation_DTO.Data.UserId, "FACEBOOK");



                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


                return await CreateUserExternalAsync(user, userInfoResponse_DTO.Email, userInfoResponse_DTO.Name, info, accessTokenLifeTime);

            }
            throw new Exception("Invalid external authentication");
        }
        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] },

            };


            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);


            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");



            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


            return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);




        }






        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {

            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
            {
                throw new NotFoundUserException()
                    ;
            }


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);


            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);

                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);

                return token;

            }
            else

                throw new AuthenticationErrorException();




        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {

            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);


            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(1, user);

                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 10);

                return token;
            }

            else
                throw new NotFoundUserException();

        }

        public async Task PasswordResetAsync(string email)
        {

            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //  byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                //resetToken=  WebEncoders.Base64UrlEncode(tokenBytes);
                resetToken = resetToken.UrlEncode();


                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);



            }




        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);

                //resetToken = Encoding.UTF8.GetString(tokenBytes);

                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);

            }

            return false;
        }
    }
}