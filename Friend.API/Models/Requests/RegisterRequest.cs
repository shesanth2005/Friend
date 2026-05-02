using System.ComponentModel.DataAnnotations;

namespace Friend.API.Models.Requests
{
    /// Represents the data a client must send to register a new user.
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Display name must be between 2 and 50 characters.")]
        public string DisplayName { get; set; } = default!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = default!;
    }
}
