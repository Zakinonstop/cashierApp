using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierFormApp.Views.Components
{
    public partial class ProductHandler : Form
    {
        public bool IsEditMode { get; set; } = false; 

        public string ProductCode
        {
            get => txtCode.Text;
            set => txtCode.Text = value;
        }

        public string ProductName
        {
            get => txtProduct.Text;
            set => txtProduct.Text = value;
        }

        public string Stock
        {
            get => txtStock.Text;
            set => txtStock.Text = value;
        }

        public string Price
        {
            get => txtPrice.Text;
            set => txtPrice.Text = value;
        }
        public ProductHandler()
        {
            InitializeComponent();
        }

        private void ProductHandler_Load(object sender, EventArgs e)
        {
            // Set txtCode to read-only if in Edit mode
            txtCode.ReadOnly = IsEditMode;
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                MessageBox.Show("Product name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
