using MySql.Data.MySqlClient;

public static class DB
{
    private static string connStr = "server=localhost;user=root;password=Zaniah2ndboy;database=moodaryDB;";

    public static MySqlConnection GetConnection()
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();
        return conn;
    }
}