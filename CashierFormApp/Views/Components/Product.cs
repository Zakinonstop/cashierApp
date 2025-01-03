using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CashierFormApp.Controller;
using CashierFormApp.Model.Entity;
using Guna.UI2.AnimatorNS;

namespace CashierFormApp.Views.Components
{
    public partial class Product : UserControl
    {
        private ProductController controller;
        private List<ProductEntity> listOfProduct = new List<ProductEntity>();

        public Product()
        {
            InitializeComponent();
            InitializeListView();
            controller = new ProductController();
            LoadDataProduct();

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

            listProduct.Columns.Add("No", 30, HorizontalAlignment.Left);
            listProduct.Columns.Add("Code", 160, HorizontalAlignment.Left);
            listProduct.Columns.Add("Product", 200, HorizontalAlignment.Left);
            listProduct.Columns.Add("Stock", 160, HorizontalAlignment.Right);
            listProduct.Columns.Add("Price", 160, HorizontalAlignment.Right);

            listProduct.Resize += (s, e) => AdjustColumnWidths();
        }

        private void LoadDataProduct()
        {
            // kosongkan listview
            listProduct.Items.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfProduct = controller.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var product in listOfProduct)
            {
                var noUrut = listOfProduct.IndexOf(product);
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(product.Code);
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Stock.ToString());
                item.SubItems.Add(product.Price.ToString());

                // tampilkan data mhs ke listview
                listProduct.Items.Add(item);
            }
        }

        private void AdjustColumnWidths()
        {
            if (listProduct.Columns.Count > 1)
            {
                int totalWidth = listProduct.ClientSize.Width - 24;
                int fixedWidth = 30 + 160 + 160 + 160;

                listProduct.Columns[2].Width = totalWidth - fixedWidth;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ProductHandler productHandler = new ProductHandler
            {
                IsEditMode = false,  
            };

            //productHandler.OnCreate += OnCreateEventHandler;

            productHandler.ShowDialog();
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (listProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }else
            {
                ProductEntity product = listOfProduct[listProduct.SelectedIndices[0]];

                ProductHandler productHandler = new ProductHandler("Edit Product", product, controller);

                productHandler.OnUpdate += OnUpdateEventHandler;

                productHandler.ShowDialog();
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
            string productName = selectedItem.SubItems[2].Text;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the product '{productName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                ProductEntity product = listOfProduct[listProduct.SelectedIndices[0]];

                var hasil = controller.Delete(product);

                //if (hasil > 0) LoadDataProduct();

                listProduct.Items.Remove(selectedItem);

                MessageBox.Show("Product deleted successfully!", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void OnUpdateEventHandler(ProductEntity product)
        {
            int index = listProduct.SelectedIndices[0];

            ListViewItem itemRow = listProduct.Items[index];
            itemRow.SubItems[1].Text = product.Code;

            itemRow.SubItems[2].Text = product.Name;

            itemRow.SubItems[3].Text = product.Stock.ToString();

            itemRow.SubItems[4].Text = product.Price.ToString();
        }

    }
}
