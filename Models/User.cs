using System;
using System.ComponentModel.DataAnnotations;

namespace QuestStore_C_of_thieves.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserGender { get; set; }
        public string UserRole { get; set; }
        public bool IsAssigned { get; set; }

        public User(int userID, string userName, string password, string email, DateTime dateOfRegistration,
            string firstName, string lastName, string userGender, string userRole)
        {
            this.UserId = userID;
            this.Username = userName;
            this.Password = password;
            this.Email = email;
            this.DateOfRegistration = dateOfRegistration;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserGender = userGender;
            this.UserRole = userRole;
        }

        public User(int userID, string userName,  string userRole)
        {
            this.UserId = userID;
            this.Username = userName;
            this.UserRole = userRole;
        }
        public User(string userName, string password)
        {
            this.Username = userName;
            this.Password = password;
        }

        public User()
        {
        }

        public User(int userId, string firstName, string lastName, string email)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public User(int userId, bool isAssigned)
        {
            this.UserId = userId;
            this.IsAssigned = isAssigned;

        }
    }
}
