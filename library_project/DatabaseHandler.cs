using MySql.Data.MySqlClient;

namespace library_project
{
    class DatabaseHandler
    {
        string connectionString = "server=localhost;port=3306;database=librarydb;user=root;password=root";

        public MySqlConnection MakeConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error connecting to database: {ex}"); 
                return null;    
            }     
        }   
    }
}