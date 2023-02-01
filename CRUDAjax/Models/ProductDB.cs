using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Xml.Linq;
using System.Web.Mvc;

namespace CRUDAjax.Models
{
    // Class definition for an Image
    public class Image
    {
        // ImageUrl property of type string with get and set accessors
        public string ImageUrl { get; set; }
        // Constructor for the Image class, taking a string parameter
        public Image(string ImageUrl)
        {
            // Parameter to the ImageUrl property of the class
            ImageUrl = ImageUrl;
        }
    }

    // Class definition for an ProductDB
    public class ProductDB
    {      
        // Create connection to SQL Database 
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=CRUDAjax;Integrated Security=True");
        
        // List with model Product, purpose is show all elements from table Products
        public List<Product> ListAll()
        {
            // Create variable
            List<Product> lst = new List<Product>();
            // Use a using statement to manage the lifetime of the SqlConnection 'con'
            using (con)
            {
                // Open access to table in database
                con.Open();

                 // Create a SqlCommand object to run the "SelectAllProducts" stored procedure
                SqlCommand com = new SqlCommand("SelectAllProducts", con);
                
                // Set the CommandType property of the SqlCommand to indicate that it is a stored procedure
                com.CommandType = CommandType.StoredProcedure;
                
                // Execute the stored procedure and get the results in a SqlDataReader
                SqlDataReader rdr = com.ExecuteReader();
                
                // Loop through the results, creating a new Product object for each record and adding it to the list
                while (rdr.Read())
                {
                    lst.Add(new Product
                    {
                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                        Name = rdr["Name"].ToString(),
                        Category = rdr["Category"].ToString(),
                        Price = Convert.ToInt32(rdr["Price"]).ToString(),
                        ImageName = rdr["ImageName"].ToString(),
                    });
                }
                // Return the list of Product objects
                return lst;
            }
        }

        // Add a new product to the database using a stored procedure
        public int Add(Product prd)
        {
            int i;
            // Open and use a database connection
            using (con)
            {
                con.Open();
                // Create a new SqlCommand to call the stored procedure "AddNewProduct"
                SqlCommand com = new SqlCommand("AddNewProduct", con);
                // Set the type of the command to a stored procedure
                com.CommandType = CommandType.StoredProcedure;
                // Add the product's properties as parameters to the command
                com.Parameters.AddWithValue("@Id", prd.ProductId);
                com.Parameters.AddWithValue("@Name", prd.Name);
                com.Parameters.AddWithValue("@Category", prd.Category);
                com.Parameters.AddWithValue("@Price", prd.Price);
                com.Parameters.AddWithValue("@ImageName", prd.ImageName);
                com.Parameters.AddWithValue("@Action", "Insert");
                // Execute the stored procedure and get the number of rows affected
                i = com.ExecuteNonQuery();
            }
            // Return the number of rows affected
            return i;
        }

        // Update product with using a stored procedure
        public int Update(Product prd)
        {
            int i;
            // Open and use a database connection
            using (con)
            {
                con.Open();
                // Create a new SqlCommand to call the stored procedure "AddNewProduct"
                SqlCommand com = new SqlCommand("AddNewProduct", con);
                // Set the type of the command to a stored procedure
                com.CommandType = CommandType.StoredProcedure;
                // Update the product's properties as parameters to the command
                com.Parameters.AddWithValue("@Id", prd.ProductId);
                com.Parameters.AddWithValue("@Name", prd.Name);
                com.Parameters.AddWithValue("@Category", prd.Category);
                com.Parameters.AddWithValue("@Price", prd.Price);
                com.Parameters.AddWithValue("@ImageName", prd.ImageName);
                com.Parameters.AddWithValue("@Action", "Update");
                // Execute the stored procedure and get the number of rows affected
                i = com.ExecuteNonQuery();
            }
            // Return the number of rows affected
            return i;
        }

        // Delete product by id using a stored procedure
        public int Delete(int ID)
        {
            int i;
            // Open and use a database connection
            using (con)
            {
                con.Open();
                // Create a new SqlCommand to call the stored procedure "DeleteProduct"
                SqlCommand com = new SqlCommand("DeleteProduct", con);
                // Set the type of the command to a stored procedure
                com.CommandType = CommandType.StoredProcedure;
                // Add the product's ID as a parameter to the command
                com.Parameters.AddWithValue("@Id", ID);
                // Execute the stored procedure and get the number of rows affected
                i = com.ExecuteNonQuery();
            }
           
            // Return the number of rows affected
            return i;
        }
    }
}