
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace MuvekkilTakipSistemi.Components
{
	public class UserNameViewComponent : ViewComponent
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserNameViewComponent(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<string> InvokeAsync()
		{
			var userName = _httpContextAccessor.HttpContext.Session.GetString("Adsoyad");

			return userName;
		}

		
	}
}

