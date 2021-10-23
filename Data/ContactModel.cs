using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;

namespace MNPContact
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

        public DataTable GetTable()
        {
            //dynamically fill schema from entity
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(Contact));
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor p in props)
            {
                dt.Columns.Add(p.Name, p.PropertyType);
            }

            //manually fill rows (yes, there's a weakness here if the schema changes)
            foreach (Contact c in Contacts)
            {
                dt.Rows.Add(c.id, c.Name, c.JobTitle, c.Company, c.Address, c.Phone, c.Email, c.LastContacted, c.Comments);
            }

            return dt;
        }
    }
}
