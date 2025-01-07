using CashierFormApp.Model.Context;
using CashierFormApp.Model.Repository;
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

namespace CashierFormApp.Views.Components
{
    public partial class MainContent : UserControl
    {
        public MainContent()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            TransactionRepository repository = new TransactionRepository(new DbContext());
            var summary = repository.GetMonthlySummary();

            transactionThisMonth.Text = summary.TotalTransactions.ToString();
            totalTransactionThisMonth.Text = "Rp. " + summary.TotalAmount.ToString("N0");

            ChartArea chartArea1 = new ChartArea("BarChartArea");
            chart1.ChartAreas.Add(chartArea1);

            var monthlySummary = repository.GetMonthlyTransactionCounts();

            Title chartTitle = new Title
            {
                Text = "Transaction Every Month", 
                Docking = Docking.Top, 
                Font = new Font("Segoe UI Semibold", 14, FontStyle.Bold),
                ForeColor = Color.Black 
            };
            chart1.Titles.Add(chartTitle);

            Series series = new Series
            {
                ChartArea = "BarChartArea"
            };

            var sortedSummary = monthlySummary.OrderBy(x => new DateTime(x.Year, x.Month, 1)).ToList();

            foreach (var monthSummary in sortedSummary)
            {
                
                string monthName = new DateTime(monthSummary.Year, monthSummary.Month, 1).ToString("MMMM yyyy");
                series.Points.AddXY(monthName, monthSummary.TransactionCount);
            }

            
            chart1.Series.Add(series);
        }
    }
}
