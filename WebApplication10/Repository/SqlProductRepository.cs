using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication10.Repository
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        //lista proizvoda
        private List<Product> products = new List<Product>();

        public SqlProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Product EditProduct(Product product)
        {
            var cs = _configuration.GetConnectionString("DefaultConnection");
            var con = new SqlConnection(cs);
            if (product.Id == 0)
            {
                try
                {
                   var command = new SqlCommand("AddProduct", con);
                   command.CommandType = System.Data.CommandType.StoredProcedure;
                   con.Open();

                   command.Parameters.AddWithValue("@Naziv", product.Naziv);
                   command.Parameters.AddWithValue("@Opis", product.Opis);
                   command.Parameters.AddWithValue("@Kategorija", product.Kategorija);
                   command.Parameters.AddWithValue("@Proizvodjac", product.Proizvodjac);
                   command.Parameters.AddWithValue("@Dobavljac", product.Dobavljac);
                   command.Parameters.AddWithValue("@Cena", product.Cena);
                   command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                try
                {
                    var command = new SqlCommand("EditProduct", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Naziv", product.Naziv);
                    command.Parameters.AddWithValue("@Opis", product.Opis);
                    command.Parameters.AddWithValue("@Kategorija", product.Kategorija);
                    command.Parameters.AddWithValue("@Proizvodjac", product.Proizvodjac);
                    command.Parameters.AddWithValue("@Dobavljac", product.Dobavljac);
                    command.Parameters.AddWithValue("@Cena", product.Cena);
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        
           
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {

            products.Clear();
            var cs = _configuration.GetConnectionString("DefaultConnection");
            var con = new SqlConnection(cs);
            try
            {
                con.Open();
                var command = new SqlCommand("SELECT * FROM Product", con);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Product
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Naziv = reader["Naziv"].ToString(),
                        Opis= reader["Opis"].ToString(),
                        Kategorija= reader["Kategorija"].ToString(),
                        Proizvodjac= reader["Proizvodjac"].ToString(),
                        Dobavljac= reader["Dobavljac"].ToString(),
                        Cena=int.Parse(reader["Cena"].ToString())
                    };
                    products.Add(p);
                }
                reader.Close();
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        public Product GetProductById(int Id)
        {
            return products.SingleOrDefault(p => p.Id == Id);
        }
    }
}
