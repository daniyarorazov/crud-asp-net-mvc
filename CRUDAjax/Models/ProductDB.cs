using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CRUDAjax.Models
{
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
                    });
                }
                return lst;
            }
        }

        //Method for Adding an Employee  
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
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating Employee record  
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
                com.Parameters.AddWithValue("@Age", prd.Category);
                com.Parameters.AddWithValue("@State", prd.Price);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an Employee  
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