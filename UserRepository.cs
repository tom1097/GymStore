using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GymStore1
{
    public static class UserRepository
    {
        private const string ConnectionString = "Data Source=gymstore.db";

        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    PhoneNumber TEXT NOT NULL,
                    RegisteredAt TEXT NOT NULL,
                    ExpiredAt TEXT NOT NULL,
                    MoneyPerMonth REAL NOT NULL
                );";
            command.ExecuteNonQuery();
            // Try to add UserImage column if it doesn't exist
            try
            {
                var alterCmd = connection.CreateCommand();
                alterCmd.CommandText = "ALTER TABLE Users ADD COLUMN UserImage TEXT;";
                alterCmd.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                // Ignore error if column already exists
                if (!ex.Message.Contains("duplicate column name"))
                    throw;
            }
        }

        public static List<User> GetAllUsers()
        {
            var users = new List<User>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, PhoneNumber, RegisteredAt, ExpiredAt, MoneyPerMonth, UserImage FROM Users";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PhoneNumber = reader.GetString(2),
                    RegisteredAt = DateTime.Parse(reader.GetString(3)),
                    ExpiredAt = DateTime.Parse(reader.GetString(4)),
                    MoneyPerMonth = reader.GetDecimal(5),
                    UserImage = !reader.IsDBNull(6) ? reader.GetString(6) : null
                });
            }
            return users;
        }

        public static void AddUser(User user)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Users (Name, PhoneNumber, RegisteredAt, ExpiredAt, MoneyPerMonth, UserImage)
                VALUES ($name, $phone, $reg, $exp, $money, $img)";
            command.Parameters.AddWithValue("$name", user.Name);
            command.Parameters.AddWithValue("$phone", user.PhoneNumber);
            command.Parameters.AddWithValue("$reg", user.RegisteredAt.ToString("o"));
            command.Parameters.AddWithValue("$exp", user.ExpiredAt.ToString("o"));
            command.Parameters.AddWithValue("$money", user.MoneyPerMonth);
            command.Parameters.AddWithValue("$img", user.UserImage ?? "");
            command.ExecuteNonQuery();
        }

        public static void UpdateUser(User user)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Users SET Name = $name, PhoneNumber = $phone, RegisteredAt = $reg, ExpiredAt = $exp, MoneyPerMonth = $money, UserImage = $img
                WHERE Id = $id";
            command.Parameters.AddWithValue("$id", user.Id);
            command.Parameters.AddWithValue("$name", user.Name);
            command.Parameters.AddWithValue("$phone", user.PhoneNumber);
            command.Parameters.AddWithValue("$reg", user.RegisteredAt.ToString("o"));
            command.Parameters.AddWithValue("$exp", user.ExpiredAt.ToString("o"));
            command.Parameters.AddWithValue("$money", user.MoneyPerMonth);
            command.Parameters.AddWithValue("$img", user.UserImage ?? "");
            command.ExecuteNonQuery();
        }

        public static void DeleteUser(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Users WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }

        public static void ConfigureDataGridView(DataGridView dgv)
        {
            // Remove Id column
            if (dgv.Columns["Id"] != null)
            {
                dgv.Columns["Id"].Visible = false;
            }

            // Add image column if not exists
            if (dgv.Columns["UserImage"] == null)
            {
                var imgCol = new DataGridViewImageColumn();
                imgCol.Name = "UserImage";
                imgCol.HeaderText = "Ảnh";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgv.Columns.Insert(0, imgCol);
            }

            // Set image for each row
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var user = row.DataBoundItem as User;
                if (user != null && !string.IsNullOrEmpty(user.UserImage) && System.IO.File.Exists(user.UserImage))
                    row.Cells["UserImage"].Value = System.Drawing.Image.FromFile(user.UserImage);
                else
                    row.Cells["UserImage"].Value = null;
            }
        }
    }
}
