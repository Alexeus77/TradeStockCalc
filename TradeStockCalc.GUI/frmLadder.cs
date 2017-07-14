using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeStockCalc.GUI
{
    public partial class frmLadder : Form
    {
        public ListView Listview { get; private set; }

        public frmLadder()
        {
            InitializeComponent();
            Listview = listView1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
