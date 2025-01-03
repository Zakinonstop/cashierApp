using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CashierFormApp.Controller;
using CashierFormApp.Model.Entity;
using MySqlX.XDevAPI.Common;

namespace CashierFormApp.Views.Components
{
    public delegate void CreateUpdateEventHandler(ProductEntity product);
    public partial class ProductHandler : Form
    {
        public event CreateUpdateEventHandler OnCreate;

        public event CreateUpdateEventHandler OnUpdate;

        private ProductController controller;
        public bool IsEditMode { get; set; } = false;

        private ProductEntity product;

        private int produckId;


        public ProductHandler()
        {
            InitializeComponent();
            //product = new ProductEntity();
            controller = new ProductController();
        }

        public ProductHandler(string title, ProductEntity obj, ProductController controller) : this()
        {
            this.Text = title;
            this.controller = controller;
            IsEditMode = true;
            product = obj;

            produckId = product.ProductId;
            txtCode.Text = product.Code;
            txtPrice.Text =  product.Price.ToString();
            txtProduct.Text = product.Name;
            txtStock.Text = product.Stock.ToString();

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

            if (!IsEditMode) product = new ProductEntity();

            product.ProductId = produckId;
            product.Code = txtCode.Text;
            product.Name = txtProduct.Text;
            product.Stock = Convert.ToInt32(txtStock.Text);
            product.Price = Convert.ToDouble(txtPrice.Text);

            int result = 0;

            if (!IsEditMode){
                result = controller.Create(product);

                if (result > 0)
                {
                    OnCreate(product);
                    txtCode.Clear();
                    txtPrice.Clear();
                    txtProduct.Clear();
                    txtStock.Clear();
                }
            }else{
                result = controller.Update(product);

                if (result > 0)
                {
                    OnUpdate(product);
                    this.Close();
                }
            }



        }

    }
}
