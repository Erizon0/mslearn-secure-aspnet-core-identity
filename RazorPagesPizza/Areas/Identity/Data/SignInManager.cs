using Microsoft.AspNetCore.Identity;
using System.DirectoryServices.AccountManagement;

namespace RazorPagesPizza.Areas.Identity.Data;

public static class SignInManagerExtentions {

    public static async Task<SignInResult> CheckLdap(this SignInManager<RazorPagesPizzaUser> sim, RazorPagesPizzaUser user, string password) {
        if (user == null) {
            throw new ArgumentNullException(nameof(user));
        }

        if (password == null) {
            throw new ArgumentNullException(nameof(password));
        }

        bool isAllowed = await sim.UserManager.IsAllowed(user);
        if (isAllowed) {
            var confirm = await LdapAuth(user, password);
            await sim.SignInAsync(user, isPersistent: false);
            return confirm ? SignInResult.Success : SignInResult.Failed;
        }
        return SignInResult.NotAllowed;
    }
    

    public static async Task<bool> LdapAuth(RazorPagesPizzaUser user, string password) {
        try {
            using PrincipalContext pc       = new(ContextType.Domain, "tummers.lan");
            string                 userName = user.FirstName + "." + user.LastName; 
            UserPrincipal          up       = UserPrincipal.FindByIdentity(pc, userName);

            return up != null;
        }
        catch (Exception ex) {

        }

        return false;
    }
}