using System;
using MySql.Data.MySqlClient;

namespace Moodary
{
    public static class DB
    {
        private const string ConnectionString = "server=localhost;user=root;password=Zaniah2ndboy;database=moodary_app;";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();
            EnsureJournalSchema(conn);
            return conn;
        }

        private static void EnsureJournalSchema(MySqlConnection conn)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"SELECT COUNT(*)
                      FROM information_schema.COLUMNS
                      WHERE TABLE_SCHEMA = DATABASE()
                        AND TABLE_NAME = 'journal_entries'
                        AND COLUMN_NAME = 'rich_text_content';";

                int columnCount = Convert.ToInt32(cmd.ExecuteScalar());
                if (columnCount > 0)
                {
                    return;
                }
            }

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"ALTER TABLE journal_entries
                      ADD COLUMN rich_text_content LONGTEXT NULL AFTER content;";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
