using System.ComponentModel.DataAnnotations;

namespace Portfolio.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; } 

    }
}
