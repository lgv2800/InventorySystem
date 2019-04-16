﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using InventorySystem.Models;

namespace InventorySystem.DataLayerClasses
{
    public class MaterialsDataLayer
    {
        SqlConnection connection;
        SqlCommand command;
        String connectionString;

        public MaterialsDataLayer(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("localDB");
            connection = new SqlConnection(connectionString);
        }

        public Material InsertMaterial(Material material)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureInsertMaterial", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaterialName", material.MaterialName);
                    command.Parameters.AddWithValue("@MaterialID_Output", material.MaterialID).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    material.MaterialID = Convert.ToInt32(command.Parameters["@MaterialID_Output"].Value);

                    /*
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                        //Throw error status code
                        */
                }

                return material;
            }
        }

        public Material UpdateMaterial(Material material)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureInsertMaterial", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaterialName", material.MaterialName);
                    command.Parameters.AddWithValue("@MaterialID_Output", material.MaterialID).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    material.MaterialID = Convert.ToInt32(command.Parameters["@MaterialID_Output"].Value);

                    /*
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                        //Throw error status code
                        */
                }

                return material;
            }
        }

        public Material DeleteMaterial(Material material)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("ProcedureInsertMaterial", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaterialName", material.MaterialName);
                    command.Parameters.AddWithValue("@MaterialID_Output", material.MaterialID).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    material.MaterialID = Convert.ToInt32(command.Parameters["@MaterialID_Output"].Value);

                    /*
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                        //Throw error status code
                        */
                }

                return material;
            }
        }

        public IEnumerable<Material> GetMaterial(int? MaterialID)
        {
            List<Material> materials = new List<Material>();

            if (MaterialID == null)
            {
                command = new SqlCommand("SELECT * FROM [ViewMaterials]", connection);
            }
            else
            {
                command = new SqlCommand("SELECT * FROM [ViewMaterials] WHERE MaterialID = '" + MaterialID + "'", connection);
            }

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Material material = new Material((int)Convert.ToInt64(reader["MaterialID"]),
                         //(String)reader["MaterialSKU"],
                         (String)reader["MaterialName"]);
                    materials.Add(material);
                }
            }

            connection.Close();

            return materials;
        }
    }
}
