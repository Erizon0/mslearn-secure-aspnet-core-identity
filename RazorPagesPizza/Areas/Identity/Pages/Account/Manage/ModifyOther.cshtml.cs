using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Areas.Identity.Data;

namespace RazorPagesPizza.Areas.Identity.Pages.Account.Manage;

public class ModifyOther : PageModel {
    private readonly UserManager<RazorPagesPizzaUser> _userManager;

    public ModifyOther(UserManager<RazorPagesPizzaUser> userManager) {
        _userManager = userManager;
    }

    [TempData] public string StatusMessage { get; set; }

    public string Username { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    public class InputModel {
        [Required] [MaxLength(100)] public string FirstName { get; set; }

        [Required] [MaxLength(100)] public string LastName { get; set; }

        [Required] [MaxLength(100)] public string Uid { get; set; }

        [Required] [MaxLength(100)] public string Role { get; set; }
    }

    private async Task LoadAsync(RazorPagesPizzaUser user) {
        var userName = await _userManager.GetUserNameAsync(user);

        Username = userName;

        var role = await _userManager.GetRolesAsync(user);
        
        Input = new InputModel {
            FirstName = user.FirstName,
            LastName  = user.LastName,
            Uid = user.Uid,
            Role = role[0]
        };
    }

    // public async Task<IActionResult> OnGetAsync(string userName) {
    public async Task<IActionResult> OnGetAsync() {
        // var user = await _userManager.FindByNameAsync(userName);
        var user = await _userManager.GetUserAsync(User);

        if (user == null) {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string userName) {
        var userToEdit = await _userManager.FindByNameAsync(userName);

        if (userToEdit == null) {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid) {
            await LoadAsync(userToEdit);
            return Page();
        }

        if (Input.FirstName != userToEdit.FirstName) {
            userToEdit.FirstName = Input.FirstName;
        }

        if (Input.LastName != userToEdit.LastName) {
            userToEdit.LastName = Input.LastName;
        }

        await _userManager.UpdateAsync(userToEdit);

        return RedirectToPage("/Users");
    }
}