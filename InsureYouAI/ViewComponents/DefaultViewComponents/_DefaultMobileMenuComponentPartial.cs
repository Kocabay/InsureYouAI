using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultMobileMenuComponentPartial : ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultMobileMenuComponentPartial(InsureContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Email = _context.Contacts.Select(x => x.Email).FirstOrDefault();
            ViewBag.Phone = _context.Contacts.Select(x => x.Phone).FirstOrDefault();
            return View();
        }
    }
}
