using Microsoft.AspNetCore.Identity;

namespace RazorPagesPizza.Areas.Identity.Data;

public class RazorPagesPizzaUser : IdentityUser {
    public string Uid { get; set; }
    
}