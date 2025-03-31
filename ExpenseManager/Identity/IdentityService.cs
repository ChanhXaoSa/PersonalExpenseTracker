using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Identity
{
    public class IdentityService(UserManager<ApplicationUser> userManager)
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                return await userManager.CheckPasswordAsync(user, password);
            }
            return false;
        }

        public async Task<bool> ValidatePinAsync(string username, string pin)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user is ApplicationUser appUser && appUser != null)
            {
                return appUser.Pin == pin;
            }
            return false;
        }
    }
}
