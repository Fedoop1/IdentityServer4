using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4._0.WEB.Client.Controllers
{
    public class ApiController : Controller
    {
        public async Task<IActionResult> CallApi()
        {
            var client = new HttpClient();
            var access_token = await HttpContext.GetTokenAsync("access_token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            var content = await client.GetStringAsync("https://localhost:6001/identity");

            return View(content);
        }
    }
}
