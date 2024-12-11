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

            // Clear the input field after creating the role
            txtName.Clear();
        }
    }
}
