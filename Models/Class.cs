using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime StartDate { get; set; }
        public List<Codecooler> MembersOfClass { get; set; }
        public List<int> IdNumbersOfMentors { get; set; }

        public Class(List<Codecooler> members )
        {
            this.MembersOfClass = members;
        }

        public Class(string className)
        {
            this.ClassName = className;
        }

        public Class(int id, string className, DateTime startDate)
        {
            this.Id = id;
            this.ClassName = className;
            this.StartDate = startDate;
        }

        public Class()
        {

        }
    }
}
