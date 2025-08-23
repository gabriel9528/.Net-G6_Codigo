using System.Data;
using System.Data.SqlClient;
using ASP.NET_SP.Models;

namespace ASP.NET_SP.DAL
{
    public class Product_DAL : IProduct_DAL
    {
        private readonly IConfiguration _configuration;
        public Product_DAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //GetAllProducts
        public List<Product> GetAllProducts()
        {
            List<Product> listProducts = new List<Product>();
            using (SqlConnection connection = 
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetALLProducts";

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();

                connection.Open();
                adapter.Fill(dtProducts);
                connection.Close();
                connection.Dispose();

                if(dtProducts.Rows.Count == 0)
                {
                    return listProducts;
                }
                else
                {
                    foreach (DataRow item in dtProducts.Rows)
                    {
                        Product product = new Product
                        {
                            ProductId = Convert.ToInt32(item["ProductId"]),
                            ProductName = item["ProductName"].ToString(),
                            Price = Convert.ToDecimal(item["Price"]),
                            Quantity = Convert.ToInt32(item["Quantity"]),
                            Remarks = item["Remarks"].ToString()
                        };
                        listProducts.Add(product);
                    }
                }

                

                return listProducts;
            }
        }
    }
}
