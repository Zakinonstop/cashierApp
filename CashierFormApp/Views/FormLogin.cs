using CashierFormApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierFormApp.View
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.FormClosed += (s, args) => Application.Exit();
            dashboard.Show();
            this.Hide();
        }
    }
}
