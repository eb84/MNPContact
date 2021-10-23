using System.ComponentModel;
using System.Data;
using System.Data.Entity;

namespace MNPContactAPI.Models
{
    public partial class ContactModel : DbContext
    {
        public ContactModel()
            : base($"name=ContactModel")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
