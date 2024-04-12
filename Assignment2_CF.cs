using CSharp.WPF.ADO.ConnectionModels.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace CSharp.WPF.ADO.ConnectionModels
{
    public class Assignment2_CF : DbContext
    {
        // Your context has been configured to use a 'Assignment2_CF' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CSharp.WPF.ADO.ConnectionModels.Assignment2_CF' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Assignment2_CF' 
        // connection string in the application configuration file.
        public Assignment2_CF()
            : base("name=Assignment2_CF")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductColor { get; set; }
        public User User { get; set; }
    }
}