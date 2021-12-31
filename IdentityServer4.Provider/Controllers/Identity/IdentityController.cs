using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4_0.Controllers.Identity;

public class IdentityController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Json(new { claims = User.Claims.Select(c => new { type = c.Type, value = c.Value }) });
    }
}