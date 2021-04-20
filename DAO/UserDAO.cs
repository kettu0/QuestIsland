using System;
using System.Collections.Generic;
using Npgsql;
using QuestStore_C_of_thieves.Models;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Xml.Linq;

namespace QuestStore_C_of_thieves.DAO
{
    public class UserDAO : ConnectionDAO, IBaseDAO<User>
    {
        public User NewUser { get; set; }

        public int GenerateNewID()
        {
            throw new NotImplementedException();
        }


        public void CreateEntity(User EntityToCreate)
        {
            string userName = EntityToCreate.Username;
            string password = EntityToCreate.Password;
            string email = EntityToCreate.Email;
            DateTime dateOfRegistration = EntityToCreate.DateOfRegistration.Date;
            string firstName = EntityToCreate.FirstName;
            string lastName = EntityToCreate.LastName;
            string gender = EntityToCreate.UserGender;
            string userRole = EntityToCreate.UserRole;
            var dtfi = CultureInfo.CreateSpecificCulture("pl-PL").DateTimeFormat;
            dtfi.DateSeparator = "-";
            string sqlQuery = "INSERT INTO users(username, password, email, date_of_registration, first_name, last_name, gender, user_role)" +
                $"VALUES('{userName}', '{password}', '{email}', '{dateOfRegistration.ToString("yyyy-MM-dd")}', '{firstName}', '{lastName}', '{gender}', '{userRole}');";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateEntity(User EntityToUpdate)
        {


            string queryID = EntityToUpdate.UserId == default(int) ? "" : $"user_id={EntityToUpdate.UserId}, ";
            string queryUsername = EntityToUpdate.Username == default(string) ? "" : $"username='{EntityToUpdate.Username}', ";
            string queryPassword = EntityToUpdate.Password == default(string) ? "" : $"password='{EntityToUpdate.Password}', ";
            string queryEmail = EntityToUpdate.Email == default(string) ? "" : $"email='{EntityToUpdate.Email}', ";
            string queryDateOfRegistration = EntityToUpdate.DateOfRegistration == default(DateTime)
                ? ""
                : $"date_of_registration='{EntityToUpdate.DateOfRegistration}', ";
            string queryFirstName = EntityToUpdate.FirstName == default(string)
                ? ""
                : $"first_name='{EntityToUpdate.FirstName}', ";
            string queryLastName = EntityToUpdate.LastName == default(string)
                ? ""
                : $"last_name='{EntityToUpdate.LastName}', ";
            string queryGender = EntityToUpdate.UserGender == default(string)
                ? ""
                : $"gender='{EntityToUpdate.UserGender}', ";
            string queryUserRole = EntityToUpdate.UserRole == default(string)
                ? ""
                : $"user_role='{EntityToUpdate.UserRole}', ";

            string sqlQuery = "UPDATE users SET " + queryID + queryUsername + queryPassword + queryEmail +
                              queryDateOfRegistration + queryFirstName + queryLastName + queryGender + queryUserRole;
            sqlQuery = sqlQuery.TrimEnd(',', ' ');
            sqlQuery += $" WHERE user_id={EntityToUpdate.UserId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdatePassword (User EntityToUpdate)
        {
            int userId = EntityToUpdate.UserId;
            string password = EntityToUpdate.Password;
         

            string SQLQuery = $"UPDATE users SET password ='{password}' WHERE user_id={userId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();

        }

        public void DeleteEntity(User EntityToDelete)
        {
            int id = EntityToDelete.UserId;

            string sqlQuery = $"DELETE FROM users WHERE user_id = {id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntityById(int id)
        {
            string sqlQuery = $"DELETE FROM users WHERE user_id = {id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public void DeleteEntitiesByStringIntPair(KeyValuePair<string, int> keywordPair)
        {
            string sqlQuery = $"DELETE FROM users WHERE {keywordPair.Key} = {keywordPair.Value};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public User ReadEntityByID(int Id)
        {
            string sqlQuery = $"SELECT * FROM users WHERE user_id={Id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var userByID = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                reader.GetString(6), reader.GetString(7), reader.GetString(8));
            return userByID;
        }

        public List<User> ReadEntityByName(string username)
        {
            var usersByName = new List<User>();
            string sqlQuery = $"SELECT * FROM users WHERE username = '{username}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                usersByName.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }
            return usersByName;
        }

        public List<User> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadEntitiesByStringPair(string keyword, string value)
        {
            var keywordPairUsers = new List<User>();
            string sqlQuery = $"SELECT * FROM users WHERE {keyword} = '{value}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                keywordPairUsers.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }
            return keywordPairUsers;
        }
        public List<User> ReadEntitiesByStringIntPair(KeyValuePair<string, int> keywordPair)
        {
            var keywordPairUsers = new List<User>();
            string sqlQuery = $"SELECT * FROM users WHERE {keywordPair.Key} = {keywordPair.Value};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                keywordPairUsers.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }
            return keywordPairUsers;
        }

        public List<User> ReadAllEntities()
        {
            var allUsers = new List<User>();
            const string sqlQuery = "SELECT * FROM users;";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allUsers.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }

            return allUsers;
        }

        public List<User> ReadAllMentors()
        {
            var allUsers = new List<User>();
            const string sqlQuery = "SELECT * FROM users WHERE user_role = 'M';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allUsers.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }

            return allUsers;
        }

        public List<User> SortAllEntitiesAlphabetically(string order)
        {
            var allUsersAlphabetically = new List<User>();
            string sqlQuery = "";
            switch (order.ToUpper())
            {
                case "ASC":
                    sqlQuery = "SELECT * FROM users ORDER BY username ASC;";
                    break;
                case "DESC":
                    sqlQuery = "SELECT * FROM users ORDER BY username DESC;";
                    break;
            }
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allUsersAlphabetically.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }

            return allUsersAlphabetically;
        }

        public List<User> SortAllEntitiesByIDs(string order)
        {
            var allUsersByID = new List<User>();
            string sqlQuery = "";
            switch (order.ToUpper())
            {
                case "ASC":
                    sqlQuery = "SELECT * FROM users ORDER BY user_id ASC;";
                    break;
                case "DESC":
                    sqlQuery = "SELECT * FROM users ORDER BY user_id DESC;";
                    break;
            }
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allUsersByID.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetDateTime(4), reader.GetString(5),
                    reader.GetString(6), reader.GetString(7), reader.GetString(8)));
            }
            return allUsersByID;
        }

        public bool LoginUser (User foreignUser)
        {
            string sqlQuerry = $"SELECT * FROM users WHERE username =  '{foreignUser.Username}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuerry, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                var newUser = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3),reader.GetDateTime(4), reader.GetString(5), reader.GetString(6),
                    reader.GetString(7), reader.GetString(8));
                NewUser = newUser;
                return true;
            }
            return false;
        }

        public int GetUserWalletById(int userId)
        {
            int coolpointsWallet = 0;
            string sqlQuery =
                $"SELECT c.coolpoints_wallet FROM users u INNER JOIN codecoolers c USING (user_id) WHERE u.user_id = {userId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                coolpointsWallet = reader.GetInt32(0);
            }

            return coolpointsWallet;
        }
    }
}
