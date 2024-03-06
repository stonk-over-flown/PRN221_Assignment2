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
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public DetailsModel(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public BO.Account Account { get; set; } = default!;
        public string RoleName { get; set; } = "";

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
                RoleName = _roleService.GetRoleById(account.RoleId).RoleName;
                Account = account;
            }
            return Page();
        }
    }
}
