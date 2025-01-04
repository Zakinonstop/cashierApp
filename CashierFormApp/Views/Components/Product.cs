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
            listProduct.Items.Clear();

            listOfProduct = controller.ReadAll();

            foreach (var value in listOfProduct)
            {
                var noUrut = listOfProduct.IndexOf(value) + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(value.Code);
                item.SubItems.Add(value.Name);
                item.SubItems.Add(value.Stock.ToString());
                item.SubItems.Add(value.Price.ToString());

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
            ProductHandler productHandler = new ProductHandler("Add Data", controller);

            productHandler.OnCreate += OnCreateEventHandler;

            productHandler.ShowDialog();
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (listProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Edit Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }else
            {
                ProductEntity product = listOfProduct[listProduct.SelectedIndices[0]];

                ProductHandler productHandler = new ProductHandler("Edit Data", product, controller);

                productHandler.OnUpdate += OnUpdateEventHandler;

                productHandler.ShowDialog();
            }
        }


        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (listProduct.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Delete Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                if (hasil > 0) LoadDataProduct();
            }
        }

        public void OnCreateEventHandler(ProductEntity product)
        {
            LoadDataProduct();
        }

        public void OnUpdateEventHandler(ProductEntity value)
        {
            int index = listProduct.SelectedIndices[0];

            ListViewItem itemRow = listProduct.Items[index];

            itemRow.SubItems[1].Text = value.Code;

            itemRow.SubItems[2].Text = value.Name;

            itemRow.SubItems[3].Text = value.Stock.ToString();

            itemRow.SubItems[4].Text = value.Price.ToString();
        }


        private void Search()
        {
            listProduct.Items.Clear();

            listOfProduct = controller.ReadByAnything(txtCari.Text);

            foreach (var value in listOfProduct)
            {
                var noUrut = listProduct.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(value.Code);
                item.SubItems.Add(value.Name);
                item.SubItems.Add(value.Stock.ToString());
                item.SubItems.Add(value.Price.ToString());

                listProduct.Items.Add(item);
            }
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
