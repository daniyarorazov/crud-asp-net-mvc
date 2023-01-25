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
    public class Image
    {
        public string ImageUrl { get; set; }
        public Image(string ImageUrl)
        {
            ImageUrl = ImageUrl;
        }
    }
    public class ProductDB
    {
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=CRUDAjax;Integrated Security=True");
        
        public List<Product> ListAll()
        {
            
            List<Product> lst = new List<Product>();
            
            using (con)
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectAllProducts", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
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
                return lst;
            }
        }

        public int Add(Product prd)
        {
            int i;
            using (con)
            {
                con.Open();
                SqlCommand com = new SqlCommand("AddNewProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", prd.ProductId);
                com.Parameters.AddWithValue("@Name", prd.Name);
                com.Parameters.AddWithValue("@Category", prd.Category);
                com.Parameters.AddWithValue("@Price", prd.Price);
                com.Parameters.AddWithValue("@ImageName", prd.ImageName);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int Update(Product prd)
        {
            int i;
            using (con)
            {
                con.Open();
                SqlCommand com = new SqlCommand("AddNewProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", prd.ProductId);
                com.Parameters.AddWithValue("@Name", prd.Name);
                com.Parameters.AddWithValue("@Category", prd.Category);
                com.Parameters.AddWithValue("@Price", prd.Price);
                com.Parameters.AddWithValue("@ImageName", prd.ImageName);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int Delete(int ID)
        {
            int i;
            using (con)
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
           

            return i;
        }
    }
}