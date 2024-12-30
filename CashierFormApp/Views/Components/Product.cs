using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CashierFormApp.Views.Components
{
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
            InitializeListView();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private void InitializeListView()
        {
            listProduct.View = System.Windows.Forms.View.Details;
            listProduct.FullRowSelect = true;
            listProduct.GridLines = false;
            listProduct.HeaderStyle = ColumnHeaderStyle.None;

            listProduct.Columns.Add("Code", 160, HorizontalAlignment.Left);
            listProduct.Columns.Add("Product", 200, HorizontalAlignment.Left);
            listProduct.Columns.Add("Stock", 160, HorizontalAlignment.Right);
            listProduct.Columns.Add("Price", 160, HorizontalAlignment.Right);

            listProduct.Resize += (s, e) => AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            if (listProduct.Columns.Count > 1)
            {
                int totalWidth = listProduct.ClientSize.Width - 24;
                int fixedWidth = 160 + 160 + 160;

                listProduct.Columns[1].Width = totalWidth - fixedWidth;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductHandler productHandler = new ProductHandler
            {
                IsEditMode = false,
                Text = "Add Product"
            };

            productHandler.ProductCode = "";
            productHandler.ProductName = "";
            productHandler.Stock = "";
            productHandler.Price = "";

            if (productHandler.ShowDialog() == DialogResult.OK)
            {
                ListViewItem newItem = new ListViewItem(new[]
                {
                    productHandler.ProductCode,
                    productHandler.ProductName,
                    productHandler.Stock,
                    productHandler.Price
                });

                listProduct.Items.Add(newItem);

                MessageBox.Show("Product added successfully!", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (listProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = listProduct.SelectedItems[0];

            ProductHandler productHandler = new ProductHandler
            {
                IsEditMode = true, 
                ProductCode = selectedItem.SubItems[0].Text,
                ProductName = selectedItem.SubItems[1].Text,
                Stock = selectedItem.SubItems[2].Text,
                Price = selectedItem.SubItems[3].Text
            };

            productHandler.Text = "Edit Product"; 

            if (productHandler.ShowDialog() == DialogResult.OK)
            {
                selectedItem.SubItems[0].Text = productHandler.ProductCode;
                selectedItem.SubItems[1].Text = productHandler.ProductName;
                selectedItem.SubItems[2].Text = productHandler.Stock;
                selectedItem.SubItems[3].Text = productHandler.Price;

                MessageBox.Show("Product updated successfully!", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (listProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = listProduct.SelectedItems[0];
            string productName = selectedItem.SubItems[1].Text;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the product '{productName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                listProduct.Items.Remove(selectedItem);

                MessageBox.Show("Product deleted successfully!", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
