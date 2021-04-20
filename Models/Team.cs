using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int MentorId { get; set; }
        public int ClassId { get; set; }
        public DateTime CreationDate { get; set; }

        public Team()
        {

        }

        public Team(int teamId, string teamName, int mentorId, int classId, DateTime creationDate)
        {
            this.TeamId = teamId;
            this.TeamName = teamName;
            this.MentorId = mentorId;
            this.ClassId = classId;
            this.CreationDate = creationDate;

        }

        public Team(int teamId, string teamName, DateTime creationdate)
        {
            this.TeamId = teamId;
            this.TeamName = teamName;
            this.CreationDate = creationdate;
        }
    }

}
