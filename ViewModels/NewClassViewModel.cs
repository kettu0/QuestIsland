using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class NewClassViewModel
    {
        public string NewClassName { get; set; }

        public Class NewClass = new Class();
        public List<User> Mentors { get; set; }
    }
}
