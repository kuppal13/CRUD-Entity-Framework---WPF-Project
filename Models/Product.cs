using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.ADO.ConnectionModels.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public double ProductPrice { get; set; }
        public string ProductColor { get; set; }
        public User Users { get; set; }


    }
}
