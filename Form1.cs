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
    public partial class Form1 : Form
    {
        ComboBox drpDat, drpRou, drpPro;

        Panel content;

        List<Shortage> fullShortageList;
        List<Date> drawData = new List<Date>();
        List<Shortage> selectedShortageList = new List<Shortage>();

        List<string> dates = new List<string>();
        List<string> routes = new List<string>();
        List<string> products = new List<string>();
        List<string> commentList = new List<string>();

        string dateChoice = "", routeChoice = "", productChoice = "";

        bool w1 = true, w2 = true, w3 = true;

        SQLQuery sqlQuery;

        int datWidth, rouWidth, ordWidth, proWidth, conWidth, desWidth, shoWidth, stoWidth;
        int datX, rouX, ordX, proX, conX, desX, shoX, stoX;

        int yCount;

        int btnWidth, btnGap;
        int drpWidth, drpGap;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Width = Screen.FromControl(this).Bounds.Width;
            //this.Height = Screen.FromControl(this).Bounds.Height;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            sqlQuery = new SQLQuery();

            datWidth = (int)(this.Width / 10.8);
            rouWidth = (int)(this.Width / 7.448);
            ordWidth = (int)(this.Width / 7.448);
            proWidth = (int)(this.Width / 5.4);
            conWidth = (int)(this.Width / 13.5);
            desWidth = (int)(this.Width / 4.32);
            shoWidth = (int)(this.Width / 13.5);
            stoWidth = (int)(this.Width / 13.5);

            btnWidth = (int)(this.Width / 3);
            btnGap = btnWidth / 3;

            drpWidth = (int)(this.Width / 4);
            drpGap = drpWidth / 4;

            datX = 0;
            rouX = datWidth;
            ordX = rouX + rouWidth;
            proX = ordX + ordWidth;
            conX = proX + proWidth;
            desX = conX + conWidth;
            shoX = desX + desWidth;
            stoX = shoX + shoWidth;

            yCount = 0;

            DrawHeader();
            fullShortageList = sqlQuery.GetData();
            drawData = ConstructData(fullShortageList);
            DrawData(drawData);
        }

        private void DrawData(List<Date> data)
        {
            content.Controls.Clear();
            int hCount = 0;
            commentList.Clear();

            foreach (Date d in data)
            {
                Panel pan = new Panel();
                pan.Size = new Size(datWidth, d.prodCount * 50);
                pan.Location = new Point(datX, hCount);
                pan.BorderStyle = BorderStyle.Fixed3D;
                content.Controls.Add(pan);

                Label lblDate = new Label();
                lblDate.Location = new Point(0, 0);
                lblDate.Text = d.name;
                lblDate.TextAlign = ContentAlignment.MiddleCenter;
                lblDate.Font = new Font(lblDate.Font.FontFamily, 20, FontStyle.Bold);
                lblDate.Size = new Size(pan.Width, pan.Height);
                pan.Controls.Add(lblDate);

                foreach (Sequence s in d.sequences)
                {
                    foreach (Route r in s.routes)
                    {
                        Panel panR = new Panel();
                        panR.Size = new Size(rouWidth, r.prodCount * 50);
                        panR.Location = new Point(rouX, hCount);
                        panR.BorderStyle = BorderStyle.Fixed3D;
                        content.Controls.Add(panR);

                        Button lblR = new Button();
                        lblR.Location = new Point(0, 0);
                        lblR.Text = r.name;
                        lblR.TextAlign = ContentAlignment.MiddleCenter;
                        lblR.Click += (se, e) => { RouteClear(r); };
                        lblR.Font = new Font(lblR.Font.FontFamily, 20, FontStyle.Bold);
                        lblR.Size = new Size(panR.Width, panR.Height);
                        panR.Controls.Add(lblR);

                        foreach (OrderNo o in r.orderNos)
                        {
                            Panel panO = new Panel();
                            panO.Size = new Size(ordWidth, o.prodCount * 50);
                            panO.Location = new Point(ordX, hCount);
                            panO.BorderStyle = BorderStyle.Fixed3D;
                            content.Controls.Add(panO);

                            Button lblO = new Button();
                            lblO.Location = new Point(0, 0);
                            lblO.Text = o.name;
                            lblO.TextAlign = ContentAlignment.MiddleCenter;
                            lblO.Font = lblR.Font;
                            lblO.Click += (se, e) => { OrderClear(o); };
                            lblO.Size = new Size(panO.Width, panO.Height);
                            panO.Controls.Add(lblO);

                            foreach (Product p in o.products)
                            {
                                Color fontCol = Color.Black;
                                if (p.manager)
                                    fontCol = Color.Red;

                                Panel panP = new Panel();
                                panP.Size = new Size(proWidth, 50);
                                panP.Location = new Point(proX, hCount);
                                panP.BorderStyle = BorderStyle.Fixed3D;
                                content.Controls.Add(panP);

                                Button lblP = new Button();
                                lblP.Size = new Size(panP.Width, panP.Height);
                                lblP.Font = lblO.Font;
                                lblP.Text = p.code;
                                if (p.extraCom == true)
                                    lblP.Text += "*";
                                lblP.ForeColor = fontCol;
                                lblP.TextAlign = ContentAlignment.MiddleCenter;
                                lblP.Click += (se, e) => { PopUp(d.name, r.name, o.name, p.code); };
                                lblP.Location = new Point(0, 0);

                                Panel panC = new Panel();
                                panC.Size = new Size(conWidth, 50);
                                panC.Location = new Point(conX, hCount);
                                panC.BorderStyle = BorderStyle.Fixed3D;
                                content.Controls.Add(panC);

                                Label lblC = new Label();
                                lblC.Size = new Size(panC.Width, panC.Height);
                                lblC.Font = lblP.Font;
                                lblC.Text = p.cons;
                                lblC.ForeColor = fontCol;
                                lblC.TextAlign = ContentAlignment.MiddleCenter;
                                lblC.Location = new Point(0, 0);

                                Panel panD = new Panel();
                                panD.Size = new Size(desWidth, 50);
                                panD.Location = new Point(desX, hCount);
                                panD.BorderStyle = BorderStyle.Fixed3D;
                                content.Controls.Add(panD);

                                Label lblD = new Label();
                                lblD.Size = new Size(panD.Width, panD.Height);
                                lblD.Font = new Font(lblD.Font.FontFamily, 12, FontStyle.Bold);
                                lblD.Text = p.desc;
                                lblD.ForeColor = fontCol;
                                lblD.TextAlign = ContentAlignment.MiddleCenter;
                                lblD.Location = new Point(0, 0);

                                Panel panN = new Panel();
                                panN.Size = new Size(shoWidth, 50);
                                panN.Location = new Point(shoX, hCount);
                                panN.BorderStyle = BorderStyle.Fixed3D;
                                content.Controls.Add(panN);

                                Label lblN = new Label();
                                lblN.Size = new Size(panN.Width, panN.Height);
                                lblN.Font = lblO.Font;
                                lblN.Text = p.need.ToString();
                                lblN.ForeColor = fontCol;
                                lblN.TextAlign = ContentAlignment.MiddleCenter;
                                lblN.Location = new Point(0, 0);

                                Panel panS = new Panel();
                                panS.Size = new Size(stoWidth - 15, 50);
                                panS.Location = new Point(stoX, hCount);
                                panS.BorderStyle = BorderStyle.Fixed3D;
                                content.Controls.Add(panS);

                                Label lblS = new Label();
                                lblS.Size = new Size(panS.Width, panS.Height);
                                lblS.Font = lblN.Font;
                                lblS.ForeColor = fontCol;
                                lblS.Text = p.stock.ToString();
                                lblS.TextAlign = ContentAlignment.MiddleCenter;
                                lblS.Location = new Point(0, 0);

                                if (p.stock >= p.need)
                                {
                                    lblS.BackColor = Color.Green;
                                    lblN.BackColor = Color.Green;
                                }

                                panP.Controls.Add(lblP);
                                panC.Controls.Add(lblC);
                                panD.Controls.Add(lblD);
                                panN.Controls.Add(lblN);
                                panS.Controls.Add(lblS);
                                hCount += panP.Height;

                                string temp = o.name + ", " + p.code + ", " + p.desc;
                                commentList.Add(temp);
                            }
                        }
                    }
                }
            }
            content.Focus();
        }

        private void BtnFont_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RouteClear(Route r)
        {
            DialogResult dialogResult = MessageBox.Show("Mark the route " + r.name + " as complete?","Complete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (OrderNo o in r.orderNos)
                {
                    foreach (Product p in o.products)
                    {
                        sqlQuery.MarkComplete(p.code, o.name);
                    }
                }
                drpDat.SelectedIndex = 0;
                drpRou.SelectedIndex = 0;
                drpPro.SelectedIndex = 0;
                fullShortageList = sqlQuery.GetData();
                drawData = ConstructData(fullShortageList);
                DrawData(drawData);
            }
        }

        private void OrderClear(OrderNo o)
        {
            DialogResult dialogResult = MessageBox.Show("Mark the order " + o.name + " as complete?", "Complete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (Product p in o.products)
                {
                    sqlQuery.MarkComplete(p.code, o.name);
                }
                drpDat.SelectedIndex = 0;
                drpRou.SelectedIndex = 0;
                drpPro.SelectedIndex = 0;
                fullShortageList = sqlQuery.GetData();
                drawData = ConstructData(fullShortageList);
                DrawData(drawData);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            drpDat.SelectedIndex = 0;
            drpRou.SelectedIndex = 0;
            drpPro.SelectedIndex = 0;
            UpdateLists(drpDat.SelectedText, drpRou.SelectedText, drpPro.SelectedText);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            drpDat.SelectedIndex = 0;
            drpRou.SelectedIndex = 0;
            drpPro.SelectedIndex = 0;
            fullShortageList = sqlQuery.GetData();
            drawData = ConstructData(fullShortageList);
            DrawData(drawData);
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void DrawHeader()
        {
            yCount = 30;

            Button btnExit = new Button();
            btnExit.Size = new Size(30, 30);
            btnExit.Font = new Font(btnExit.Font.FontFamily, 15, FontStyle.Bold);
            btnExit.Location = new Point(this.Width - 30, 30);
            btnExit.Text = "X";
            btnExit.Click += new EventHandler(BtnFont_Click);
            this.Controls.Add(btnExit);

            Button btnMin = new Button();
            btnMin.Size = new Size(30, 30);
            btnMin.Font = btnExit.Font;
            btnMin.Location = new Point(this.Width - 60, 30);
            btnMin.Text = "-";
            btnMin.Click += new EventHandler(BtnMin_Click);
            this.Controls.Add(btnMin);

            Button btnClear = new Button();
            btnClear.Size = new Size(btnWidth, 100);
            btnClear.Font = new Font(btnClear.Font.FontFamily, 25, FontStyle.Bold);
            btnClear.Location = new Point(btnGap, yCount);
            btnClear.Text = "Clear";
            btnClear.Click += new EventHandler(BtnClear_Click);
            this.Controls.Add(btnClear);

            Button btnRefresh = new Button();
            btnRefresh.Size = new Size(btnWidth, 100);
            btnRefresh.Font = btnClear.Font;
            btnRefresh.Location = new Point(btnWidth + (btnGap * 2), yCount);
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += new EventHandler(BtnRefresh_Click);
            this.Controls.Add(btnRefresh);

            yCount = 130;

            Label tempD = new Label();
            tempD.Location = new Point(drpGap, yCount);
            tempD.Size = new Size(drpWidth, 50);
            tempD.Font = btnClear.Font;
            tempD.Text = "Date";
            tempD.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(tempD);

            Label tempR = new Label();
            tempR.Location = new Point((drpGap * 2) + drpWidth, yCount);
            tempR.Size = new Size(drpWidth, 50);
            tempR.Font = btnClear.Font;
            tempR.Text = "Route";
            tempR.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(tempR);

            Label tempP = new Label();
            tempP.Location = new Point((drpGap * 3) + (drpWidth * 2), yCount);
            tempP.Size = new Size(drpWidth, 50);
            tempP.Font = btnClear.Font;
            tempP.Text = "Product";
            tempP.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(tempP);

            yCount += 50;

            drpDat = new ComboBox();
            drpDat.Size = new Size(drpWidth, 100);
            drpDat.Font = btnClear.Font;
            drpDat.Location = new Point(drpGap, yCount);
            drpDat.Text = "Date";
            drpDat.SelectionChangeCommitted += new EventHandler(DrpDat_SelectedIndexChange);
            this.Controls.Add(drpDat);

            drpRou = new ComboBox();
            drpRou.Size = drpDat.Size;
            drpRou.Font = drpDat.Font;
            drpRou.Location = new Point((drpGap * 2) + drpWidth, yCount);
            drpRou.Text = "Route";
            drpRou.SelectionChangeCommitted += new EventHandler(DrpRou_SelectedIndexChange);
            this.Controls.Add(drpRou);

            drpPro = new ComboBox();
            drpPro.Size = drpDat.Size;
            drpPro.Font = drpDat.Font;
            drpPro.Location = new Point((drpGap * 3) + (drpWidth * 2), yCount);
            drpPro.Text = "Product";
            drpPro.SelectionChangeCommitted += new EventHandler(DrpPro_SelectedIndexChange);
            this.Controls.Add(drpPro);

            yCount = drpDat.Location.Y + drpDat.Height + 10;

            Label lblDat = new Label();
            lblDat.Font = new Font(lblDat.Font.FontFamily, 15, FontStyle.Bold);
            lblDat.TextAlign = ContentAlignment.MiddleCenter;
            lblDat.BorderStyle = BorderStyle.Fixed3D;
            lblDat.Location = new Point(datX, yCount);
            lblDat.Size = new Size(datWidth, 75);
            lblDat.Text = "Date";
            this.Controls.Add(lblDat);

            Label lblRou = new Label();
            lblRou.Font = new Font(lblRou.Font.FontFamily, 15, FontStyle.Bold);
            lblRou.TextAlign = ContentAlignment.MiddleCenter;
            lblRou.BorderStyle = BorderStyle.Fixed3D;
            lblRou.Location = new Point(rouX, yCount);
            lblRou.Size = new Size(rouWidth, 75);
            lblRou.Text = "Route";
            this.Controls.Add(lblRou);

            Label lblOrd = new Label();
            lblOrd.Font = new Font(lblOrd.Font.FontFamily, 15, FontStyle.Bold);
            lblOrd.TextAlign = ContentAlignment.MiddleCenter;
            lblOrd.BorderStyle = BorderStyle.Fixed3D;
            lblOrd.Location = new Point(ordX, yCount);
            lblOrd.Size = new Size(ordWidth, 75);
            lblOrd.Text = "Order No";
            this.Controls.Add(lblOrd);

            Label lblPro = new Label();
            lblPro.Font = new Font(lblPro.Font.FontFamily, 15, FontStyle.Bold);
            lblPro.TextAlign = ContentAlignment.MiddleCenter;
            lblPro.BorderStyle = BorderStyle.Fixed3D;
            lblPro.Location = new Point(proX, yCount);
            lblPro.Size = new Size(proWidth, 75);
            lblPro.Text = "Product";
            this.Controls.Add(lblPro);

            Label lblCon = new Label();
            lblCon.Font = new Font(lblCon.Font.FontFamily, 15, FontStyle.Bold);
            lblCon.TextAlign = ContentAlignment.MiddleCenter;
            lblCon.BorderStyle = BorderStyle.Fixed3D;
            lblCon.Location = new Point(conX, yCount);
            lblCon.Size = new Size(conWidth, 75);
            lblCon.Text = "Consumer Unit";
            this.Controls.Add(lblCon);

            Label lblDes = new Label();
            lblDes.Font = new Font(lblDes.Font.FontFamily, 15, FontStyle.Bold);
            lblDes.TextAlign = ContentAlignment.MiddleCenter;
            lblDes.BorderStyle = BorderStyle.Fixed3D;
            lblDes.Location = new Point(desX, yCount);
            lblDes.Size = new Size(desWidth, 75);
            lblDes.Text = "Description";
            this.Controls.Add(lblDes);

            Label lblSho = new Label();
            lblSho.Font = new Font(lblSho.Font.FontFamily, 15, FontStyle.Bold);
            lblSho.TextAlign = ContentAlignment.MiddleCenter;
            lblSho.BorderStyle = BorderStyle.Fixed3D;
            lblSho.Location = new Point(shoX, yCount);
            lblSho.Size = new Size(shoWidth, 75);
            lblSho.Text = "Short";
            this.Controls.Add(lblSho);

            Label lblSto = new Label();
            lblSto.Font = new Font(lblSto.Font.FontFamily, 15, FontStyle.Bold);
            lblSto.TextAlign = ContentAlignment.MiddleCenter;
            lblSto.BorderStyle = BorderStyle.Fixed3D;
            lblSto.Location = new Point(stoX, yCount);
            lblSto.Size = new Size(stoWidth, 75);
            lblSto.Text = "Stock";
            this.Controls.Add(lblSto);

            yCount += 75;

            content = new Panel();
            content.Size = new Size(this.Width, this.Height - yCount);
            content.Location = new Point(0, yCount);
            content.AutoScroll = true;
            this.Controls.Add(content);
        }

        private void DrpDat_SelectedIndexChange(object sender, EventArgs e)
        {
            dateChoice = drpDat.SelectedItem.ToString();
            UpdateLists(dateChoice, routeChoice, productChoice);
        }

        private void DrpRou_SelectedIndexChange(object sender, EventArgs e)
        {
            routeChoice = drpRou.SelectedItem.ToString();
            UpdateLists(dateChoice, routeChoice, productChoice);
        }

        private void DrpPro_SelectedIndexChange(object sender, EventArgs e)
        {
            productChoice = drpPro.SelectedItem.ToString();
            UpdateLists(dateChoice, routeChoice, productChoice);
        }

        private void UpdateLists(string date, string route, string product)
        {
            List<Shortage> tempShort = new List<Shortage>();
            List<Shortage> removalList = new List<Shortage>();

            foreach (Shortage s in fullShortageList)
                tempShort.Add(s);


            if (!date.Equals(""))
                foreach (Shortage s in fullShortageList)
                    if (!s.date.Equals(date))
                        if (!removalList.Contains(s))
                            removalList.Add(s);

            if (!route.Equals(""))
                foreach (Shortage s in fullShortageList)
                    if (!s.route.Equals(route))
                        if (!removalList.Contains(s))
                            removalList.Add(s);

            if (!product.Equals(""))
                foreach (Shortage s in fullShortageList)
                    if (!s.product.Equals(product))
                        if (!removalList.Contains(s))
                            removalList.Add(s);

            foreach (Shortage s in removalList)
                tempShort.Remove(s);

            selectedShortageList = tempShort;

            dates.Clear();
            routes.Clear();
            products.Clear();

            foreach(Shortage s in selectedShortageList)
            {
                if (!dates.Contains(s.date))
                    dates.Add(s.date);
                if (!products.Contains(s.product))
                    products.Add(s.product);
                if (!routes.Contains(s.route))
                    routes.Add(s.route);
            }

            dates.Sort();
            products.Sort();
            routes.Sort();

            dates.Insert(0, "");
            routes.Insert(0, "");
            products.Insert(0, "");

            drpDat.DataSource = null;
            drpDat.DataSource = dates;
            drpDat.Text = date;

            drpRou.DataSource = null;
            drpRou.DataSource = routes;
            drpRou.Text = route;

            drpPro.DataSource = null;
            drpPro.DataSource = products;
            drpPro.Text = product;

            drawData = ConstructData(selectedShortageList);
            DrawData(drawData);
        }

        private List<Date> ConstructData(List<Shortage> shorts)
        {
            List<Date> dat = new List<Date>();

            foreach (Shortage s in shorts)
            {
                Date tempD = new Date(s.date, 1);
                Product tempP = new Product(s.product, s.description, s.cons, s.stock, s.need, s.manager, s.comment, s.shortDesc, s.extraCom);
                Route tempR = new Route(s.route, 1);
                Sequence tempS = new Sequence(s.sequence, 1);
                OrderNo tempO = new OrderNo(s.orderNo, 1);

                if (!routes.Contains(s.route))
                    routes.Add(s.route);
                if (!dates.Contains(s.date))
                    dates.Add(s.date);
                if (!products.Contains(s.product))
                    products.Add(s.product);

                tempO.products.Add(tempP);
                tempR.orderNos.Add(tempO);
                tempS.routes.Add(tempR);
                tempD.sequences.Add(tempS);

                Date datT = dat.Find(i => i.name == s.date);
                if(datT != null)
                {
                    Sequence seqT = datT.sequences.Find(i => i.name == s.sequence);
                    if(seqT != null)
                    {
                        Route rouT = seqT.routes.Find(i => i.name == s.route);
                        if(rouT != null)
                        {
                            OrderNo ordT = rouT.orderNos.Find(i => i.name == s.orderNo);
                            if(ordT != null)
                            {
                                Product proT = ordT.products.Find(i => i.code == s.product);
                                if(proT != null)
                                {
                                    proT.need += tempP.need;
                                }
                                else
                                {
                                    ordT.products.Add(tempP);
                                    ordT.prodCount += 1;
                                    rouT.prodCount += 1;
                                    seqT.prodCount += 1;
                                    datT.prodCount += 1;
                                }
                            }
                            else
                            {
                                rouT.orderNos.Add(tempO);
                                rouT.prodCount += 1;
                                seqT.prodCount += 1;
                                datT.prodCount += 1;
                            }
                        }
                        else
                        {
                            seqT.routes.Add(tempR);
                            seqT.prodCount += 1;
                            datT.prodCount += 1;
                        }
                    }
                    else
                    {
                        datT.sequences.Add(tempS);
                        datT.prodCount += 1;
                    }
                }
                else
                {
                    dat.Add(tempD);
                }
            }
            dates.Sort();
            routes.Sort();
            products.Sort();

            if (!dates.Contains(""))
                dates.Insert(0, "");
            if (!routes.Contains(""))
                routes.Insert(0, "");
            if (!products.Contains(""))
                products.Insert(0, "");

            drpDat.DataSource = null;
            drpDat.DataSource = dates;

            drpRou.DataSource = null;
            drpRou.DataSource = routes;

            drpPro.DataSource = null;
            drpPro.DataSource = products;
            dat = OrderDates(dat);
            return dat;
        }

        private List<Date> OrderDates(List<Date> data)
        {
            List<Date> unSorted = data;
            List<Date> sorted = new List<Date>();
            List<DateTime> dates = new List<DateTime>();

            foreach(Date d in unSorted)
            {
                DateTime temp = new DateTime();
                if (d.name == "-")
                    dates.Add(temp);
                else
                    dates.Add(temp = DateTime.Parse(d.name));
            }
            dates.Sort();

            foreach(DateTime dt in dates)
            {
                if (dt == new DateTime())
                {
                    Date tempD = unSorted.Find(i => i.name == "-");
                    sorted.Add(tempD);
                }
                else
                {
                    Date tempD = unSorted.Find(i => i.name == dt.ToShortDateString());
                    sorted.Add(tempD);
                }
            }
            return sorted;
        }

        public void PopUp(string date, string route, string order, string product)
        {
            string comm = sqlQuery.GetComment(order, product);

            using (Pop pop = new Pop(date, route, order, product, comm))
            {
                var result = pop.ShowDialog();
                if(result == DialogResult.OK)
                {
                    string ord = pop.orderNo;
                    string pro = pop.product;

                    bool dat = false, rou = false, prop = false;
                    if(selectedShortageList.Count != 0)
                    {
                        int count = 0, correct = 0;

                        foreach(Shortage s in selectedShortageList)
                        {
                            if (pro == s.product && ord == s.orderNo)
                                correct = count;

                            count++;
                        }
                        selectedShortageList.RemoveAt(correct);
                    }

                    foreach(Shortage s in selectedShortageList)
                    {
                        if (s.date == dateChoice)
                            dat = true;
                        if (s.route == routeChoice)
                            rou = true;
                        if (s.product == productChoice)
                            prop = true;
                    }

                    if(drpDat.SelectedIndex != 0)
                    {
                        if(!dat)
                        {
                            Reset();
                            return;
                        }
                    }

                    if(drpRou.SelectedIndex != 0)
                    {
                        if(!rou)
                        {
                            Reset();
                            return;
                        }
                    }

                    if(drpPro.SelectedIndex != 0)
                    {
                        if(!prop)
                        {
                            Reset();
                            return;
                        }
                    }

                    if(selectedShortageList.Count == 0)
                    {
                        Reset();
                        return;
                    }

                    drawData = ConstructData(selectedShortageList);
                    DrawData(drawData);
                }
            }
        }

        private void Reset()
        {
            fullShortageList = sqlQuery.GetData();
            drawData = ConstructData(fullShortageList);
            UpdateLists("", "", "");
            DrawData(drawData);
        }

        private void warehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlQuery.warehouse == "!=")
                sqlQuery.warehouse = "=";
            else
                sqlQuery.warehouse = "!=";

            Reset();
        }

        private void multiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Combo c = new Combo(commentList))
            {
                var result = c.ShowDialog();
                if (result == DialogResult.OK)
                {
                    drpDat.SelectedIndex = 0;
                    drpRou.SelectedIndex = 0;
                    drpPro.SelectedIndex = 0;
                    fullShortageList = sqlQuery.GetData();
                    drawData = ConstructData(fullShortageList);
                    DrawData(drawData);
                }
            }
        }
    }
}
