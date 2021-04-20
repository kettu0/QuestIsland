using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestStore_C_of_thieves.Models;
using Npgsql;

namespace QuestStore_C_of_thieves.DAO
{

    public class CodecoolerArtifactDAO : ConnectionDAO, IBaseDAO<CodecoolerArtifact>
    {
        public void CreateEntity(CodecoolerArtifact EntityToCreate)
        {
            int codecoolerId = EntityToCreate.CodecoolerID;
            int artifactId = EntityToCreate.ArtifactId;
            bool isUsed = EntityToCreate.IsUsed;
            string transactionDate = EntityToCreate.TransactionDate.ToString("dd.MM.yyyy HH:mm:ss");
            string SQLQuery = $"INSERT INTO codecoolers_artifacts(artifact_id, codecooler_id, transaction_date, is_used) VALUES({artifactId}, {codecoolerId}, '{transactionDate}', {isUsed});";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(CodecoolerArtifact EntityToDelete)
        {
            string SQLQuery = $"DELETE FROM codecoolers_artifacts WHERE codecooler_artifact_id = {EntityToDelete.Id});";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public List<CodecoolerArtifact> ReadAllEntities()
        {
            var allEntitesList = new List<CodecoolerArtifact>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = "SELECT * FROM codecoolers_artifacts;";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                allEntitesList.Add(new CodecoolerArtifact(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2), Reader.GetDateTime(3), Reader.GetBoolean(4)));
            }
            return allEntitesList;
        }

        public List<CodecoolerArtifact> ReadAllEntitiesByCodecoolerID(int id)
        {
            var allEntitesList = new List<CodecoolerArtifact>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM codecoolers_artifacts WHERE codecooler_id = {id};";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                allEntitesList.Add(new CodecoolerArtifact(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2), Reader.GetDateTime(3), Reader.GetBoolean(4)));
            }
            return allEntitesList;
        }


        public CodecoolerArtifact ReadEntityByID(int ID)
        {
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM codecoolers_artifacts WHERE codecooler_artifact_id = {ID};";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            Reader.Read();
            var codecoolerArtifactById = new CodecoolerArtifact(Reader.GetInt32(0), Reader.GetInt32(1), Reader.GetInt32(2), Reader.GetDateTime(3), Reader.GetBoolean(4));
            return codecoolerArtifactById;
        }

        public void UpdateEntity(CodecoolerArtifact EntityToUpdate)
        {
            int codecoolerId = EntityToUpdate.CodecoolerID;
            int artifactId = EntityToUpdate.ArtifactId;
            bool isUsed = EntityToUpdate.IsUsed;
            string SQLQuery = $"UPDATE codecoolers_artifacts SET artifact_id = {artifactId}, codecooler_id = {codecoolerId}, is_used = {isUsed} WHERE codecooler_artifact_id={EntityToUpdate.Id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }


        public List<CodecoolerArtifact> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<CodecoolerArtifact> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }


        public List<CodecoolerArtifact> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<CodecoolerArtifact> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
