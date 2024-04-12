
using CSharp.WPF.ADO.ConnectionModels.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace CSharp.WPF.ADO.ConnectionModels.Services
{
    public static class DataServices
    {

        #region Connection String to MSSQLEXPRESS 
        /// <summary>
        /// Use Application Properties - Right Click app, select Properties and choose
        /// Settings - to entry strings for Application
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            var connStr = CSharp.WPF.ADO.ConnectionModels.Properties.Settings.Default.Assignment2SqlServer;
            return connStr;
        }
        #endregion

        #region Helpers
        public static async Task HandleException(Exception ex)
        {
            string msg = ex.Message.ToString();
            MessageBox.Show(msg);
            await Task.CompletedTask;
        }


        public static bool IsFieldsEmpty(string userName, string userRole)
        {
            var result = false;

            if (userName == "") result = true;
            if (userRole == "") result = true;

            return result;
        }

        public static bool IsProductsEmpty(string productName, string productPrice, string productColour)
        {
            var result = false;

            if (productName == "") result = true;
            if (productPrice == "") result = true;
            if (productColour == "") result = true;

            return result;
        }
        #endregion


        #region Async Task Retrieve Users and Product

        public async static void GetUsersAsync(ObservableCollection<User> users)
        {

            try
            {

                var query = "Select * from [Assignment2_CF].[dbo].[Users]";

                var connection = new SqlConnection(GetConnectionString());
                connection.Open();

                var cmd = new SqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _user = new User
                    {
                        UserID = reader.GetInt32(0)
                    };

                    if (!reader.IsDBNull(1))
                        _user.UserName = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        _user.UserRole = reader.GetString(2);

                    //_user.UserDetail = $"{_user.UserName} {_user.UserRole}";

                    users.Add(_user);
                }

                connection.Close();
            }
            catch (Exception ex)
            {

                await HandleException(ex);
                throw;

            }

        }

        public async static void GetProductsAsync(ObservableCollection<Product> products)
        {

            try
            {

                var query = "Select * from [Assignment2_CF].[dbo].[Products]";

                var connection = new SqlConnection(GetConnectionString());
                connection.Open();

                var cmd = new SqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var _product = new Product
                    {
                        ProductID = reader.GetInt32(0)
                    };

                    if (!reader.IsDBNull(1))
                        _product.ProductName = reader.GetString(1);

                    if (!reader.IsDBNull(2))
                        _product.ProductPrice = reader.GetDouble(2);

                    if (!reader.IsDBNull(3))
                        _product.ProductColor = reader.GetString(3);

                    products.Add(_product);
                }

                connection.Close();
            }
            catch (Exception ex)
            {

                await HandleException(ex);
                throw;

            }

        }

        #endregion

        #region Async Task Add User
        public static async Task AddUser(string userName, string userRole)
        {
            if (IsFieldsEmpty(userName, userRole))
            {
                MessageBox.Show("User name and user role cannot be empty.");
                return;
            }

            string insertQuery = @"INSERT INTO [Assignment2_CF].[dbo].[Users] ([UserName], [UserRole]) VALUES (@UserName, @UserRole)";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (var cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@UserRole", userRole);

                        int result = await cmd.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            MessageBox.Show($"User {userName} was successfully added.");
                        }
                        else
                        {
                            MessageBox.Show("No user was added.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        #endregion

        #region Async Task Edit User
        public static async Task EditUser(int userID, string userName, string userRole)
        {
            if (IsFieldsEmpty(userName, userRole))
            {
                MessageBox.Show("User name and user role cannot be empty.");
                return;
            }

            string updateQuery = @"UPDATE [Assignment2_CF].[dbo].[Users] SET [UserName] = @UserName, [UserRole] = @UserRole WHERE [UserID] = @UserID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (var cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@UserRole", userRole);

                        int result = await cmd.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            MessageBox.Show($"User {userName} was successfully updated.");
                        }
                        else
                        {
                            MessageBox.Show($"User {userName} was not found or could not be updated.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        #endregion

        #region Async Task Delete User
        public static async Task DeleteUser(int userID)
        {
            string deleteQuery = @"DELETE FROM [Assignment2_CF].[dbo].[Users] WHERE [UserID] = @UserID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (var cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        int result = await cmd.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            MessageBox.Show($"User with ID {userID} was successfully deleted.");
                        }
                        else
                        {
                            MessageBox.Show($"User with ID {userID} was not found or could not be deleted.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        #endregion

        #region Async Task Add Product
        public static async Task AddProduct(string productName, string productPrice, string productColor)
        {
            if (IsProductsEmpty(productName, productPrice, productColor))
            {
                MessageBox.Show("Product name and price cannot be empty.");
                return;
            }

            string insertQuery = @"INSERT INTO [Assignment2_CF].[dbo].[Products] ([ProductName], [ProductPrice], [ProductColor]) VALUES (@ProductName, @ProductPrice, @ProductColor)";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@ProductPrice", productPrice);
                        cmd.Parameters.AddWithValue("@ProductColor", productColor);

                        int result = await cmd.ExecuteNonQueryAsync();
                        if (result > 0)
                        {
                            MessageBox.Show($"Product '{productName}' was successfully added.");
                        }
                        else
                        {
                            MessageBox.Show("No product was added.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        #endregion

        #region Async Task Edit Product
        public static async Task EditProduct(int productId, string productName, string productPrice, string productColor)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                MessageBox.Show("Product name cannot be empty.");
                return;
            }

            string updateQuery = @"UPDATE [Assignment2_CF].[dbo].[Products] SET [ProductName] = @ProductName, [ProductPrice] = @ProductPrice, [ProductColor] = @ProductColor WHERE [ProductID] = @ProductID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var cmd = new SqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@ProductPrice", productPrice);
                        cmd.Parameters.AddWithValue("@ProductColor", productColor);

                        int result = await cmd.ExecuteNonQueryAsync();
                        if (result > 0)
                        {
                            MessageBox.Show($"Product '{productName}' was successfully updated.");
                        }
                        else
                        {
                            MessageBox.Show("Product was not found or could not be updated.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        #endregion

        #region Async Task Delete Product
        public static async Task DeleteProduct(int productId)
        {
            string deleteQuery = @"DELETE FROM [Assignment2_CF].[dbo].[Products] WHERE [ProductID] = @ProductID";

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (var cmd = new SqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        int result = await cmd.ExecuteNonQueryAsync();
                        if (result > 0)
                        {
                            MessageBox.Show($"Product with ID {productId} was successfully deleted.");
                        }
                        else
                        {
                            MessageBox.Show($"Product with ID {productId} was not found or could not be deleted.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
        #endregion

    }
}
