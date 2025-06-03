using System;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public static class DatabaseInitializer
    {
        private static string _connectionString;

        public static void Initialize()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDir, "DataShop.mdf");
            _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";

            if (!File.Exists(dbPath))
            {
                CreateDatabase();
            }
        }

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        private static void CreateDatabase()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDir, "DataShop.mdf");
            string logPath = Path.Combine(baseDir, "DataShop_log.ldf");

            // Create the database files
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True"))
            {
                connection.Open();

                // Create the database
                string createDbQuery = $@"
                    CREATE DATABASE DataShop
                    ON PRIMARY (
                        NAME = DataShop,
                        FILENAME = '{dbPath}'
                    )
                    LOG ON (
                        NAME = DataShop_log,
                        FILENAME = '{logPath}'
                    )";

                using (var command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Switch to the new database
                using (var command = new SqlCommand("USE DataShop", connection))
                {
                    command.ExecuteNonQuery();
                }

                // Read and execute the SQL script
                string sqlScript = File.ReadAllText(Path.Combine(baseDir, "CreateDatabase.sql"));
                using (var command = new SqlCommand(sqlScript, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
} 