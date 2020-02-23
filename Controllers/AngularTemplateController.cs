using Microsoft.AspNetCore.Mvc;

namespace esport.Controllers
{
    public class AngularTemplateController : Controller
    {
        public PartialViewResult RenderView(string name)
        {
            return PartialView($"~/app/templates/{name}");
        }
    }
}
