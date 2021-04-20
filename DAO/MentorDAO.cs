using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestStore_C_of_thieves.Models;
using Npgsql;

namespace QuestStore_C_of_thieves.DAO
{
    public class MentorDAO : ConnectionDAO, IBaseDAO<Mentor>
    {
        public void CreateEntity(Mentor EntityToCreate)
        {
            string mainField = EntityToCreate.MainField;
            int userId = EntityToCreate.UserId;
            string sqlQuery = $"INSERT INTO mentors (main_field, user_id) VALUES ('{mainField}', {userId});";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateEntity(Mentor EntityToUpdate)
        {
            int mentorId = EntityToUpdate.MentorId;
            string mainField = EntityToUpdate.MainField;
            int userId = EntityToUpdate.UserId;
            string sqlQuery = $"UPDATE mentors SET main_field='{mainField}', user_id={userId} WHERE mentor_id={mentorId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(Mentor EntityToDelete)
        {
            int mentorId = EntityToDelete.UserId;
            string sqlQuery = $" DELETE FROM mentors_classes WHERE mentor_id = {mentorId}; DELETE FROM mentors WHERE mentor_id={mentorId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public void DeleteEntitiesByStringIntPair(string keyword, int value)
        {
            string sqlQuery = $"DELETE FROM mentors_classes WHERE mentor_id IN (SELECT mentor_id FROM mentors WHERE {keyword} = {value}); " +
                              $"DELETE FROM mentors WHERE {keyword} = {value};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public Mentor ReadEntityByID(int ID)
        {

            string sqlQuery = $"SELECT * FROM mentors WHERE mentor_id={ID};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var mentorById = new Mentor(reader.GetInt32(0), reader.GetString(2), reader.GetInt32(1));
            return mentorById;
        }

        public Mentor ReadEntityByUserID(int ID)
        {

            string sqlQuery = $"SELECT * FROM mentors WHERE user_id={ID};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var mentorById = new Mentor(reader.GetInt32(0), reader.GetString(2), reader.GetInt32(1));
            return mentorById;
        }

        public List<Mentor> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Mentor> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }
        public List<Mentor> ReadEntitiesByStringIntPair(string keyword, int value)
        {
            var keywordPairMentors = new List<Mentor>();
            string sqlQuery = $"SELECT * FROM mentors WHERE {keyword}={value};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                keywordPairMentors.Add(new Mentor(reader.GetInt32(0), reader.GetString(2), reader.GetInt32(1)));
            }
            return keywordPairMentors;
        }
        public List<Mentor> ReadAllEntities()
        {
            var allEntities = new List<Mentor>();
            string sqlQuery = $"SELECT * FROM mentors;";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allEntities.Add(new Mentor(reader.GetInt32(0), reader.GetString(2), reader.GetInt32(1)));
            }

            return allEntities;
        }

        public List<Mentor> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<Mentor> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }
    }
}
