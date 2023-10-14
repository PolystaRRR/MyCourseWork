using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Himchistka.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string user)
    {

        if (string.IsNullOrEmpty(user))
        {
            ModelState.AddModelError("user", "Введите логин");
            return View();
        }
        

        await AuthAsync(user);
        return RedirectToAction("Index", "Home");
    }

    public ActionResult AccessDenied()
    {
        return View();
    }

    private async Task AuthAsync(string user)
    {
        List<Claim> claims = new();

        switch (user)
        {
            case "admin":
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, "qwer"));
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
                break;
            default:
                claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, "qwer"));
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"));
                break;
        }
        // создаем объект ClaimsIdentity
        ClaimsIdentity identity = new(claims, "Cookies", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        // установка аутентификационных куки
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));
    }
}