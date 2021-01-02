using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Wsei.ExchangeThings.Web.Models;

namespace Wsei.ExchangeThings.Web.Auth
{
    public interface IAuthService
    {
        AuthResult Authenticate(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private static readonly IReadOnlyList<UserModel> Users = new List<UserModel>
        {
            new UserModel {Id = 1, Username = "john", Password = "password1"},
            new UserModel {Id = 2, Username = "rob", Password = "secure"},
        };

        private readonly IHttpContextAccessor _httpContext;

        public AuthService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public AuthResult Authenticate(string username, string password)
        {
            var user = GetUser(username, password);

            if (user == null)
                return AuthResult.Failure;

            AuthenticateHttpResponse(user);
            return AuthResult.Success;
        }

        private UserModel GetUser(string username, string password)
        {
            return Users.FirstOrDefault(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase) &&
                                             x.Password.Equals(password));
        }

        private void AuthenticateHttpResponse(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("id", user.Id.ToString())
            };
            var identities = new List<ClaimsIdentity> { new ClaimsIdentity(claims, "custom") };

            _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identities));
        }
    }

    public enum AuthResult
    {
        Success,
        Failure,
    }
}