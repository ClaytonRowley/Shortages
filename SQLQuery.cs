using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortagesTablet
{
    class SQLQuery
    { 
        SqlConnection conn = null;

        string connection = "Data Source=SCORCH;Initial Catalog=live;User ID=sa";

        public string warehouse = "!=";

        string whs_due_date = "Date";
        string OrderNo = "Order No";
        string route_name = "Route";
        string Product = "Product";
        string long_description = "Description";
        string physical_stk = "Stock";
        string Qty_Short = "Short";
        string seq = "Sequence";
        string Comments = "Comments";
        string consumer_unit = "Consumer";
        string despatched = "Despatched";
        string manager = "Manager";
        string comment = "Comment";

        public SQLQuery()
        {
            conn = new SqlConnection(connection);
        }

        public bool Connect()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        /*public List<Shortage> GetData()
        {
            List<string> manComms = new List<string>();
            List<string> comms = new List<string>();
            List<Shortage> shortages = new List<Shortage>();
            if (!Connect())
            {
                MessageBox.Show("Failed to connect to the server!");
                return null;
            }
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT * FROM [_Paul A0 Shortages2] WHERE Manager Is Not Null", conn);
                using (myReader = myCommand.ExecuteReader())
                {
                    while(myReader.Read())
                    {
                        string ord = SafeGetString("OrderNo", myReader);
                        string pro = SafeGetString("Product", myReader);
                        string man = ord + "," + pro;
                        manComms.Add(man);
                    }
                }
                myCommand = new SqlCommand("SELECT * FROM [_Paul A0 Shortages2]", conn);
                using (myReader = myCommand.ExecuteReader())
                {
                    while(myReader.Read())
                    {
                        string ord = SafeGetString("OrderNo", myReader);
                        string pro = SafeGetString("Product", myReader);
                        string man = ord + "," + pro;
                        comms.Add(man);
                    }
                }
                myCommand = new SqlCommand("SELECT * FROM [_Paul A0 Shortages Stores Open 3] WHERE [warehouse]" + warehouse + "'03' AND [Status] Is Null", conn);
                using (myReader = myCommand.ExecuteReader())
                {
                    while(myReader.Read())
                    {
                        {
                            Shortage sho = new Shortage();
                            sho.date = SafeGetDate(whs_due_date, myReader).ToShortDateString();
                            if (sho.date == new DateTime().ToShortDateString())
                                sho.date = "-";
                            sho.sequence = SafeGetString(seq, myReader);
                            sho.route = SafeGetString(route_name, myReader);
                            sho.product = SafeGetString(Product, myReader);
                            sho.description = SafeGetString(long_description, myReader);
                            sho.orderNo = SafeGetString(Order_No, myReader);
                            sho.cons = SafeGetString(consumer_unit, myReader);
                            sho.stock = SafeGetInt(physical_stk, myReader);
                            sho.need = SafeGetInt(Qty_Short, myReader);
                            foreach(string s in manComms)
                            {
                                string[] com = s.Split(',');
                                if (sho.orderNo == com[0] && sho.product == com[1])
                                    sho.manager = true;
                            }
                            foreach(string s in comms)
                            {
                                string[] com = s.Split(',');
                                if (sho.orderNo == com[0] && sho.product == com[1])
                                    sho.comment = true;
                            }
                            shortages.Add(sho);
                        }
                    }
                }
                myReader.Close();
                conn.Close();
                return shortages;
            }
            catch (Exception e)
            {
                return null;
            }
        }*/

        public List<Shortage> GetData()
        {
            List<Shortage> shortages = new List<Shortage>();
            if (Connect())
            {
                try
                {
                    SqlCommand myCommand = new SqlCommand("CR_Get_Shortages", conn);
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@wh", warehouse));

                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        while (myReader.Read())
                        {
                            Shortage s = new Shortage();
                            s.date = SafeGetString(whs_due_date, myReader);
                            s.orderNo = SafeGetString(OrderNo, myReader);
                            s.sequence = SafeGetString(seq, myReader);
                            s.route = SafeGetString(route_name, myReader);
                            s.product = SafeGetString(Product, myReader);
                            s.description = SafeGetString(long_description, myReader);
                            s.stock = SafeGetInt(physical_stk, myReader);
                            s.need = SafeGetInt(Qty_Short, myReader);
                            s.comment = SafeGetString(Comments, myReader);
                            s.cons = SafeGetString(consumer_unit, myReader);

                            string man = SafeGetString(manager, myReader);
                            if (man == "NOPE")
                                s.manager = false;
                            else
                                s.manager = true;

                            string comm = SafeGetString(comment, myReader);
                            if (comm == "NOPE")
                                s.extraCom = false;
                            else
                                s.extraCom = true;

                            shortages.Add(s);
                        }
                    }
                    conn.Close();
                    return shortages;
                }
                catch (Exception e)
                {
                    string error = e.ToString();
                    conn.Close();
                    return null;
                }
            }
            return null;
        }

        public string GetComment(string ord, string pro)
        {
            string comment = "";
            string sto, loa, sal, mac, man, inv, ass, pai, top;
            if (!Connect())
            {
                MessageBox.Show("Failed to connect to the server!");
                return null;
            }
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT * FROM [_Paul A0 Shortages2] WHERE [OrderNo] = '" + ord + "' AND [Product] = '" + pro + "'", conn);
                using (myReader = myCommand.ExecuteReader())
                {
                    myReader.Read();
                    sto = SafeGetString("Stores", myReader);
                    loa = SafeGetString("Loading", myReader);
                    sal = SafeGetString("Sales", myReader);
                    mac = SafeGetString("Machine_Shop", myReader);
                    man = SafeGetString("Manager", myReader);
                    inv = SafeGetString("Inventory", myReader);
                    ass = SafeGetString("Assembly", myReader);
                    pai = SafeGetString("Paint_Shop", myReader);
                    top = SafeGetString("Top_Off", myReader);
                    comment = sto + "," + loa + "," + sal + "," + mac + "," + man + "," + inv + "," + ass + "," + pai + "," + top;
                }
                myReader.Close();
                conn.Close();
                return comment;
            }
            catch (Exception e)
            {
                conn.Close();
                return "-,-,-,-,-,-,-,-,-";
            }
        }

        public void SetComment(string order, string product, string comment, string commenter)
        {
            if (!Connect())
            {
                MessageBox.Show("Failed to connect to the server!");
            }
            try
            {
                using (SqlCommand command = new SqlCommand(
                    "UPDATE [_Paul A0 Shortages2] SET ["+commenter+"] = @Comments2 WHERE [OrderNo] = @OrderNo AND [Product] = @Product", conn))
                {
                    command.Parameters.Add(new SqlParameter("@Comments2", comment));
                    command.Parameters.Add(new SqlParameter("@OrderNo", order));
                    command.Parameters.Add(new SqlParameter("@Product", product));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Failed to submit!");
            }
        }

        public void SubmitComment(string order, string product, string comment, string commenter)
        {
            if (!Connect())
            {
                MessageBox.Show("Failed to connect to the server!");
            }
            try
            {
                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO [_Paul A0 Shortages2] (OrderNo,Product,"+commenter+") VALUES ('"+order+"','"+product+"','"+comment+"')", conn))
                {
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Failed to submit!");
            }
        }

        public void MarkComplete(string prod, string ord)
        {
            if (!Connect())
            {
                MessageBox.Show("Failed to connect to the server!");
            }
            try
            {
                using (SqlCommand command = new SqlCommand(
                    "UPDATE [_Paul A0 Shortages] SET [Status] = @Status WHERE [Order No] = @ORDERNO AND [Product] = @PRODUCT", conn))
                {
                    command.Parameters.Add(new SqlParameter("@Status", "COMPLETE"));
                    command.Parameters.Add(new SqlParameter("@ORDERNO", ord));
                    command.Parameters.Add(new SqlParameter("@PRODUCT", prod));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
        }

        private string SafeGetString(string ord, SqlDataReader reader)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(ord)))
            {
                string temp = reader.GetString(reader.GetOrdinal(ord)).Trim();
                if (temp == "")
                    return "-";
                else
                    return temp;
            }
            else
                return "-";
        }

        private int SafeGetInt(string ord, SqlDataReader reader)
        {
            float tem = reader.IsDBNull(reader.GetOrdinal(ord)) ? default(float) : float.Parse(reader[ord].ToString());
            return (int)tem;
        }

        private DateTime SafeGetDate(string ord, SqlDataReader reader)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(ord)))
            {
                DateTime temp = reader.GetDateTime(reader.GetOrdinal(ord));
                return temp;
            }
            else
                return new DateTime();
        }
    }
}
