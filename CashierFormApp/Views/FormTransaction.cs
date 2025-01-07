using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CashierFormApp.Controller;
using CashierFormApp.Model.Entity;
using CashierFormApp.View;
using CashierFormApp.Views.Components;
using Google.Protobuf.WellKnownTypes;
using Guna.UI2.AnimatorNS;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CashierFormApp.Views
{
    public class ProductTransaction
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public ProductTransaction(string productCode, string productName, int price)
        {
            ProductCode = productCode;
            ProductName = productName;
            Price = price;
            Quantity = 1;
        }
    }

    public partial class FormTransaction : Form
    {
        private TransactionController controller;
        private ProductController productController;
        private TransactionDetailController transactionDetailController;
        private List<TransactionEntity> listOfTransaction = new List<TransactionEntity>();
        private List<ProductEntity> listOfProduct = new List<ProductEntity>();
        private List<TransactionDetailEntity> listOfDetailTransaction = new List<TransactionDetailEntity>();
        private List<TransactionDetailEntity> listOfSumTransaction = new List<TransactionDetailEntity>();
        private int transactionDetailId;
        public FormTransaction()
        {
            InitializeComponent();
            InitializeListView();
            controller = new TransactionController();
            productController = new ProductController();
            transactionDetailController = new TransactionDetailController();

            GetNewTransactionDetailId();
        }

        private void InitializeListView()
        {
            listTransaction.View = System.Windows.Forms.View.Details;
            listTransaction.FullRowSelect = true;
            listTransaction.GridLines = false;
            listTransaction.HeaderStyle = ColumnHeaderStyle.None;

            listTransaction.Columns.Add("Code", 160, HorizontalAlignment.Left);
            listTransaction.Columns.Add("Product", 200, HorizontalAlignment.Left);
            listTransaction.Columns.Add("Price", 160, HorizontalAlignment.Right);

            listTransaction.Resize += (s, e) => AdjustColumnWidths();

            listSumTransaction.View = System.Windows.Forms.View.Details;
            listSumTransaction.FullRowSelect = true;
            listSumTransaction.GridLines = false;
            listSumTransaction.HeaderStyle = ColumnHeaderStyle.None;

            listSumTransaction.Columns.Add("Code", 0, HorizontalAlignment.Left);
            listSumTransaction.Columns.Add("qty", 40, HorizontalAlignment.Left);
            listSumTransaction.Columns.Add("Product", 180, HorizontalAlignment.Left);
            listSumTransaction.Columns.Add("Total Price", 100, HorizontalAlignment.Right);

            listSumTransaction.Resize += (s, e) => AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            if (listTransaction.Columns.Count > 1)
            {
                int totalWidth = listTransaction.ClientSize.Width - 24;
                int fixedWidth = 160 + 100 + 48; 

                listTransaction.Columns[1].Width = totalWidth - fixedWidth;
            }

            if (listSumTransaction.Columns.Count > 1)
            {
                int totalWidth = listSumTransaction.ClientSize.Width;
                int fixedWidth = 40 + 100 ;

                listSumTransaction.Columns[2].Width = totalWidth - fixedWidth;
            }
        }

        private void UpdateSumTransaction(int transactionDetailId)
        {
            listOfSumTransaction = transactionDetailController.ReadByTransactionDetailId(transactionDetailId);

            if(listOfSumTransaction.Any())
            {
                listSumTransaction.Items.Clear();

                foreach (var value in listOfSumTransaction)
                {
                    var item = new ListViewItem(value.Qty.ToString());
                    item.SubItems.Add(value.Qty.ToString());
                    item.SubItems.Add(value.ProductName);
                    item.SubItems.Add("Rp. " + value.Price.ToString("N0"));
                    listSumTransaction.Items.Add(item);

                    CalculateTotalPrice();
                }
            }
            else
            {
                MessageBox.Show("Code Product Not Found.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //bool productSumFound = false;

            //foreach (ListViewItem item in listSumTransaction.Items)
            //{
            //    if (item.Text == product.ProductCode)
            //    {
            //        productSumFound = true;

            //        string qtyText = item.SubItems[1].Text.Replace("x", "").Trim();
            //        int currentQty = int.Parse(qtyText);
            //        int newQty = currentQty + product.Quantity;

            //        string priceText = item.SubItems[3].Text.Replace("Rp.", "").Trim();
            //        int currentPrice = int.Parse(priceText.Replace(",", ""));
            //        int newTotalPrice = currentPrice + (product.Quantity * 15000);

            //        item.SubItems[1].Text = newQty.ToString() + "x";
            //        item.SubItems[3].Text = "Rp. " + newTotalPrice.ToString("N0");
            //        break;
            //    }
            //}

            //if (!productSumFound)
            //{
            //    int totalPrice = product.Quantity * product.Price;

            //    var item = new ListViewItem(product.ProductCode);
            //    item.SubItems.Add(product.Quantity.ToString() + "x");
            //    item.SubItems.Add(product.ProductName);
            //    item.SubItems.Add("Rp. " + totalPrice.ToString("N0"));

            //    listSumTransaction.Items.Add(item);
            //}

            //CalculateTotalPrice();
        }

        private decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (ListViewItem item in listSumTransaction.Items)
            {
                string priceText = item.SubItems[3].Text.Replace("Rp.", "").Replace(",", "").Trim();
                if (decimal.TryParse(priceText, out decimal price))
                {
                    totalPrice += price;
                }
            }

            decimal tax = totalPrice * 0.12m;
            decimal totalWithTax = totalPrice + tax;

            labelTax.Text = "Rp. " + tax.ToString("N0");
            labelTotal.Text = "Rp. " + totalWithTax.ToString("N0");

            return totalWithTax;
        }

        private void InputCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                string productCode = InputCode.Text.Trim();

                if (!string.IsNullOrEmpty(productCode))
                {

                    listOfProduct = productController.ReadByCode(productCode);

                    if (listOfProduct.Any())
                    {
                        foreach (var value in listOfProduct)
                        {
                            int qty = 1;
                            var createData = transactionDetailController.Create(transactionDetailId, value.ProductId, qty, value.Price);

                            if(createData > 0)
                            {
                                LoadDetailTransaksi();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Code Product Not Found.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    InputCode.Clear();
                }
            }
        }

        private void LoadDetailTransaksi()
        {

            listOfDetailTransaction = transactionDetailController.ReadDetailByTransactionDetailId(transactionDetailId);

            if (listOfDetailTransaction.Any())
            {
                listTransaction.Items.Clear();
                foreach (var valueDetail in listOfDetailTransaction)
                {
                    var item = new ListViewItem(valueDetail.ProductCode);
                    item.SubItems.Add(valueDetail.ProductName);
                    item.SubItems.Add("Rp. " + valueDetail.Price.ToString("N0"));
                    listTransaction.Items.Add(item);

                    UpdateSumTransaction(transactionDetailId);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listTransaction.SelectedItems.Count > 0)
            {
                int selectedIndex = listTransaction.SelectedIndices[0];

                if (selectedIndex >= 0 && selectedIndex < listOfDetailTransaction.Count)
                {
                    TransactionDetailEntity transactionDetail = listOfDetailTransaction[selectedIndex];

                    var result = transactionDetailController.Delete(transactionDetail);


                    if (result > 0)
                    {
                        // Reload transaction details and update the summary
                        LoadDetailTransaksi();
                        UpdateSumTransaction(transactionDetailId);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the selected transaction.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.", "Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalPrice = CalculateTotalPrice();
                
                string paymentText = InputPay.Text.Trim();
                if (string.IsNullOrEmpty(paymentText))
                {
                    MessageBox.Show("Please enter the payment amount.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal paymentAmount = decimal.Parse(paymentText);

                if (paymentAmount == 0)
                {
                    return;
                }

                if (paymentAmount < totalPrice)
                {
                    MessageBox.Show("Payment amount is less than the total price.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal change = paymentAmount - totalPrice;

                TransactionEntity transaction = new TransactionEntity();

                DateTime now = DateTime.Now;

                var session = SessionController.Instance;
                transaction.UserId = session.UserId;
                transaction.TransactionDetailId = transactionDetailId;
                transaction.Datetime = now.ToString("yyyy-MM-dd HH:mm:ss");
                transaction.TotalAmount = Convert.ToSingle(totalPrice);

                int result = controller.Create(transaction);

                if (result > 0)
                {
                    MessageBox.Show($"Payment successful!\nChange: Rp. {change.ToString("N0")}", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GetNewTransactionDetailId();
                    LoadDetailTransaksi();
                    listTransaction.Items.Clear();
                    listSumTransaction.Items.Clear();
                    InputPay.Clear();
                    labelTax.Text = "Rp. 0";
                    labelTotal.Text = "Rp. 0";
                }else
                {
                    MessageBox.Show($"Payment Failed!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid payment amount. Please enter a valid number.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int GetNewTransactionDetailId()
        {
            int result = 0;

            listOfTransaction = controller.GetMaxTransactionDetailId();

            if (listOfTransaction != null)
            {
                foreach (var value in listOfTransaction)
                {
                   result = value.TransactionDetailId;
                }
            }

            transactionDetailId = result + 1;

            return result;
        }

        private void logout_click(object sender, EventArgs e)
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
