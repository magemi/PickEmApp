using App.Db.Identity;
using App.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Web.Controllers
{
  [Route("[controller]/[action]")]
  public class AccountController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(string returnUrl = null)
    {
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      ViewData["ReturnUrl"] = returnUrl;

      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(LoginViewModel model, string returnUrl = null)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      ViewData["ReturnUrl"] = returnUrl;

      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
      if (result.Succeeded)
      {
        return RedirectToLocal(returnUrl);
      }

      ModelState.AddModelError(string.Empty, "Invalid login attempt.");
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> SignOut()
    {
      await _signInManager.SignOutAsync();

      return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
      if (ModelState.IsValid)
      {
        var user = new AppUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          await _signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToLocal(returnUrl);
        }
        AddErrors(result);
      }
      // If we got this far, something failed, redisplay form
      return View(model);
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToAction(nameof(HomeController.Index), "Home");
      }
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error.Description);
      }
    }
  }
}