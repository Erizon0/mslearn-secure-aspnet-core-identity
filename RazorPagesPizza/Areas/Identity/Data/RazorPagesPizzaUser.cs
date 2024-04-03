using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace RazorPagesPizza.Areas.Identity.Data;

public class RazorPagesPizzaUser : IdentityUser {
    public string Uid { get; set; } = string.Empty;
    
    [Microsoft.Build.Framework.Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Microsoft.Build.Framework.Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
}