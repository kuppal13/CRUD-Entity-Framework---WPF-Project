using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharp.WPF.ADO.ConnectionModels.UserControls
{
    /// <summary>
    /// Interaction logic for ProductUC.xaml
    /// </summary>
    public partial class ProductUC : UserControl
    {
        ProductViewModel ViewModel = new ProductViewModel();
        public ProductUC()
        {
            InitializeComponent();
            ViewModel.InitializeProductInput(pbProductName, pbProductPrice, pbProductColor);
            this.DataContext = ViewModel;
        }


        private void ProductItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Product selectedProduct = (Product)button.DataContext;
            int productId = selectedProduct.ProductID;

            UpdateProductImage("/Assets/Images/Product_Images/product_2.png");

            

            ViewModel.ProductID = productId;
            ViewModel.SelectProduct(productId);
        }

        private void UpdateProductImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                productImage.Source = bitmap;
            }
            else
            {
                productImage.Source = null;
            }
        }

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddProduct();
        }

        private void RefreshProduct_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }

        private async void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditProduct();
        }

        private async void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeleteProduct();
        }
    }
}
