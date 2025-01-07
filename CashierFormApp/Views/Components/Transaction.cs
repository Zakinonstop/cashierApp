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
    public partial class Transaction : UserControl
    {
        private TransactionController controller;
        private List<TransactionEntity> listOfTransactionLog = new List<TransactionEntity>();
        public Transaction()
        {
            InitializeComponent();
            InitializeListView();
            //AddDummyData(); // Add dummy data
            controller = new TransactionController();
            LoadDataTransaction();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private void InitializeListView()
        {
            listTansactionLog.View = System.Windows.Forms.View.Details;
            listTansactionLog.FullRowSelect = true;
            listTansactionLog.GridLines = false;
            listTansactionLog.HeaderStyle = ColumnHeaderStyle.None;

            listTansactionLog.Columns.Add("Date", 180, HorizontalAlignment.Left);
            listTansactionLog.Columns.Add("Cashier", 300, HorizontalAlignment.Left);
            listTansactionLog.Columns.Add("Paid", 200, HorizontalAlignment.Left);
            listTansactionLog.Columns.Add("Change", 200, HorizontalAlignment.Left);
            listTansactionLog.Columns.Add("Price", 200, HorizontalAlignment.Right);

            listTansactionLog.Resize += (s, e) => AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            if (listTansactionLog.Columns.Count > 1)
            {
                int totalWidth = listTansactionLog.ClientSize.Width - 24;
                int fixedWidth = 160 + 200 + 200 + 200;

                listTansactionLog.Columns[1].Width = totalWidth - fixedWidth;
            }
        }

        private void AddDummyData()
        {
            // Sample dummy data
            var dummyData = new List<string[]>
            {
                new string[] { "12 Jan 2025 - 19:20", "Shabbah Athabiyyu", "Rp 150.000,00", "Rp 10.000,00", "Rp 140.000,00" },
                new string[] { "12 Jan 2025 - 19:25", "Shabbah Athabiyyu", "Rp 200.000,00", "Rp 50.000,00", "Rp 150.000,00" },
                new string[] { "12 Jan 2025 - 19:30", "Shabbah Athabiyyu", "Rp 100.000,00", "Rp 20.000,00", "Rp 80.000,00" },
                new string[] { "12 Jan 2025 - 19:35", "Shabbah Athabiyyu", "Rp 300.000,00", "Rp 150.000,00", "Rp 150.000,00" }
            };

            // Add rows to ListView
            foreach (var data in dummyData)
            {
                ListViewItem item = new ListViewItem(data);
                listTansactionLog.Items.Add(item);
            }
        }

        private void LoadDataTransaction()
        {
            listOfTransactionLog = controller.ReadAll();
            MessageBox.Show($"Total records: {listOfTransactionLog.Count}");

            if (listOfTransactionLog.Any())
            {
                foreach (var value in listOfTransactionLog)
                {
                    var noUrut = listOfTransactionLog.IndexOf(value) + 1;
                    var item = new ListViewItem(noUrut.ToString());

                    item.SubItems.Add(value.Datetime.ToString());
                    item.SubItems.Add("Rp. " + value.Paid.ToString());
                    item.SubItems.Add("Rp. " + value.Changed.ToString());
                    item.SubItems.Add("Rp. " + value.TotalAmount.ToString());
                    listTansactionLog.Items.Add(item);
                }
            }
        }

        private void Search()
        {
            listTansactionLog.Items.Clear();

            listOfTransactionLog = controller.ReadByAnything(txtCari.Text);

            foreach (var value in listOfTransactionLog)
            {
                var noUrut = listOfTransactionLog.IndexOf(value) + 1;
                var item = new ListViewItem(noUrut.ToString());

                item.SubItems.Add(value.Datetime.ToString());
                item.SubItems.Add("Rp. " + value.Paid.ToString());
                item.SubItems.Add("Rp. " + value.Changed.ToString());
                item.SubItems.Add("Rp. " + value.TotalAmount.ToString());
                listTansactionLog.Items.Add(item);
            }
        }


        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
