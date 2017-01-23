using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortagesTablet
{
    public partial class Pop : Form
    {
        bool comm = false;
        SQLQuery sqlQuery = new SQLQuery();
        public string orderNo { get; set; }
        public string product { get; set; }

        public Pop(string date, string route, string ordeNo, string prod, string comment)
        {
            InitializeComponent();
            lblDate.Text = date;
            lblRoute.Text = route;
            lblOrderNo.Text = ordeNo;
            lblProduct.Text = prod;
            orderNo = ordeNo;
            product = prod;
            string[] comms = comment.Split(',');
            lblStoCom.Text = comms[0];
            lblLoaCom.Text = comms[1];
            lblSalCom.Text = comms[2];
            lblMacCom.Text = comms[3];
            lblManCom.Text = comms[4];
            lblInvCom.Text = comms[5];
            lblAssCom.Text = comms[6];
            lblPaiCom.Text = comms[7];
            lblTopCom.Text = comms[8];
            for(int a = 0; a < 8; a++)
            {
                if (comms[a] != "-")
                    comm = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string commenter = "";

            if (radMan.Checked)
                commenter = "Manager";
            if (radSto.Checked)
                commenter = "Stores";
            if (radLoa.Checked)
                commenter = "Loading";
            if (radSal.Checked)
                commenter = "Sales";
            if (radMac.Checked)
                commenter = "Machine_Shop";
            if (radInv.Checked)
                commenter = "Inventory";
            if (radAss.Checked)
                commenter = "Assembly";
            if (radPai.Checked)
                commenter = "Paint_Shop";
            if (radTopOff.Checked)
                commenter = "Top_Off";

            if (comm)
                sqlQuery.SetComment(lblOrderNo.Text, lblProduct.Text, textBox1.Text, commenter);
            else
                sqlQuery.SubmitComment(lblOrderNo.Text, lblProduct.Text, textBox1.Text, commenter);

            this.DialogResult = DialogResult.None;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlQuery.MarkComplete(product, orderNo);
            this.product = lblProduct.Text;
            this.orderNo = lblOrderNo.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
