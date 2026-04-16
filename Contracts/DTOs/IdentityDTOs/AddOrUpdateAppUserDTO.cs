using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs.IdentityDTOs
{
    public class AddOrUpdateAppUserDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
