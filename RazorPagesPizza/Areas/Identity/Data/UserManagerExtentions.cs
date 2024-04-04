using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesPizza.Areas.Identity.Data;

public static class UserManagerExtentions {

    public static async Task<RazorPagesPizzaUser> FindByUidAsync(this UserManager<RazorPagesPizzaUser> um, string uid) {
        return await um.Users?.SingleOrDefaultAsync(x => x.Uid == uid);
    }
    
    public static async Task<bool> IsAllowed(this UserManager<RazorPagesPizzaUser> um, RazorPagesPizzaUser user) {
        var allowed =  um.Users?.SingleOrDefault(x => x.FirstName == user.FirstName && x.LastName == user.LastName);
        return allowed != null;
    }
}