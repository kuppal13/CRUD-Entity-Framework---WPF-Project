using CSharp.WPF.ADO.ConnectionModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CSharp.WPF.ADO.ConnectionModels.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        static void InsertProduct(string ProductName, double ProductPrice, string ProductColor)
        {
            using (var ctx = new Assignment2_CF())
            {
                bool productExists = ctx.Products.Any(p => p.ProductName == ProductName && p.ProductColor == ProductColor);

                if (!productExists)
                {
                    var product = new Product()
                    {
                        ProductName = ProductName,
                        ProductPrice = ProductPrice,
                        ProductColor = ProductColor,
                        //User = new User()
                        //{
                        //    UserName = "Karam",
                        //    UserRole = "Admin"
                        //}
                    };
                    ctx.Products.Add(product);
                    ctx.SaveChanges();
                    Console.WriteLine("Product added successfully");
                }
            }
        }

        #region Constructor
        public MainView()
        {
            InsertProduct("Ruby Woo", 19.99, "Red");
            InsertProduct("Velvet Teddy", 22.99, "Light Red");
            InsertProduct("Lady Danger", 18.50, "Bright Orange");
            InitializeComponent();
        }

        #endregion

        private void AppWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
