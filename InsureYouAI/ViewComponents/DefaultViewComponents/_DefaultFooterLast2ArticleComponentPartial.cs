using InsureYouAI.Context;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.ViewComponents.DefaultViewComponents
{
    public class _DefaultFooterLast2ArticleComponentPartial:ViewComponent
    {
        private readonly InsureContext _context;

        public _DefaultFooterLast2ArticleComponentPartial(InsureContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var articles = _context.Articles.OrderByDescending(x => x.ArticleId).Skip(3).Take(2).ToList();
            return View(articles);
        }
    }
}
