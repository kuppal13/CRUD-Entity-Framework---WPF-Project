using CSharp.WPF.ADO.ConnectionModels.Models;
using CSharp.WPF.ADO.ConnectionModels.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CSharp.WPF.ADO.ConnectionModels.ViewModels
{
    public partial class AdminViewModel
    {
        #region AdminViewModel Properties

        private TextBox _tbUserName;

        public TextBox TbUserName
        {
            get { return _tbUserName; }
            set { _tbUserName = value; }
        }

        private TextBox _tbUserRole;

        public TextBox TbUserRole
        {
            get { return _tbUserRole; }
            set { _tbUserRole = value; }
        }

        public int UserId { get; set; }

        #endregion

        #region Private Members

        public static ObservableCollection<User> UsersList { get; set; } = new ObservableCollection<User>();

        private string UserName;
        private string UserRole;
        private int UserID;
        //private string UserDetail;

        #endregion

        #region Constructor

        public AdminViewModel()
        {
            _tbUserName = new TextBox();
            _tbUserRole = new TextBox();

            LoadData();
        }

        #endregion

        #region Load Data

        public void LoadData()
        {
            if (UsersList != null)
            {
                UsersList.Clear();
                DataServices.GetUsersAsync(UsersList);
            }
        }

        #endregion

        #region Initialize Input

        public void InitializeUserInput(TextBox tbUserName, TextBox tbUserRole)
        {
            _tbUserName = tbUserName;
            _tbUserRole = tbUserRole;
        }

        #endregion

        #region Helpers

        public void ClearUserInput()
        {
            _tbUserName.Text = string.Empty;
            _tbUserRole.Text = string.Empty;
        }

        public void Refresh_Page()
        {
            ClearUserInput();
            LoadData();
            UserId = -1;
        }

        #endregion

        #region Relay Commands for Admin

        public void SelectUser(int id)
        {
            UserId = id;

            var query = from u in UsersList
                        where u.UserID == UserId
                        select u;

            foreach (var item in query)
            {
                _tbUserName.Text = item.UserName;
                _tbUserRole.Text = item.UserRole;
            }
        }

        public async Task AddUser()
        {
            var userName = _tbUserName.Text;
            var userRole = _tbUserRole.Text;

            await DataServices.AddUser(userName, userRole);
            Refresh_Page();
        }

        public async Task DeleteUser()
        {
            await DataServices.DeleteUser(UserId);
            Refresh_Page();
        }

        public async Task EditUser()
        {
            var updateUserName = _tbUserName.Text;
            var updateUserRole = _tbUserRole.Text;
            await DataServices.EditUser(UserId, updateUserName, updateUserRole);
            Refresh_Page();
        }

        public void RefreshUser()
        {
            Refresh_Page();
        }

        #endregion
    }
}
