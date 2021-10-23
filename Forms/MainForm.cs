using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static MNPContact.Program;

namespace MNPContact
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            //for sort persistence
            DataGridViewColumn sortCol = dgvContacts.SortedColumn;
            SortOrder sortOrder = dgvContacts.SortOrder;

            //merge the name+title columns before presenting
            DataTable dt = __contactModel.GetTable();
            string nameTitleHeader = "Name" + Environment.NewLine + "(job title)";
            dt.Columns.Add(nameTitleHeader);
            foreach (DataRow r in dt.Rows)
            {
                r[nameTitleHeader] = r["Name"] + Environment.NewLine + r["JobTitle"];
            }

            dgvContacts.DataSource = dt;

            dgvContacts.Columns["Name"].Visible = false;
            dgvContacts.Columns["JobTitle"].Visible = false;
            dgvContacts.Columns["id"].Visible = false;
            dgvContacts.Columns["Comments"].Visible = false;

            dgvContacts.Columns[nameTitleHeader].DisplayIndex = 0;

            dgvContacts.Refresh();

            if (sortCol != null)
            {
                dgvContacts.Sort(dgvContacts.Columns[sortCol.Name], (ListSortDirection)Enum.Parse(typeof(ListSortDirection), sortOrder.ToString()));
            }
            else //OnSorted already resizes
            {
                dgvContacts.AutoResizeColumns();
                dgvContacts.AutoResizeRows();
            }
        }

        private void dgvContacts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvContacts.SelectedRows.Count > 0)
            {
                ContactDetailsForm contactForm = new ContactDetailsForm(
                    Convert.ToInt32(dgvContacts.SelectedRows[0].Cells["id"].Value));

                contactForm.ShowDialog();

                RefreshGrid();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ContactDetailsForm contactForm = new ContactDetailsForm();
            contactForm.ShowDialog();

            RefreshGrid();
        }

        private void dgvContacts_Sorted(object sender, EventArgs e)
        {
            dgvContacts.AutoResizeColumns();
            dgvContacts.AutoResizeRows();
        }
    }
}
