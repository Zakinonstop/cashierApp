using CashierFormApp.View;
using CashierFormApp.Views.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CashierFormApp.Views
{
    public partial class Dashboard : Form
    {
        private Dictionary<string, UserControl> userControlCache = new Dictionary<string, UserControl>(); 
        private UserControl currentControl;

        public Dashboard()
        {
            InitializeComponent();
            LoadUserControl("MainContent"); 
        }

        private void LoadUserControl(string controlName)
        {
            labelUsername.Text = "zakinonstop";
            labelRole.Text = "Kasir";

            if (currentControl != null && currentControl.Name == controlName)
            {
                return; 
            }

            if (!userControlCache.ContainsKey(controlName))
            {
                UserControl newControl = CreateUserControl(controlName);
                if (newControl != null)
                {
                    userControlCache[controlName] = newControl;
                }
            }

            if (userControlCache.ContainsKey(controlName))
            {
                panelContainer.Controls.Clear();
                currentControl = userControlCache[controlName];
                currentControl.Dock = DockStyle.Fill;
                panelContainer.Controls.Add(currentControl);
                currentControl.BringToFront();
            }
        }

        private UserControl CreateUserControl(string controlName)
        {
            UserControl userControl = null;

            switch (controlName)
            {
                case "MainContent":
                    userControl = new MainContent { Name = "MainContent" };
                    break;

                case "Product":
                    userControl = new Product { Name = "Product" };
                    break;

                case "Transaction":
                    userControl = new Transaction { Name = "Transaction" };
                    break;

                case "Member":
                    userControl = new Member { Name = "Member" };
                    break;

                default:
                    break;
            }

            return userControl;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadUserControl("MainContent");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LoadUserControl("Product");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            LoadUserControl("Transaction");
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LoadUserControl("Member");
        }

        private void guna2ShadowPanel2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                FormLogin FormLogin = new FormLogin();
                FormLogin.FormClosed += (s, args) => Application.Exit();
                FormLogin.Show();
                this.Hide();

            }
        }

        
    }
}
