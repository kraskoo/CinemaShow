namespace CinemaShow.Application.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Data;
    using Models;
    using Models.ViewModels.Account;
    using Services;

    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        public AccountController(
            SignInManager signInManager,
            UserManager userManager,
            CinemaShowContext context) : base(
                signInManager,
                userManager,
                context)
        {
        }

        public ActionResult Register()
        {
            return this.View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var role = this.CinemaShowDbContext.Users.Any() ? "Regular" : "Admin";
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = $"{model.FirstName} {model.LastName}",
                    Email = model.Email,
                    PasswordHash = new PasswordHasher().HashPassword(model.Password)
                };

                var result = await this.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    this.UserManager.AddToRole(user.Id, role);
                    await this.SignInManager.SignInAsync(user, false, false);
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return this.RedirectToAction("Index", "Home");
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View(
                new LoginViewModel
                {
                    AuthenticationDescriptions = this.AuthenticationManager.GetExternalAuthenticationTypes()
                });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                this.ModelState.AddModelError(string.Empty, @"Invalid input");
                return this.View(model);
            }

            model.AuthenticationDescriptions = this.AuthenticationManager.GetExternalAuthenticationTypes();
            var userByEmail = this.UserManager.FindByEmail(model.Email);
            if (userByEmail == null)
            {
                this.ModelState.AddModelError(string.Empty, @"Invalid input");
                return this.View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await this.SignInManager.PasswordSignInAsync(
                userByEmail.UserName,
                model.Password,
                model.RememberMe,
                shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.RequiresVerification:
                    return this.RedirectToAction(
                        "SendCode",
                        new
                        {
                            ReturnUrl = returnUrl,
                            model.RememberMe
                        });
                case SignInStatus.Failure:
                default:
                    this.ModelState.AddModelError(string.Empty, @"Invalid input");
                    return this.View(model);
            }
        }
    }
}