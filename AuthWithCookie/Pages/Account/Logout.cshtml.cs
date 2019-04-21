using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace AuthWithCookie.Pages.Account {
  public class LogoutModel : PageModel {
    public async Task<LocalRedirectResult> OnPostAsync() {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return LocalRedirect(Url.Content("~/"));
    }
  }
}