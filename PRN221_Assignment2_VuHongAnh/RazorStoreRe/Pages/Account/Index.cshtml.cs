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
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public IndexModel(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public IList<BO.Account> Account { get;set; } = default!;
        public List<string> RoleName { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            if (_accountService.GetAllAccounts() != null)
            {
                Account = _accountService.GetAllAccounts();
                foreach (BO.Account account in Account)
                {
                    RoleName.Add(_roleService.GetRoleById(account.RoleId).RoleName);
                }
            }
        }
    }
}
