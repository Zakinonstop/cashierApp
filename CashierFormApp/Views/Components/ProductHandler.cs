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
using CashierFormApp.Model.Entity;

namespace CashierFormApp.Views.Components
{
    public partial class ProductHandler : Form
    {
        public bool IsEditMode { get; set; } = false;
        private ProductEntity product;
        private ProductController controller;

        public ProductHandler()
        {
            InitializeComponent();
            //product = new ProductEntity();
            controller = new ProductController();
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

            if (!IsEditMode)
            {
                product = new ProductEntity();

                product.Code = txtCode.Text;
                product.Name = txtProduct.Text;
                product.Stock = Convert.ToInt32(txtStock.Text);
                product.Price = Convert.ToDouble(txtPrice.Text);

                int result = 0;

                result = controller.Create(product);

                if (result > 0)
                {
                    txtCode.Clear();
                    txtPrice.Clear();
                    txtProduct.Clear();
                    txtStock.Clear();
                }
            }



        }

    }
}
