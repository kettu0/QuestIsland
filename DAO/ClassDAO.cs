using Microsoft.Extensions.Caching.Memory;
using Npgsql;
using QuestStore_C_of_thieves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.DAO
{
    public class ClassDAO : ConnectionDAO, IBaseDAO<Class>
    {
        public void CreateEntity(Class entityToCreate)
        {
            string className = entityToCreate.ClassName;
            DateTime startDate = entityToCreate.StartDate;

            string sqlQuery = $"INSERT INTO classes(class_name, start_date) VALUES ('{className}', '{startDate.ToString("yyyy-MM-dd")}');";    
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public void DeleteEntity(Class classToDelete)
        {
            string sqlQuery = $"DELETE FROM mentors_classes WHERE class_id = {classToDelete.Id}; UPDATE codecoolers SET class_id = NULL WHERE class_id = {classToDelete.Id}; DELETE FROM classes WHERE class_id = {classToDelete.Id};";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }

        public List<User> ReadAllMentors()
        {
            var mentors = new List<User>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM users WHERE user_role = 'M';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                mentors.Add(new User(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3)));
            }
            return mentors;
        }

        public List<Mentor> ReadMentorsById(List<User> mentors)
        {
            var mentorsList = new List<Mentor>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            foreach(var mentor in mentors)
            {
                string SQLquery = $"SELECT * FROM mentors WHERE user_id = '{mentor.UserId}';";
                using var CMD = new NpgsqlCommand(SQLquery, Connection);
                using NpgsqlDataReader Reader = CMD.ExecuteReader();

                while (Reader.Read())
                {
                    mentors.Add(new User(Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3)));
                }

            }
            
            return mentorsList;
        }

        public List<int> ReadMentorsIdByClassId(int id)
        {
            var mentorsId = new List<int>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM mentors_classes WHERE class_id = '{id}';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();
            while(Reader.Read())
            {
                mentorsId.Add(Reader.GetInt16(0));
            }
            return mentorsId;
        
        }

        public List<int> ReadUsersIdByMentorsId(List<int> mentorsId)
        {
            var usersId = new List<int>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            foreach(int id in mentorsId)
            { 
                string SQLquery = $"SELECT * FROM mentors WHERE mentor_id = '{id}';";
                using var CMD = new NpgsqlCommand(SQLquery, Connection);
                using NpgsqlDataReader Reader = CMD.ExecuteReader();
                Reader.Read();
                usersId.Add(Reader.GetInt16(1));

            }
            
            return usersId;

        }

        public List<User> ReadUsersById(List<int> usersId)
        {
            var users = new List<User>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            foreach (int id in usersId)
            {
                string SQLquery = $"SELECT * FROM users WHERE user_id = '{id}';";
                using var CMD = new NpgsqlCommand(SQLquery, Connection);
                using NpgsqlDataReader Reader = CMD.ExecuteReader();
                Reader.Read();
                users.Add(new User(Reader.GetInt16(0), Reader.GetString(5), Reader.GetString(6), Reader.GetString(3)));

            }

            return users;
        }


        public List<Codecooler> ReadAllCodecoolersFromClass(Class pickedClass)
        {
            var entitiesByClassId = new List<Codecooler>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            string SQLquery = $"SELECT * FROM codecoolers WHERE class_id = '{pickedClass.Id}';";
            using var CMD = new NpgsqlCommand(SQLquery, Connection);
            using NpgsqlDataReader Reader = CMD.ExecuteReader();

            while (Reader.Read())
            {
                entitiesByClassId.Add(new Codecooler(Reader.GetInt32(0), Reader.GetInt16(1), Reader.GetInt16(2), Reader.GetInt32(3), Reader.GetInt32(4), Reader.GetInt32(5)));
            }
            return entitiesByClassId;
        }

        public List<User> ReadUsersFromList(List<Codecooler> codecoolers)
        {
            var users = new List<User>();
            using var Connection = new NpgsqlConnection(conn);
            Connection.Open();
            foreach(var codecooler in codecoolers)
            {
                string SQLquery = $"SELECT * FROM users WHERE user_id = '{codecooler.UserId}';";
                 using var CMD = new NpgsqlCommand(SQLquery, Connection);
                using NpgsqlDataReader Reader = CMD.ExecuteReader();

                while (Reader.Read())
                {
                    users.Add(new User(Reader.GetInt32(0), Reader.GetString(5), Reader.GetString(6), Reader.GetString(3)));
                }

            }
           
            return users;
        }
        public List<Class> ReadAllEntities()
        {
            var allClasses = new List<Class>();
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            string sqlQuery = "SELECT * FROM classes;";
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allClasses.Add(new Class( reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2)));
            }

            return allClasses;
        }

        public void AddCodecoolersToClass(List<Codecooler> members, Class codecoolersClass)
        {
            using var Connection = new NpgsqlConnection(conn);
            string SQLquery = "";
            Connection.Open();
            foreach(Codecooler member in members)
            {
                SQLquery += $"UPDATE codecoolers SET codecooler_id={member.CodecoolerId}, team_id={member.TeamId}, class_id={codecoolersClass.Id}," +
                                $"user_id={member.UserId}, coolpoints_wallet={member.CoolpointsWallet}, experience={member.Exp} WHERE codecooler_id={member.CodecoolerId};";
            }
            using var cmd = new NpgsqlCommand(SQLquery, Connection);
            cmd.ExecuteNonQuery();
        }

        public List<Class> ReadEntitiesByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public Class ReadEntityByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Class> ReadEntityByName(string name)
        {
            throw new NotImplementedException();
        }
        public Class ReadClassByName(string className)
        {
            var classByName = new Class();
            string sqlQuery = $"SELECT * FROM classes WHERE class_name = '{className}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            while (reader.Read())
            {
                classByName = (new Class(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2)));
            }
            return classByName;
        }

        public Class ReadClassById(int id)
        {
            string sqlQuery = $"SELECT * FROM classes WHERE class_id = '{id}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            var classById = (new Class(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2)));
            return classById;

        }

        public int ReadClassId(Class newClass)
        {
            string sqlQuery = $"SELECT * FROM classes WHERE class_name = '{newClass.ClassName}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int newClassId = reader.GetInt32(0);
            return newClassId;
        }

        public int ReadMentorIdByUserId(int userId)
        {
            string sqlQuery = $"SELECT * FROM mentors WHERE user_id = '{userId}';";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int mentorId = reader.GetInt32(0);
            return mentorId;
        }

        public void AssignMentorsToClass(int mentorId, int newClassId,  DateTime assignmentDate)
        {
            string sqlQuery = $"INSERT INTO mentors_classes (mentor_id, class_id, assignment_date) VALUES ('{mentorId}', '{newClassId}', '{assignmentDate.ToString("yyyy-MM-dd")}');";
            using var connection = new NpgsqlConnection(conn);
            connection.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connection);
            cmd.ExecuteNonQuery();
        }
        public List<Class> SortAllEntitiesAlphabetically(string order)
        {
            throw new NotImplementedException();
        }

        public List<Class> SortAllEntitiesByIDs(string order)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Class EntityToUpdate)
        {
            throw new NotImplementedException();
        }


    }
}
