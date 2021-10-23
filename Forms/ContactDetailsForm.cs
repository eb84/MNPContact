using System;
using System.Windows.Forms;
using static MNPContact.Program;

namespace MNPContact
{
    public partial class ContactDetailsForm : Form
    {
        private Contact _contact;
        bool _new = false;

        public ContactDetailsForm(int id = -1)
        {
            if (id > 0)
            {
                _contact = __contactModel.Contacts.Find(id);
            }
            else
            {
                _new = true;
                _contact = __contactModel.Contacts.Create();
            }

            InitializeComponent();
        }

        private void ContactDetailsForm_Load(object sender, EventArgs e)
        {
            txtName.Text = _contact.Name;
            txtJobTitle.Text = _contact.JobTitle;
            cboCompany.Text = _contact.Company;
            txtAddress.Text = _contact.Address;
            txtPhone.Text = _contact.Phone;
            txtEmail.Text = _contact.Email;
            dtpLastContacted.Value = _contact.LastContacted < dtpLastContacted.MinDate ? dtpLastContacted.MinDate : _contact.LastContacted;
            txtComments.Text = _contact.Comments;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(errEmail.GetError(txtEmail)))
            {
                return;
            }

            _contact.Name = txtName.Text;
            _contact.JobTitle = txtJobTitle.Text;
            _contact.Company = cboCompany.Text;
            _contact.Address = txtAddress.Text;
            _contact.Phone = txtPhone.Text;
            _contact.Email = txtEmail.Text;
            _contact.LastContacted = dtpLastContacted.Value;
            _contact.Comments = txtComments.Text;

            if (_new)
            {
                DialogResult dr = MessageBox.Show("You are about to create a new contact. Continue?",
                        "Confirm new contact",
                        MessageBoxButtons.YesNo);

                if (dr == DialogResult.No)
                {
                    return;
                }

                __contactModel.Contacts.Add(_contact);
            }
            else
            {
                if (__contactModel.ChangeTracker.HasChanges())
                {
                    DialogResult dr = MessageBox.Show("This action will overwrite the existing contact. Continue?",
                        "Overwrite details?",
                        MessageBoxButtons.YesNo);

                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            __contactModel.SaveChanges();

            this.Close();
        }

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                try
                {
                    System.Net.Mail.MailAddress address = new System.Net.Mail.MailAddress(txtEmail.Text);
                    errEmail.Clear();
                }
                catch (FormatException)
                {
                    errEmail.SetError(txtEmail, "Invalid email address");
                }
            }
            else
            {
                errEmail.Clear();
            }
        }
    }
}
