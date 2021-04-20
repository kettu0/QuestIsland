using QuestStore_C_of_thieves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace QuestStore_C_of_thieves.DAO
{
    public class CodecoolerQuestDAO : ConnectionDAO, IBaseDAO<CodecoolerQuest>
    {
        public void CreateEntity(CodecoolerQuest EntityToCreate)
        {
            int codecoolerId = EntityToCreate.CodecoolerId;
            int questId = EntityToCreate.QuestId;
            bool isCompleted = EntityToCreate.IsCompleted;
            string SQLQuery = $"INSERT INTO codecoolers_quests(quest_id, codecooler_id, is_completed) VALUES({questId}, {codecoolerId}, {isCompleted});";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(CodecoolerQuest EntityToDelete)
        {
            throw new NotImplementedException();
        }

        public List<CodecoolerQuest> ReadAllEntities()
        {
            var allQuestsList = new List<CodecoolerQuest>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM codecoolers_quests ORDER BY completion_date DESC;";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                allQuestsList.Add(new CodecoolerQuest(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2),
                    Reader.IsDBNull(3)
                    ? (int?)null
                    : Reader.GetInt32(3),
                    Reader.GetBoolean(4),
                    Reader.IsDBNull(5)
                    ? default
                    : Reader.GetDateTime(5)));
            }
            return allQuestsList;
        }

        public List<CodecoolerQuest> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public CodecoolerQuest ReadEntityByID(int ID)
        {
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM codecoolers_quests WHERE codecooler_quest_id = {ID};";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            Reader.Read();
            var codecoolerQuestByID = new CodecoolerQuest(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2),
                    Reader.IsDBNull(3)
                    ? (int?)null
                    : Reader.GetInt32(3),
                    Reader.GetBoolean(4),
                    Reader.IsDBNull(5)
                    ? default
                    : Reader.GetDateTime(5));
            return codecoolerQuestByID;
        }

        public List<CodecoolerQuest> ReadEntitiesByCodecoolerID(int id)
        {
            var allQuestsList = new List<CodecoolerQuest>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM codecoolers_quests WHERE codecooler_id = {id} ORDER BY completion_date DESC;";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                allQuestsList.Add(new CodecoolerQuest(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2),
                    Reader.IsDBNull(3)
                    ? (int?)null
                    : Reader.GetInt32(3), 
                    Reader.GetBoolean(4),
                    Reader.IsDBNull(5)
                    ? default
                    : Reader.GetDateTime(5)));
            }
            return allQuestsList;
        }

        public List<CodecoolerQuest> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<CodecoolerQuest> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<CodecoolerQuest> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(CodecoolerQuest EntityToUpdate)
        {
            int id = EntityToUpdate.Id;
            int questId = EntityToUpdate.QuestId;
            int codecoolerId = EntityToUpdate.CodecoolerId;
            var approverId = EntityToUpdate.ApproverId;
            bool isCompleted = EntityToUpdate.IsCompleted;
            string completionDate = EntityToUpdate.CompletionDate.ToString("dd.MM.yyyy HH:mm:ss");
            string query;
            if (approverId != null)
            {
                query = $"UPDATE codecoolers_quests SET quest_id={questId}, codecooler_id={codecoolerId}, accepting_mentor_id='{approverId}', is_completed={isCompleted}, completion_date='{completionDate}' WHERE codecooler_quest_id={id};";
            }
            else
            {
                query = $"UPDATE codecoolers_quests SET quest_id={questId}, codecooler_id={codecoolerId}, is_completed={isCompleted}, completion_date='{completionDate}' WHERE codecooler_quest_id={id};";
            }
            string SQLQuery = query;
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }
    }
}
