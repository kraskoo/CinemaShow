namespace CinemaShow.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using CinemaShow.Models;

    public class SignInManager : SignInManager<User, string>
    {
        public SignInManager(
            UserManager<User, string> userManager,
            IAuthenticationManager authenticationManager) : base(
                userManager,
                authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }

        public static SignInManager Create(
            IdentityFactoryOptions<SignInManager> options,
            IOwinContext context)
        {
            return new SignInManager(
                context.GetUserManager<UserManager>(),
                context.Authentication);
        }
    }
}