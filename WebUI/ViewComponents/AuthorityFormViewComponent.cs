using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
	public class AuthorityFormViewComponent:ViewComponent
	{
		public IViewComponentResult Invoke(AuthorityModel model)
		{
			var values = new AuthorityModel()
			{

			};

			return View(values);
		}
	}
}
