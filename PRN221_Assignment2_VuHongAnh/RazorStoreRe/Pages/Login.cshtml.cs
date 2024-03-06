using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace RazorStoreRe.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IAccountService _accountService;

        public LoginModel(ILogger<LoginModel> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(string email, string password)
        {
            var storeAccount = _accountService.CheckLogin(email,password);
            if (storeAccount != null)
            {
                HttpContext.Session.SetInt32("role", storeAccount.RoleId);
                HttpContext.Session.SetString("fullname", storeAccount.FullName);
                switch (storeAccount.RoleId)
                {
                    case 1:
                        return RedirectToPage("/Index");
                    case 2:
                        return RedirectToPage("/Index");
                    default:
                        return RedirectToPage("/Error");
                }
            }
            else
            {
                HttpContext.Session.SetString("errorlogin", "Invalid email or password");
                return RedirectToPage("/Login");
            }
        }
    }
}
