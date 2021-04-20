using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestStore_C_of_thieves.Models;
using Npgsql;

namespace QuestStore_C_of_thieves.DAO
{
    public class CodecoolerDAO : ConnectionDAO, IBaseDAO<Codecooler>
    {
        public void CreateEntity(Codecooler EntityToCreate)
        {
            int codecoolerId = EntityToCreate.CodecoolerId;
            int teamId = EntityToCreate.TeamId;
            string teamIdQueryKey = teamId == default(int) ? "" : "team_id, ";
            string teamIdQueryValue = teamId == default(int) ? "" : $"{teamId},";
            int classId = EntityToCreate.ClassId;
            string classIdQueryKey = classId == default(int) ? "" : "class_id, ";
            string classIdQueryValue = classId == default(int) ? "" : $"{classId},";
            int coolpointsWallet = EntityToCreate.CoolpointsWallet;
            int exp = EntityToCreate.Exp;
            int userID = EntityToCreate.UserId;
            string sqlQuery = $"INSERT INTO codecoolers ({teamIdQueryKey} {classIdQueryKey} coolpoints_wallet, experience, user_id)" +
                              $"VALUES ({teamIdQueryValue} {classIdQueryValue} {coolpointsWallet}, {exp}, {userID});";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateEntity(Codecooler EntityToUpdate)
        {
            int codecoolerId = EntityToUpdate.CodecoolerId;
            int teamId = EntityToUpdate.TeamId;
            string teamIdQuery = teamId == default(int) ? "" : $"team_id={teamId}, ";
            int classId = EntityToUpdate.ClassId;
            string classIdQuery = classId == default(int) ? "" : $"class_id={classId}, ";
            int coolpointsWallet = EntityToUpdate.CoolpointsWallet;
            int exp = EntityToUpdate.Exp;
            int userID = EntityToUpdate.UserId;
            string sqlQuery = $"UPDATE codecoolers SET codecooler_id={codecoolerId}, {teamIdQuery} {classIdQuery}" +
                              $"coolpoints_wallet={coolpointsWallet}, user_id={userID}, experience='{exp}' WHERE codecooler_id={codecoolerId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(Codecooler EntityToDelete)
        {
            int codecoolerId = EntityToDelete.UserId;
            string sqlQuery = $"DELETE FROM codecoolers WHERE codecooler_id={codecoolerId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public void DeleteEntitiesByStringIntPair(string keyword, int value)
        {
            string sqlQuery = $"DELETE FROM codecoolers WHERE {keyword} = {value};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public Codecooler ReadEntityByID(int Id)
        {

            string sqlQuery = $"SELECT * FROM codecoolers WHERE codecooler_id={Id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var codecoolerById = new Codecooler(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
            return codecoolerById;
        }

        public Codecooler ReadEntityByUserID(int Id)
        {

            string sqlQuery = $"SELECT * FROM codecoolers WHERE user_id={Id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var codecoolerById = new Codecooler(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
            return codecoolerById;
        }

        public List<Codecooler> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Codecooler> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<Codecooler> ReadAllEntities()
        {
            var allEntities = new List<Codecooler>();
            string sqlQuery = $"SELECT * FROM codecoolers;";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allEntities.Add(new Codecooler(reader.GetInt32(0), reader["team_id"] == DBNull.Value ? 0 : (int)reader["team_id"],
                    reader["class_id"] == DBNull.Value ? 0 : (int)reader["class_id"], reader.GetInt32(3),
                    reader.GetInt32(4), reader.GetInt32(5)));
            }

            return allEntities;
        }
        public List<Codecooler> ReadEntitiesByStringIntPair(string keyword, int value)
        {
            var keywordPairCodecoolers = new List<Codecooler>();
            string sqlQuery = $"SELECT * FROM codecoolers WHERE {keyword}={value};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                keywordPairCodecoolers.Add(new Codecooler(reader.GetInt32(0), reader["team_id"] == DBNull.Value ? 0 : (int)reader["team_id"],
                    reader["class_id"] == DBNull.Value ? 0 : (int)reader["class_id"], reader.GetInt32(3),
                    reader.GetInt32(4), reader.GetInt32(5)));
            }
            return keywordPairCodecoolers;
        }

        public List<Codecooler> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<Codecooler> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }
    }
}
