using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CSharp.WPF.ADO.ConnectionModels.ViewModels
{
    public partial class ProductViewModel
    {
        #region ProductViewModel Properties

        private TextBox _tbProductName;

        public TextBox TbProductName
        {
            get { return _tbProductName; }
            set { _tbProductName = value; }
        }

        private TextBox _tbProductPrice;

        public TextBox TbProductPrice
        {
            get { return _tbProductPrice; }
            set { _tbProductPrice = value; }
        }

        public TextBox _tbProductColor;

        public TextBox TbProductColor
        {
            get { return _tbProductColor; }
            set { _tbProductColor = value; }
        }

        public int ProductId { get; set;}
 

        #endregion

        #region Private Members

        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();

        private string ProductName;
        private string ProductPrice;
        public int ProductID;
        private string ProductColor;
        private string ProductDetail;
        
        #endregion

        #region Constructor

        public ProductViewModel()
        {
            _tbProductName = new TextBox();
            _tbProductPrice = new TextBox();
            _tbProductColor = new TextBox();

            LoadData();
        }

        #endregion

        #region Load Data

        public void LoadData()
        {
            if (ProductList != null)
            {
                ProductList.Clear();
                DataServices.GetProductsAsync(ProductList);
            }
        }

        #endregion

        #region Initialize Input

        public void InitializeProductInput(TextBox tbProductName, TextBox tbProductPrice, TextBox tbProductColor)
        {
            _tbProductName = tbProductName;
            _tbProductPrice = tbProductPrice;
            _tbProductColor = tbProductColor;
        }

        #endregion

        #region Helpers

        public void ClearProductInput()
        {
            _tbProductName.Text = string.Empty;
            _tbProductPrice.Text = string.Empty;
            _tbProductColor.Text = string.Empty;
        }

        public void Refresh_Page()
        {
            ClearProductInput();
            LoadData();
            ProductId = -1;
        }

        #endregion

        #region Relay Commands for Product

        public void SelectProduct(int id)
        {
            ProductId = id;

            var query = from p in ProductList
                        where p.ProductID == ProductId
                        select p;

            foreach (var item in query)
            {
                _tbProductName.Text = item.ProductName;
                _tbProductPrice.Text = item.ProductPrice.ToString();
                _tbProductColor.Text = item.ProductColor;
            }
        }

        public async Task AddProduct()
        {
            var productName = _tbProductName.Text;
            //var productPrice = double.TryParse(_tbProductPrice.Text, out double price) ? price : 0;
            var productPrice = _tbProductPrice.Text;
            var productColor = _tbProductColor.Text;

            await DataServices.AddProduct(productName, productPrice, productColor);
            Refresh_Page();
        }

        public async Task DeleteProduct()
        {
            await DataServices.DeleteProduct(ProductId);
            Refresh_Page();
        }

        public async Task EditProduct()
        {
            var updateProductName = _tbProductName.Text;
            var updateProductPrice = _tbProductPrice.Text;
            var updateProductColor = _tbProductColor.Text;
            await DataServices.EditProduct(ProductId, updateProductName, updateProductPrice, updateProductColor);
            Refresh_Page();
        }

        public void RefreshProduct()
        {
            Refresh_Page();
        }

        #endregion
    }
}
