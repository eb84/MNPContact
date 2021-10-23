using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MNPContact
{
    public partial class Contact
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; private set; } = -1;

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
        [Required]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastContacted { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }
    }
}
