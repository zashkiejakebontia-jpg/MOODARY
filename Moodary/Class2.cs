using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Moodary
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }

    public class JournalEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PlainTextContent { get; set; } = string.Empty;
        public string RichTextContent { get; set; } = string.Empty;
        public string Mood { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public string DisplayTitle
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Title))
                {
                    return Title.Trim();
                }

                if (!string.IsNullOrWhiteSpace(PlainTextContent))
                {
                    string preview = PlainTextContent.Replace(Environment.NewLine, " ").Trim();
                    return preview.Length > 28 ? preview.Substring(0, 28) + "..." : preview;
                }

                return "Untitled Entry";
            }
        }

        public string DisplayMoodEmoji
        {
            get
            {
                switch ((Mood ?? string.Empty).Trim().ToLowerInvariant())
                {
                    case "happy":
                        return "😊";
                    case "sad":
                        return "😢";
                    case "fear":
                        return "😨";
                    case "angry":
                        return "😠";
                    case "disgusted":
                    case "disgust":
                        return "🤢";
                    default:
                        return string.Empty;
                }
            }
        }
    }

    public static class SharedData
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUsername { get; set; } = string.Empty;
        public static BindingList<JournalEntry> JournalEntries { get; } = new BindingList<JournalEntry>();

        public static void SetCurrentUser(UserAccount user)
        {
            CurrentUserId = user.Id;
            CurrentUsername = user.Username;
        }

        public static void ReplaceJournalEntries(BindingList<JournalEntry> entries)
        {
            JournalEntries.Clear();
            foreach (JournalEntry entry in entries)
            {
                JournalEntries.Add(entry);
            }
        }

        public static void ClearSession()
        {
            CurrentUserId = 0;
            CurrentUsername = string.Empty;
            JournalEntries.Clear();
        }
    }

    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }

    public static class UserRepository
    {
        public static void CreateUser(string username, string passwordHash)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO users (username, password_hash)
                      VALUES (@username, @passwordHash);";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                cmd.ExecuteNonQuery();
            }
        }

        public static UserAccount GetUserByUsername(string username)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"SELECT id, username, password_hash
                      FROM users
                      WHERE username = @username
                      LIMIT 1;";
                cmd.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new UserAccount
                    {
                        Id = reader.GetInt32("id"),
                        Username = reader.GetString("username"),
                        PasswordHash = reader.GetString("password_hash")
                    };
                }
            }
        }

        public static void UpdateUsername(int userId, string newUsername)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"UPDATE users
                      SET username = @username
                      WHERE id = @userId;";
                cmd.Parameters.AddWithValue("@username", newUsername);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdatePassword(int userId, string passwordHash)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"UPDATE users
                      SET password_hash = @passwordHash
                      WHERE id = @userId;";
                cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteUser(int userId)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM users WHERE id = @userId;";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static class JournalRepository
    {
        public static BindingList<JournalEntry> GetEntriesForUser(int userId)
        {
            BindingList<JournalEntry> entries = new BindingList<JournalEntry>();

            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"SELECT id, user_id, title, content, rich_text_content, mood, created_at, updated_at
                      FROM journal_entries
                      WHERE user_id = @userId
                      ORDER BY updated_at DESC;";
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string content = reader["content"] == DBNull.Value ? string.Empty : reader["content"].ToString();
                        string richTextContent = reader["rich_text_content"] == DBNull.Value ? string.Empty : reader["rich_text_content"].ToString();
                        entries.Add(new JournalEntry
                        {
                            Id = reader.GetInt32("id"),
                            UserId = reader.GetInt32("user_id"),
                            Title = reader["title"] == DBNull.Value ? string.Empty : reader["title"].ToString(),
                            PlainTextContent = content,
                            RichTextContent = string.IsNullOrWhiteSpace(richTextContent) ? content : richTextContent,
                            Mood = reader["mood"] == DBNull.Value ? string.Empty : reader["mood"].ToString(),
                            CreatedAt = reader.GetDateTime("created_at"),
                            LastUpdated = reader.GetDateTime("updated_at")
                        });
                    }
                }
            }

            return entries;
        }

        public static JournalEntry SaveEntry(JournalEntry entry)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                if (entry.Id == 0)
                {
                    cmd.CommandText =
                        @"INSERT INTO journal_entries (user_id, title, content, rich_text_content, mood)
                          VALUES (@userId, @title, @content, @richTextContent, @mood);
                          SELECT LAST_INSERT_ID();";
                    cmd.Parameters.AddWithValue("@userId", entry.UserId);
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.PlainTextContent);
                    cmd.Parameters.AddWithValue("@richTextContent", string.IsNullOrWhiteSpace(entry.RichTextContent) ? (object)DBNull.Value : entry.RichTextContent);
                    cmd.Parameters.AddWithValue("@mood", string.IsNullOrWhiteSpace(entry.Mood) ? (object)DBNull.Value : entry.Mood);
                    entry.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    cmd.CommandText =
                        @"UPDATE journal_entries
                          SET title = @title,
                              content = @content,
                              rich_text_content = @richTextContent,
                              mood = @mood
                          WHERE id = @id AND user_id = @userId;";
                    cmd.Parameters.AddWithValue("@id", entry.Id);
                    cmd.Parameters.AddWithValue("@userId", entry.UserId);
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.PlainTextContent);
                    cmd.Parameters.AddWithValue("@richTextContent", string.IsNullOrWhiteSpace(entry.RichTextContent) ? (object)DBNull.Value : entry.RichTextContent);
                    cmd.Parameters.AddWithValue("@mood", string.IsNullOrWhiteSpace(entry.Mood) ? (object)DBNull.Value : entry.Mood);
                    cmd.ExecuteNonQuery();
                }
            }

            return GetEntryById(entry.Id, entry.UserId) ?? entry;
        }

        public static void DeleteEntry(int entryId, int userId)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM journal_entries WHERE id = @id AND user_id = @userId;";
                cmd.Parameters.AddWithValue("@id", entryId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }

        private static JournalEntry GetEntryById(int entryId, int userId)
        {
            using (MySqlConnection conn = DB.GetConnection())
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    @"SELECT id, user_id, title, content, rich_text_content, mood, created_at, updated_at
                      FROM journal_entries
                      WHERE id = @id AND user_id = @userId
                      LIMIT 1;";
                cmd.Parameters.AddWithValue("@id", entryId);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    string content = reader["content"] == DBNull.Value ? string.Empty : reader["content"].ToString();
                    string richTextContent = reader["rich_text_content"] == DBNull.Value ? string.Empty : reader["rich_text_content"].ToString();
                    return new JournalEntry
                    {
                        Id = reader.GetInt32("id"),
                        UserId = reader.GetInt32("user_id"),
                        Title = reader["title"] == DBNull.Value ? string.Empty : reader["title"].ToString(),
                        PlainTextContent = content,
                        RichTextContent = string.IsNullOrWhiteSpace(richTextContent) ? content : richTextContent,
                        Mood = reader["mood"] == DBNull.Value ? string.Empty : reader["mood"].ToString(),
                        CreatedAt = reader.GetDateTime("created_at"),
                        LastUpdated = reader.GetDateTime("updated_at")
                    };
                }
            }
        }
    }
}
