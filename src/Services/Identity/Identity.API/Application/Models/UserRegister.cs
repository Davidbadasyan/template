namespace Identity.API.Application.Models;

public class UserRegister
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Status { get; set; }

    [Required]
    public string Role { get; set; }
}