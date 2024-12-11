using CashierFormApp.Model.Entity;
using CashierFormApp.Controller;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSubmitRole_Click(object sender, EventArgs e)
        {

            Role role = new Role();
            role.name = txtName.Text;

            RoleController roleController = new RoleController();
            roleController.Create(role);

            txtName.Clear();
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            // Check if the txtRoleId text box is not empty
            if (string.IsNullOrEmpty(txtRoleId.Text))
            {
                MessageBox.Show("Please enter a valid role ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Try to parse the role ID from the text box
            if (int.TryParse(txtRoleId.Text, out int roleId))
            {
                Role role = new Role();
                role.role_id = roleId;

                // Create an instance of RoleController and call Delete method
                RoleController roleController = new RoleController();

                // Call the Delete method and check the result
                int result = roleController.Delete(role);

                txtRoleId.Clear();
            }
            else
            {
                MessageBox.Show("Invalid role ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
