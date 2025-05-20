using System;
using System.Data.Linq;
using System.IO;
using System.Reflection;

namespace DataLayer
{
    public static class DatabaseInitializer
    {
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();
        private static string _dbPath;

        public static void Initialize()
        {
            if (_isInitialized) return;

            lock (_lock)
            {
                if (_isInitialized) return;

                try
                {
                    // Set up database path
                    string appDataPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "ShopApp"
                    );
                    Directory.CreateDirectory(appDataPath);
                    _dbPath = Path.Combine(appDataPath, "ShopDatabase.mdf");

                    // Create database context
                    using (var db = new ShopDatabaseDataContext(GetConnectionString()))
                    {
                        // Create database if it doesn't exist
                        if (!db.DatabaseExists())
                        {
                            db.CreateDatabase();
                            Console.WriteLine("Database created successfully.");
                        }

                        // Create tables if they don't exist
                        CreateTablesIfNotExist(db);
                    }

                    _isInitialized = true;
                    Console.WriteLine("Database initialization completed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error initializing database: {ex.Message}");
                    throw;
                }
            }
        }

        private static void CreateTablesIfNotExist(ShopDatabaseDataContext db)
        {
            // Create ItemCatalog table if it doesn't exist
            if (!TableExists(db, "ItemCatalog"))
            {
                db.ExecuteCommand(@"
                    CREATE TABLE ItemCatalog (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(100) NOT NULL,
                        Price FLOAT NOT NULL
                    )");
                Console.WriteLine("ItemCatalog table created successfully.");
            }

            // Create UserData table if it doesn't exist
            if (!TableExists(db, "UserData"))
            {
                db.ExecuteCommand(@"
                    CREATE TABLE UserData (
                        Id BIGINT PRIMARY KEY,
                        Type INT NOT NULL
                    )");
                Console.WriteLine("UserData table created successfully.");
            }
        }

        private static bool TableExists(ShopDatabaseDataContext db, string tableName)
        {
            try
            {
                db.ExecuteCommand($"SELECT TOP 1 * FROM {tableName}");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetConnectionString()
        {
            return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_dbPath};Integrated Security=True";
        }
    }
} 