using Microsoft.AspNetCore.Mvc;
using MobleFinalServer.Service;

namespace MobleFinalServer.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { 1 })]
    public class FireDetectController : Controller
	{
		public IActionResult Index()
		{
			string serial = HttpContext.Session.GetString("UserClient");
			ViewBag.Serial = serial;
            return View();
		}
	}
}