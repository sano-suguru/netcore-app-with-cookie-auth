﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthWithCookie.Pages {
  [Authorize]
  public class ContactModel : PageModel {
    public string Message { get; set; }

    public void OnGet() => Message = "Your contact page.";
  }
}
