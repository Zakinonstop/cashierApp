﻿using CashierFormApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CashierFormApp.Controller;

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

            FormTransaction formTransaction = new FormTransaction();
            formTransaction.FormClosed += (s, args) => Application.Exit();

            AuthController authController = new AuthController();

            var Session = SessionController.Instance;

            var username = guna2TextBox1.Text;
            var password = guna2TextBox2.Text;

            var (isLoginTrue, message) = authController.isLoginTrue(username, password);

            if (isLoginTrue) {
                if (Session.RoleId == 2)
                {
                    dashboard.Show();
                    this.Hide();
                }
                else if (Session.RoleId == 1) 
                {
                    formTransaction.Show();
                    this.Hide();
                }
            }else{
                MessageBox.Show(message, "Peringatan", MessageBoxButtons.OK);
            }

        }

    }
}
