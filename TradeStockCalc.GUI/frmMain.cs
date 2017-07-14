using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using TradeStockCalc;
using TradeStockCalc.Data;
using TradeStockCalc.DataLoaders;
using TradeStockCalc.Converter;
using TradeStockCalc.Helpers;

namespace TradeStockCalc.GUI
{
    public partial class frmMain : Form
    {
        Stream _currentStream = null;

        public frmMain()
        {
            InitializeComponent();
        }

        private void selectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {

                _currentStream = openFileDialog1.OpenFile();
                label1.Text = openFileDialog1.FileName;
                FillMainList();

            }
        }

        private void FillMainList()
        {
            try
            {

                if (_currentStream == null)
                    return;

                this.listView1.Items.Clear();
                FillListView(_currentStream, this.listView1.Items, AddMainListViewItems);
            }
            catch(Exception exp)
            {
                ProcessError(exp);
            }
        }

        private void ProcessError(Exception exp)
        {
            MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(Currency)));
            comboBox1.SelectedIndex = 0;

            openFileDialog1.FileName = "";

            Price.convertor = new ECBCurrencyConverter();

            label3.Text = string.Format(label3.Text, ECBCurrencyConverter.ServiceUrl);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMainList();
        }

        private Currency GetTargetCurrencyFromComboBox()
        {
            return (Currency)Enum.Parse(
                typeof(Currency),
                comboBox1.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int[] ladderRange =
                { -10, -5, -2, -1, 0, 1, 2, 5, 10 };

            ShowMatrixFrm("Volatily Deviation", ladderRange,
                (inputstream, currency, filter, deviationValue) => 
                TradesCalculationHelper.CalculateTrades(inputstream, currency, filter, 0, deviationValue));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] ladderRange =
               { -100, -50, -25, -10, 0, 10, 25, 50, 100 };

            ShowMatrixFrm("Stock Deviation", ladderRange,
                (inputstream, currency, filter, deviationValue) =>
                TradesCalculationHelper.CalculateTrades(inputstream, currency, filter, deviationValue, 0));
        }

        private void ShowMatrixFrm(string caption, int[] ladderRange, 
            Func<Stream, Currency, Func<TradeData, bool>, double, 
                IEnumerable<Tuple<TradeData, Price, Currency>>> calculateTrades)
        {

            if (_currentStream == null)
                return;

            string[] columnHeadersNames = new string[ladderRange.Count() + 1];

            columnHeadersNames[0] = "Option";

            frmLadder frmLadder = new frmLadder();

            frmLadder.Text = caption;

            frmLadder.Listview.AddColumnsHeaders(new string[] { "Option" });

            frmLadder.Listview.AddColumnsHeaders(
                ladderRange.Select(value => value.ToString() + " %").ToArray());


            var callItem = frmLadder.Listview.Items.Add("CALL");
            var putItem = frmLadder.Listview.Items.Add("PUT");

            var targetCurrency = GetTargetCurrencyFromComboBox();

            try
            {

                foreach (var deviationValue in ladderRange)
                {
                    Price totalResultCall = Price.Default;
                    calculateTrades(_currentStream, targetCurrency, trade => trade.cp == CP.C, deviationValue).ToList().
                        ForEach(result => totalResultCall.Value += result.Item2.Value);

                    Price totalResultPut = Price.Default;
                    calculateTrades(_currentStream, targetCurrency, trade => trade.cp == CP.P, deviationValue).ToList().
                        ForEach(result => totalResultPut.Value += result.Item2.Value);

                    callItem.SubItems.Add(totalResultCall.Value);
                    putItem.SubItems.Add(totalResultPut.Value);
                }

                frmLadder.Show(this);
            }
            catch(Exception exp)
            {
                ProcessError(exp);
                frmLadder.Close();
            }


        }
        

        private void FillListView(Stream inputStream,
            ListView.ListViewItemCollection listViewItems,
            Action<ListView.ListViewItemCollection, TradeData, Price, Currency> addItemsAction)
        {

            Currency targetCurrency = GetTargetCurrencyFromComboBox();

            decimal callTotal = 0;
            decimal putTotal = 0;

            foreach (var tradeResult in TradesCalculationHelper.CalculateTrades(inputStream, targetCurrency, trade => true))
            {
                addItemsAction(listViewItems, tradeResult.Item1, tradeResult.Item2, tradeResult.Item3);

                callTotal += tradeResult.Item1.cp == CP.C ? tradeResult.Item2.Value : 0;
                putTotal += tradeResult.Item1.cp == CP.P ? tradeResult.Item2.Value : 0;
            }

            listViewItems.Add("");

            listViewItems.Add("Total CALL").SubItems.Add(callTotal.ToString());
            listViewItems.Add("Total PUT").SubItems.Add(putTotal.ToString());
        }

        private void AddMainListViewItems(ListView.ListViewItemCollection listViewItems, 
            TradeData trade, Price tradeResult, Currency targetCurrency)
        {
            listViewItems.AddItems(
                trade.Name,
                InitialData.stockData[trade.Name].SpotPrice.Convert(targetCurrency).Value,
                trade.StrikePrice.Convert(targetCurrency).Value,
                trade.Expiry,
                trade.cp == CP.C ? "CALL" : "PUT",
                tradeResult.Value);
        }
    }

}
