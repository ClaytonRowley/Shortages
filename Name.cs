using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShortagesTablet
{
    public partial class Name : Form
    {
        public string commenter { get; set; }

        public Name()
        {
            InitializeComponent();
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            this.commenter = "Manager";
            this.Close();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            this.commenter = "Sales";
            this.Close();
        }

        private void btnStores_Click(object sender, EventArgs e)
        {
            this.commenter = "Stores";
            this.Close();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            this.commenter = "Inventory";
            this.Close();
        }

        private void btnMachineShop_Click(object sender, EventArgs e)
        {
            this.commenter = "Machine_Shop";
            this.Close();
        }

        private void btnPaintShop_Click(object sender, EventArgs e)
        {
            this.commenter = "Paint_Shop";
            this.Close();
        }

        private void btnAssembly_Click(object sender, EventArgs e)
        {
            this.commenter = "Assembly";
            this.Close();
        }

        private void btnLoadingBay_Click(object sender, EventArgs e)
        {
            this.commenter = "Loading";
            this.Close();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            this.commenter = "Top_Off";
            this.Close();
        }
    }
}
