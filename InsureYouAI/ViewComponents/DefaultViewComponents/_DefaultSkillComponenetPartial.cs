using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponenetPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
