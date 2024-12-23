using CashierFormApp.Views.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierFormApp.Views
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            MainContent mainContent = new MainContent();
            AddUserControl(mainContent);
        }

        private void AddUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MainContent mainContent = new MainContent();
            AddUserControl(mainContent);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            AddUserControl(product);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction();
            AddUserControl(transaction);
        }
    }
}
