using System;
using System.Windows.Forms;

namespace CashierFormApp.Views.Components
{
    public partial class MemberHandler : Form
    {
        public bool IsEditMode { get; set; } = false;

        public string MemberName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        public string MemberNIK
        {
            get => txtNIK.Text;
            set => txtNIK.Text = value;
        }

        public string MemberAddress
        {
            get => txtAddress.Text;
            set => txtAddress.Text = value;
        }

        public MemberHandler()
        {
            InitializeComponent();
        }

        private void MemberHandler_Load(object sender, EventArgs e)
        {
            // Set txtNIK to read-only if in Edit mode
            txtNIK.ReadOnly = IsEditMode;
        }

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric input for NIK
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate that required fields are filled
            if (string.IsNullOrWhiteSpace(MemberName))
            {
                MessageBox.Show("Member name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(MemberNIK))
            {
                MessageBox.Show("NIK is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(MemberAddress))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
