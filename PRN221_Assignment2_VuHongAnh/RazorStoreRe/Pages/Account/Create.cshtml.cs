using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BO;
using Services;

namespace RazorStoreRe.Pages.Account
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public CreateModel(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public IActionResult OnGet()
        {
            ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
            return Page();
        }

        [BindProperty]
        public BO.Account Account { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int maxId = 0;
            foreach (var account in _accountService.GetAllAccounts())
            {
                if (account.AccountId > maxId)
                {
                    maxId = account.AccountId;
                }
            }
            Account.AccountId = maxId + 1;
            //if(!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _accountService.AddAccount(Account);

            return RedirectToPage("./Index");
        }
    }
}
