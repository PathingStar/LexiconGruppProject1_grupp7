using System.ComponentModel.DataAnnotations;

namespace LexiconGruppProject1_grupp7.Web.Views.Account;

public class RegisterVM
{

    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [Display(Name = "E-Mail")]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Repeat password")]
    [Compare(nameof(Password))]
    public string PasswordRepeat { get; set; } = null!;

    [Display(Name = "Get Admin access")]
    public bool AdminAccess { get; set; } = false;
}
