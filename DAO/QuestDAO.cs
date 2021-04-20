using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using QuestStore_C_of_thieves.Models;

namespace QuestStore_C_of_thieves.DAO
{
    public class QuestDAO : ConnectionDAO, IBaseDAO<Quest>
    {    
        public void CreateEntity(Quest EntityToCreate)
        {
            string questName = EntityToCreate.Name;
            string questDescription = EntityToCreate.Description;
            int questPrice = EntityToCreate.CoolpointsValue;
            int questExp = EntityToCreate.ExpValue;
            string questType = EntityToCreate.Type;
            string SQLQuery = $"INSERT INTO quests(quest_name, quest_type, coolpoints_value, exp_value, description) VALUES('{questName}', '{questType}', {questPrice}, {questExp}, '{questDescription}');";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(Quest EntityToDelete)
        {
            int questID = EntityToDelete.Id;
            string questName = EntityToDelete.Name;
            string questDescription = EntityToDelete.Description;
            int questPrice = EntityToDelete.CoolpointsValue;
            int questExp = EntityToDelete.ExpValue;
            string questType = EntityToDelete.Type;
            string SQLQuery = $"DELETE FROM quests WHERE quest_id={questID} AND quest_name='{questName}' AND quest_type='{questType}' AND coolpoints_value={questPrice} AND exp_value={questExp} AND description='{questDescription}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public int GenerateNewID()
        {
            throw new NotImplementedException();
        }

        public List<Quest> ReadAllEntities()
        {
            var allQuestsList = new List<Quest>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = "SELECT * FROM quests;";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                allQuestsList.Add(new Quest(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5)));
            }
            return allQuestsList;
        }

        public List<Quest> ReadEntitiesByKeyword(string keyword)
        {
            var questsByKeyword = new List<Quest>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM quests WHERE LOWER(quest_name) LIKE '%{keyword}%' OR LOWER(description) LIKE '%{keyword}%';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                questsByKeyword.Add(new Quest(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5)));
            }
            return questsByKeyword;
        }

        public Quest ReadEntityByID(int ID)
        {
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM quests WHERE quest_id = {ID};";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            Reader.Read();
            var questByID = new Quest(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5));
            return questByID;
        }

        public List<Quest> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Quest> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<Quest> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Quest EntityToUpdate)
        {
            int questID = EntityToUpdate.Id;
            string questName = EntityToUpdate.Name;
            string questDescription = EntityToUpdate.Description;
            int questPrice = EntityToUpdate.CoolpointsValue;
            int questExp = EntityToUpdate.ExpValue;
            string questType = EntityToUpdate.Type;
            string SQLQuery = $"UPDATE quests SET quest_name='{questName}', description ='{questDescription}', quest_type = '{questType}', coolpoints_value = {questPrice}, exp_value={questExp} WHERE quest_id={questID};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
