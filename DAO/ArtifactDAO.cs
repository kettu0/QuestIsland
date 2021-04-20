using QuestStore_C_of_thieves.Models;
using System;
using System.Collections.Generic;
using Npgsql;

namespace QuestStore_C_of_thieves.DAO
{
    public class ArtifactDAO : ConnectionDAO, IBaseDAO<Artifact>
    {

        public void CreateEntity(Artifact EntityToCreate)
        {
            string artifactName = EntityToCreate.Name;
            string artifactDescription = EntityToCreate.Description;
            int artifactPrice = EntityToCreate.Price;
            int artifactAmount = EntityToCreate.Amount;
            string artifactType = EntityToCreate.Type;
            string artifactImage = EntityToCreate.Image;
            string SQLQuery = $"INSERT INTO artifacts(artifact_name, artifact_type, price, available_amount, description, image) VALUES('{artifactName}', '{artifactType}', {artifactPrice}, {artifactAmount}, '{artifactDescription}', '{artifactImage}.png');";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(Artifact EntityToDelete)
        {
            int artifactID = EntityToDelete.Id;
            string artifactName = EntityToDelete.Name;
            string artifactDescription = EntityToDelete.Description;
            int artifactPrice = EntityToDelete.Price;
            int artifactAmount = EntityToDelete.Amount;
            string artifactType = EntityToDelete.Type;
            string artifactImage = EntityToDelete.Image;
            string SQLQuery = $"DELETE FROM artifacts WHERE artifact_id={artifactID} AND artifact_name='{artifactName}' AND artifact_type='{artifactType}' AND price={artifactPrice} AND available_amount={artifactAmount} AND description='{artifactDescription}' AND image='{artifactImage}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateEntity(Artifact EntityToUpdate)
        {
            int artifactID = EntityToUpdate.Id;
            string artifactName = EntityToUpdate.Name;
            string artifactDescription = EntityToUpdate.Description;
            int artifactPrice = EntityToUpdate.Price;
            int artifactAmount = EntityToUpdate.Amount;
            string artifactType = EntityToUpdate.Type;
            string artifactImage = EntityToUpdate.Image;
            string SQLQuery = $"UPDATE artifacts SET artifact_name='{artifactName}', description ='{artifactDescription}', artifact_type= '{artifactType}', price = {artifactPrice}, available_amount={artifactAmount}, image='{artifactImage}' WHERE artifact_id={artifactID};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(SQLQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public List<Artifact> ReadAllEntities()
        {
            var allEntitesList = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = "SELECT * FROM artifacts ORDER BY artifact_id ASC;";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                allEntitesList.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }
            return allEntitesList;
        }

        public List<Artifact> ReadEntitiesByKeyword(string keyword)
        {
            var entitiesByKeyword = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM artifacts WHERE LOWER(artifact_name) LIKE LOWER('%{keyword}%') OR LOWER(description) LIKE LOWER('%{keyword}%');";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                entitiesByKeyword.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }
            return entitiesByKeyword;
        }

        public List<Artifact> ReadEntityByType(string type)
        {
            var entitiesByType = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM artifacts WHERE artifact_type = '{type}';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                entitiesByType.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }
            return entitiesByType;
        }

        public Artifact ReadEntityByID(int ID)
        {
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM artifacts WHERE artifact_id = {ID};";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            Reader.Read();
            var artifactByID = new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6));
            return artifactByID;
        }

        public List<Artifact> ReadEntityByName(string name)
        {
            var entitiesByName= new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM artifacts WHERE artifact_name = '{name}';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read()) 
            {
                entitiesByName.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }
            return entitiesByName;
        }

        public List<Artifact> SortAllEntitiesAlphabetically(string order)
        {
            var artifactsSorted = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            string SQLquery = "";
            Connection.Open();

            if (order == "ASC")
            {
                SQLquery += $"SELECT * FROM artifacts ORDER BY artifact_name ASC";
            }
            else if (order == "DESC")
            {
                SQLquery += $"SELECT * FROM artifacts ORDER BY artifact_name DESC";
            }

            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                artifactsSorted.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }

            return artifactsSorted;
        }

        public List<Artifact> SortAllEntitiesByIDs(string order)
        {
            var artifactsSorted = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            string SQLquery = "";
            Connection.Open();

            if (order == "ASC")
            {
                SQLquery += $"SELECT * FROM artifacts ORDER BY artifact_id ASC";
            }
            else if (order == "DESC")
            {
                SQLquery += $"SELECT * FROM artifacts ORDER BY artifact_id DESC";
            }

            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                artifactsSorted.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }

            return artifactsSorted;
        }

        public List<Artifact> SortAllEntitiesByPrice(string order)
        {
            var artifactsSorted = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            string SQLquery = "";
            Connection.Open();

            if (order == "ASC")
            {
                SQLquery += $"SELECT * FROM artifacts ORDER BY price ASC";
            }
            else if (order == "DESC")
            {
                SQLquery += $"SELECT * FROM artifacts ORDER BY price DESC";
            }

            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                artifactsSorted.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }

            return artifactsSorted;
        }

        public List<Artifact> SortEntitiesByType(string type, string orderParam, string order)
        {
            var artifactsSorted = new List<Artifact>();
            using var Connection = new NpgsqlConnection(conn);
            string SQLquery = $"SELECT * FROM artifacts where artifact_type = '{type}' order by {orderParam} {order};";
            Connection.Open();
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                artifactsSorted.Add(new Artifact(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetString(5), Reader.GetString(6)));
            }

            return artifactsSorted;
        }


    }


}

