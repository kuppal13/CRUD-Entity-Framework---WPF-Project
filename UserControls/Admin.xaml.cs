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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        AdminViewModel ViewModel = new AdminViewModel();
        public Admin()
        {
            InitializeComponent();
            ViewModel.InitializeUserInput(adUserName, adUserRole);
            this.DataContext = ViewModel;
        }


        private void UsersItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User selectedUsers = (User)button.DataContext;
            int UsersId = selectedUsers.UserID;
            ViewModel.UserId = UsersId;
            ViewModel.SelectUser(UsersId);
        }

        private async void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddUser();
        }

        private void RefreshAdmin_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }

        private async void EditAdmin_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditUser();
        }

        private async void DeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DeleteUser();
        }

        private void adUserRole_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void adUserName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pbProductName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
