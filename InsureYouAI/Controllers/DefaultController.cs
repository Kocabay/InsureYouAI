using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SendMessage(string message)
        {
            return PartialView();
        }

        public PartialViewResult SubscribeEmail()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SubscribeEmail(string email)
        {
            return PartialView();
        }
    }
}
