using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthWithCookie.Pages.Account {
  [AllowAnonymous]
  public class LoginModel : PageModel {

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel {
      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
    }

    [TempData]
    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
      if (!ModelState.IsValid) return Page();

      var mockDB = new[] {
        (email: "nossa@example.com", password: "pazzword1"),
        (email: "nyaan@example.com", password: "pazzword2")
      };
      bool isValid = mockDB.Any(
        x => x.email == Input.Email &&
        x.password == Input.Password);

      if (!isValid) return Page();
      
      // 名前、電子メール アドレス、年齢、Sales ロールのメンバーシップなど、id 情報の一部
      Claim[] claims = {
        new Claim(ClaimTypes.NameIdentifier, Input.Email), // ユニークID
        new Claim(ClaimTypes.Name, Input.Email),
      };

      // 一意の ID 情報
      var claimsIdentity = new ClaimsIdentity(
        claims, CookieAuthenticationDefaults.AuthenticationScheme);

      // サインイン（ログイン）
      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),
        new AuthenticationProperties {
          // Cookie をブラウザー セッション間で永続化するか？（ブラウザを閉じてもログアウトしないかどうか）
          IsPersistent = Input.RememberMe
        });

      return LocalRedirect(returnUrl ?? Url.Content("~/"));
    }
  }
}