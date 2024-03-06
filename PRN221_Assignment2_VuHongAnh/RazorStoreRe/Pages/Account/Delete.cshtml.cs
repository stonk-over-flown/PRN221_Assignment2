using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BO;
using Services;

namespace RazorStoreRe.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DeleteModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public BO.Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _accountService.GetAllAccounts() == null)
            {
                return NotFound();
            }

            var account = _accountService.GetAccountById(id);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                Account = account;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _accountService.GetAllAccounts() == null)
            {
                return NotFound();
            }
            var account = _accountService.GetAccountById(id);

            if (account != null)
            {
                Account = account;
                _accountService.DeleteAccount(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
