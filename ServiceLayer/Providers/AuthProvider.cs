using Microsoft.Owin.Security.OAuth;
using RepositoryLayer.Contexts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServiceLayer.Providers
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly SystemContext _context = new SystemContext();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var user = _context.Users.FirstOrDefault(u => u.UserName == context.UserName && u.UserPassword == context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            else
            {
                identity.AddClaim(new Claim("username", user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                context.Validated(identity);
            }
        }
    }
}