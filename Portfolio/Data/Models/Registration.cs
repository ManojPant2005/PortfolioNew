﻿namespace Portfolio.Data.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProductionName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string CertificateName { get; set; } = string.Empty;

    }
}