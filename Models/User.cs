using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.WPF.ADO.ConnectionModels.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        //public string UserDetail { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
