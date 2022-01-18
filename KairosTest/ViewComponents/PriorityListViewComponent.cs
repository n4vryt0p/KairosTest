using Microsoft.AspNetCore.Mvc;

namespace KairosTest.ViewComponents
{
    public class PriorityListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
