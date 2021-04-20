using System;

namespace QuestStore_C_of_thieves.Models
{
    public class Mentor
    {
        public int MentorId { get; set; }
        public string MainField { get; set; }
        public int UserId { get; set; }


        public Mentor(int mentorId, string mainField, int userID)
        {
            this.MentorId = mentorId;
            this.MainField = mainField;
            this.UserId = userID;
        }

        public Mentor()
        {
        }
    }
}
