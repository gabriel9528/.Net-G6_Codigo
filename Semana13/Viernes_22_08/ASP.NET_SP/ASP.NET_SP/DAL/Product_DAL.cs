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
        public List<Product>? GetAllProducts()
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

        //GetProductById
        public Product? GetProductById(int id)
        {
            Product product = new();
            using(SqlConnection connection = 
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetProductById";

                command.Parameters.AddWithValue("@ProductId", id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtProduct = new DataTable();

                connection.Open();
                adapter.Fill(dtProduct);
                connection.Close();
                connection.Dispose();

                if (dtProduct.Rows.Count == 0)
                {
                    return product;
                }
                else
                {
                    DataRow item = dtProduct.Rows[0];
                    product.ProductId = Convert.ToInt32(item["ProductId"]);
                    product.ProductName = item["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(item["Price"]);
                    product.Quantity = Convert.ToInt32(item["Quantity"]);
                    product.Remarks = item["Remarks"].ToString();
                }
            }
            return product;
        }

        //InsertProducts
        public bool InsertProduct(Product product)
        {
            int id = 0;
            using (SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = new SqlCommand("sp_InsertProducts", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();

                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }

        //UpdateProducts
        public bool UpdateProduct(Product product)
        {
            int id = 0;
            using (SqlConnection connection =
               new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = new SqlCommand("sp_UpadteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();

                if (id > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
    }
}
