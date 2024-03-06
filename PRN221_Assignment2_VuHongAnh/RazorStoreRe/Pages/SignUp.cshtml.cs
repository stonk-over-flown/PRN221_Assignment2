using BO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace RazorStoreRe.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<SignUpModel> _logger;
        private readonly IAccountService _accountService;

        public SignUpModel(ILogger<SignUpModel> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSignUp(string fullname, string email, string password, string confirmpassword, string phonenumber, string address)
        {
            bool check = _accountService.GetAllAccounts().Any(x => x.AccountEmail == email);
            if (check)
            {
                HttpContext.Session.SetString("emailerror", "Email already exists");
                return RedirectToPage("/SignUp");
            }
            if (password != confirmpassword)
            {
                HttpContext.Session.SetString("passworderror", "Password and confirm password do not match");
                return RedirectToPage("/SignUp");
            }
            if (phonenumber.Length < 10 || phonenumber.Length > 11)
            {
                HttpContext.Session.SetString("phonenumbererror", "Phone number must be 10 or 11 digits");
                return RedirectToPage("/SignUp");
            }
            BO.Account account = new BO.Account
            {
                FullName = fullname,
                DateOfBirth = DateTime.Now,
                AccountEmail = email,
                AccountPassword = password,
                PhoneNumber = phonenumber,
                HomeAddress = address,
                AccountStatus = true,
                RoleId = 3
            };
            if (_accountService.AddAccount(account))
            {
                HttpContext.Session.SetInt32("role", account.RoleId);
                HttpContext.Session.SetString("fullname", account.FullName);
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Error");
        }
    }
}
