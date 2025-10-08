using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

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
        }

        public static List<User> GetAllUsers()
        {
            var users = new List<User>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, PhoneNumber, RegisteredAt, ExpiredAt, MoneyPerMonth FROM Users";
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
                    MoneyPerMonth = reader.GetDecimal(5)
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
                INSERT INTO Users (Name, PhoneNumber, RegisteredAt, ExpiredAt, MoneyPerMonth)
                VALUES ($name, $phone, $reg, $exp, $money)";
            command.Parameters.AddWithValue("$name", user.Name);
            command.Parameters.AddWithValue("$phone", user.PhoneNumber);
            command.Parameters.AddWithValue("$reg", user.RegisteredAt.ToString("o"));
            command.Parameters.AddWithValue("$exp", user.ExpiredAt.ToString("o"));
            command.Parameters.AddWithValue("$money", user.MoneyPerMonth);
            command.ExecuteNonQuery();
        }

        public static void UpdateUser(User user)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Users SET Name = $name, PhoneNumber = $phone, RegisteredAt = $reg, ExpiredAt = $exp, MoneyPerMonth = $money
                WHERE Id = $id";
            command.Parameters.AddWithValue("$id", user.Id);
            command.Parameters.AddWithValue("$name", user.Name);
            command.Parameters.AddWithValue("$phone", user.PhoneNumber);
            command.Parameters.AddWithValue("$reg", user.RegisteredAt.ToString("o"));
            command.Parameters.AddWithValue("$exp", user.ExpiredAt.ToString("o"));
            command.Parameters.AddWithValue("$money", user.MoneyPerMonth);
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
    }
}
