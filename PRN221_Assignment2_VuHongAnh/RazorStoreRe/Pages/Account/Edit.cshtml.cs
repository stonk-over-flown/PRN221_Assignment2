using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BO;
using Services;

namespace RazorStoreRe.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public EditModel(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
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
            Account = account;
            ViewData["RoleId"] = new SelectList(_roleService.GetAllRoles(), "RoleId", "RoleName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                _accountService.UpdateAccount(Account);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.AccountId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AccountExists(int id)
        {
            return (_accountService.GetAccountById(id) != null);
        }
    }
}
