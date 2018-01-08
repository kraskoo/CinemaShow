namespace CinemaShow.Application.Controllers
{
    using System.Web.Mvc;

    public class ErrorsController : Controller
    {
        public ActionResult MovedPermanently()
        {
            return this.View();
        }

        public ActionResult MovedTemporary()
        {
            return this.View();
        }

        public ActionResult BadRequest()
        {
            return this.View();
        }

        public ActionResult NotAuthorized()
        {
            return this.View();
        }

        public ActionResult NotFound()
        {
            return this.View();
        }

        public ActionResult InternalServerError()
        {
            return this.View();
        }
    }
}