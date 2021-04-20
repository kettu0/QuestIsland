using Microsoft.Extensions.Caching.Memory;
using Npgsql;
using QuestStore_C_of_thieves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.DAO
{
    public class TeamDAO : ConnectionDAO, IBaseDAO<Team>
    {
        public void CreateEntity(Team EntityToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(Team teamToDelete)
        {
            string sqlQuery = $"UPDATE codecoolers SET team_id = null WHERE team_id = {teamToDelete.TeamId}; DELETE FROM teams_quests WHERE team_id = {teamToDelete.TeamId}; DELETE FROM teams WHERE team_id = {teamToDelete.TeamId};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

         public List<Team> ReadAllEntities()
        {
            var allTeams = new List<Team>();
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            string sqlQuery = "SELECT * FROM teams;";
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allTeams.Add(new Team( reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDateTime(4)));
            }

            return allTeams;
        }

        public List<int> ReadUsersIdByTeamId (int id)
        {
            var usersId = new List<int>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();     
            string SQLquery = $"SELECT * FROM codecoolers WHERE team_id = '{id}';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();
            while(Reader.Read())
            {
                usersId.Add(Reader.GetInt16(3));
            }

            return usersId;
        }

        public List<User> ReadUsersByUserId(List<int> idNumbers)
        {
            var users = new List<User>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            foreach (int id in idNumbers)
            {
                string SQLquery = $"SELECT * FROM users WHERE user_id = '{id}';";
                using var CMD = new NpgsqlCommand(SQLquery, Connection);
                using NpgsqlDataReader Reader = CMD.ExecuteReader();
                Reader.Read();
                users.Add(new User(Reader.GetInt16(0), Reader.GetString(5), Reader.GetString(6), Reader.GetString(3)));

            }

            return users;
        }

        public List<int> ReadQuestsIdByTeamId(int id)
        {
            var questsId = new List<int>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM teams_quests WHERE team_id = '{id}';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();
            while (Reader.Read())
            {
                questsId.Add(Reader.GetInt16(2));
            }

            return questsId;
        }

        public List<Quest> ReadQuestsById(List<int> idNumbers)
        {
            var quests = new List<Quest>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            foreach (int id in idNumbers)
            {
                string SQLquery = $"SELECT * FROM quests WHERE quest_id = '{id}';";
                using var CMD = new NpgsqlCommand(SQLquery, Connection);
                using NpgsqlDataReader Reader = CMD.ExecuteReader();
                Reader.Read();
                quests.Add(new Quest(Reader.GetInt16(0), Reader.GetString(1), Reader.GetInt32(3), Reader.GetInt32(4)));

            }

            return quests;
        }
        public Team ReadEntityByID(int id)
        {
            string sqlQuery = $"SELECT * FROM teams WHERE team_id = '{id}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var teamById = (new Team(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(4)));
            return teamById;
        }

        public List<Team> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Team> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<Team> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Team EntityToUpdate)
        {
            throw new NotImplementedException();
        }

        public List<Team> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
