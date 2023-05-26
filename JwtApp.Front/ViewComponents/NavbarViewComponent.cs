using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Back.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
