namespace CinemaShow.Application.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    using Data;
    using Services;

    public abstract class BaseController : Controller
    {
        private SignInManager signInManager;
        private UserManager userManager;
        private CinemaShowContext dataContext;

        protected BaseController()
        {
        }

        protected BaseController(
            SignInManager signInManager,
            UserManager userManager,
            CinemaShowContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dataContext = context;
        }

        public IAuthenticationManager AuthenticationManager =>
           this.HttpContext.GetOwinContext().Authentication;

        public CinemaShowContext CinemaShowDbContext
        {
            get => this.dataContext ?? this.HttpContext
                .GetOwinContext()
                .Get<CinemaShowContext>();

            private set => this.dataContext = value;
        }

        public UserManager UserManager
        {
            get => this.userManager ?? this.HttpContext
                .GetOwinContext()
                .Get<UserManager>();

            private set => this.userManager = value;
        }

        public SignInManager SignInManager
        {
            get => this.signInManager ?? this.HttpContext
                .GetOwinContext()
                .Get<SignInManager>();

            private set => this.signInManager = value;
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}