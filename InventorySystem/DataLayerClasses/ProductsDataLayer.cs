﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using InventorySystem.Models;

namespace InventorySystem.DataLayerClasses
{
    public class ProductsDataLayer
    {
        SqlConnection connection;
        SqlCommand command;

        public ProductsDataLayer(IConfiguration configuration)
        {
            String connectionString = configuration.GetConnectionString("localDB");
            connection = new SqlConnection(connectionString);
        }

        public void InsertProduct(Product product)
        {

                String toSearch = String.Format("INSERT INTO Products VALUES ({1}, {2}, {3}, {4}, {5}, {6})",
                    product.ProductID,
                    product.ProductSKU,
                    product.ProductName,
                    product.ProductBrand,
                    product.ProductMaterialsOrderID,
                    product.ProductPrice,
                    product.ProductVariableCost);

            command = new SqlCommand(toSearch, connection);    

                connection.Open();
                command.ExecuteNonQuery();
            connection.Close();
      
        }

        public IEnumerable<String> GetProductByName(string name)
        {
            List<String> products = new List<String>();
            String toSearch = String.Format("SELECT * FROM Products WHERE productnameid LIKE '%{0}%'", name);
            command = new SqlCommand(toSearch, connection);

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                { 
                    products.Add((String)reader["ProductNameId"]);
                }
            }

            connection.Close();
            return products;
        }

        public IEnumerable<Product> GetProduct(int? ProductID)
        {
            List<Product> products = new List<Product>();

            if (ProductID == null)
            {
                command = new SqlCommand("SELECT * FROM Products", connection);
            }
            else
            {
                command = new SqlCommand("SELECT * FROM Products WHERE ProductID = " + ProductID, connection);
            }

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product((int)Convert.ToInt64(reader["ProductID"]),
                         (String)reader["ProductSKUID"],
                         (String)reader["ProductNameID"],
                         (int)Convert.ToInt64(reader["ProductPriceID"]),
                         (int)Convert.ToInt64(reader["ProductVariableCostID"]),
                          (String)reader["ProductBrandID"],
                          (int)Convert.ToInt64(reader["ProductMaterialsOrderID"]));
                    products.Add(product);
                }
            }

            connection.Close();

            return products;
        }

    }
}
