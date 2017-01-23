using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortagesTablet
{
    class Date
    {
        public string name;
        public int prodCount;
        public List<Sequence> sequences;

        public Date()
        {
            name = "";
            prodCount = 0;
            sequences = new List<Sequence>();
        }

        public Date(string n, int c)
        {
            name = n;
            prodCount = c;
            sequences = new List<Sequence>();
        }
    }

    class Sequence
    {
        public string name;
        public int prodCount;
        public List<Route> routes;

        public Sequence()
        {
            name = "";
            prodCount = 0;
            routes = new List<Route>();
        }

        public Sequence(string n, int c)
        {
            name = n;
            prodCount = c;
            routes = new List<Route>();
        }
    }

    class Route
    {
        public string name;
        public int prodCount;
        public List<OrderNo> orderNos;

        public Route()
        {
            name = "";
            prodCount = 0;
            orderNos = new List<OrderNo>();
        }

        public Route(string n, int c)
        {
            name = n;
            prodCount = c;
            orderNos = new List<OrderNo>();
        }
    }

    class OrderNo
    {
        public string name;
        public int prodCount;
        public List<Product> products;

        public OrderNo()
        {
            name = "";
            prodCount = 0;
            products = new List<Product>();
        }

        public OrderNo(string n, int c)
        {
            name = n;
            prodCount = c;
            products = new List<Product>();
        }
    }

    class Product
    {
        public string code;
        public string desc;
        public string cons;
        public int stock;
        public int need;
        public bool manager;
        public string comment;
        public string shortDesc;
        public bool extraCom;

        public Product()
        {
            code = "";
            desc = "";
            cons = "";
            shortDesc = "";
            stock = 0;
            need = 0;
            manager = false;
            comment = "";
            extraCom = false;
        }

        public Product(string c, string d, string co, int s, int n, bool m, string com, string sd, bool ec)
        {
            code = c;
            desc = d;
            cons = co;
            stock = s;
            shortDesc = sd;
            need = n;
            manager = m;
            comment = com;
            extraCom = ec;
        }
    }

    class Shortage
    {
        public string date;
        public string sequence;
        public string route;
        public string orderNo;
        public string product;
        public string description;
        public string cons;
        public int stock;
        public int need;
        public bool manager;
        public string comment;
        public string shortDesc;
        public bool extraCom;

        public Shortage()
        {
            date = "";
            sequence = "";
            route = "";
            shortDesc = "";
            orderNo = "";
            product = "";
            description = "";
            cons = "";
            stock = 0;
            need = 0;
            manager = false;
            comment = "";
            extraCom = false;
        }
    }
}
