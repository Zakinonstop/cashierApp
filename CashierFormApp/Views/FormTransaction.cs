using System;
using System.Windows.Forms;

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
        public FormTransaction()
        {
            InitializeComponent();
            InitializeListView();
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

        private void UpdateSumTransaction(ProductTransaction product)
        {
            bool productSumFound = false;

            foreach (ListViewItem item in listSumTransaction.Items)
            {
                if (item.Text == product.ProductCode)
                {
                    productSumFound = true;

                    string qtyText = item.SubItems[1].Text.Replace("x", "").Trim();
                    int currentQty = int.Parse(qtyText);
                    int newQty = currentQty + product.Quantity;

                    string priceText = item.SubItems[3].Text.Replace("Rp.", "").Trim();
                    int currentPrice = int.Parse(priceText.Replace(",", ""));
                    int newTotalPrice = currentPrice + (product.Quantity * 15000);

                    item.SubItems[1].Text = newQty.ToString() + "x"; 
                    item.SubItems[3].Text = "Rp. " + newTotalPrice.ToString("N0"); 
                    break;
                }
            }

            if (!productSumFound)
            {
                int totalPrice = product.Quantity * product.Price;

                var item = new ListViewItem(product.ProductCode);
                item.SubItems.Add(product.Quantity.ToString() + "x");
                item.SubItems.Add(product.ProductName);  
                item.SubItems.Add("Rp. " + totalPrice.ToString("N0")); 

                listSumTransaction.Items.Add(item);
            }
            CalculateTotalPrice();
        }

        private void UpdateSumTransactionAfterDeletion(string productCode, decimal productPrice)
        {
            foreach (ListViewItem item in listSumTransaction.Items)
            {
                if (item.SubItems[0].Text == productCode)
                {
                    string qtyText = item.SubItems[1].Text.Replace("x", "").Trim();
                    int currentQty = int.Parse(qtyText);

                    string totalPriceText = item.SubItems[3].Text.Replace("Rp.", "").Replace(",", "").Trim();
                    decimal currentTotalPrice = decimal.Parse(totalPriceText);

                    int newQty = currentQty - 1;
                    decimal newTotalPrice = currentTotalPrice - productPrice;

                    if (newQty > 0)
                    {
                        item.SubItems[1].Text = newQty.ToString() + "x";
                        item.SubItems[3].Text = "Rp. " + newTotalPrice.ToString("N0");
                    }
                    else
                    {
                        listSumTransaction.Items.Remove(item);
                    }

                    break;
                }
            }
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
                    string productName = "Sample Product";
                    int productPrice = 15000;

                    var item = new ListViewItem(productCode);
                    item.SubItems.Add(productName);
                    item.SubItems.Add("Rp." + productPrice.ToString("N0"));
                    listTransaction.Items.Add(item);

                    UpdateSumTransaction(new ProductTransaction(productCode, productName, productPrice));

                    InputCode.Clear();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listTransaction.SelectedItems.Count > 0)
            {
                var selectedItem = listTransaction.SelectedItems[0];

                string productCode = selectedItem.Text;
                string productPriceText = selectedItem.SubItems[2].Text.Replace("Rp.", "").Replace(",", "").Trim();
                decimal productPrice = decimal.Parse(productPriceText);

                listTransaction.Items.Remove(selectedItem);

                UpdateSumTransactionAfterDeletion(productCode, productPrice);

                CalculateTotalPrice();
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

                MessageBox.Show($"Payment successful!\nChange: Rp. {change.ToString("N0")}", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);

                listTransaction.Items.Clear();
                listSumTransaction.Items.Clear();
                InputPay.Clear();
                labelTax.Text = "Rp. 0";
                labelTotal.Text = "Rp. 0";
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid payment amount. Please enter a valid number.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
