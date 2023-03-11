using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sales.WEB.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimous = new ClaimsIdentity();
            var sotoUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Jhonatan"),
                new Claim("LastName", "Soto"),
                new Claim(ClaimTypes.Name, "soto@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(sotoUser)));
        }
    }
}
