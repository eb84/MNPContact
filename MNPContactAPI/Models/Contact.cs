using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MNPContactAPI.Models
{
    public partial class Contact
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; } = -1;

        [Column(Order = 1)]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Column(Order = 2)]
        [StringLength(50)]
        [Required]
        public string JobTitle { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        [CompanyAttribute]
        [Required]
        public string Company { get; set; }

        [Column(Order = 4)]
        [StringLength(200)]
        [Required]
        public string Address { get; set; }

        [Column(Order = 5)]
        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        [Column(Order = 6)]
        [StringLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastContacted { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        public class CompanyAttribute : ValidationAttribute
        {
            public string GetErrorMessage() => "Invalid company. Expected one of: Disney, HP, Microsoft.";

            protected override ValidationResult IsValid(object value,
                ValidationContext validationContext)
            {
                Contact contact = (Contact)validationContext.ObjectInstance;
                if (!new string[] { "Disney", "HP", "Microsoft" }.Contains(contact.Company))
                {
                    return new ValidationResult(GetErrorMessage());
                }

                return ValidationResult.Success;
            }
        }
    }
}
