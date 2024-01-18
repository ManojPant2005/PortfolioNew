using System.ComponentModel.DataAnnotations;

namespace Portfolio.Data.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        [Required, MinLength(5), MaxLength(100)]
        public string? Name { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public int? Phone { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, DataType(DataType.Password), MinLength(5), MaxLength(100)]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password"), MinLength(5), MaxLength(100)]
        public string? ConfirmPassword { get; set; }
        [Required, MinLength(5), MaxLength(100)]
        public string? ProductionName { get; set; }
        [Required, MinLength(5), MaxLength(1000), DataType(DataType.MultilineText)]
        public string? Address { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public string? CertificateName { get; set; }
    }
}
