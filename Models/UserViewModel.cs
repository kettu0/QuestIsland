using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class UserViewModel
    {
        public User LoadedUser { get; set; }
        public Mentor LoadedMentor { get; set; }
        public Codecooler LoadedCodecooler { get; set; }
        public List<Mentor> MentorsList { get; set; }
        public List<Codecooler> CodecoolersList { get; set; }
        public List<User> UsersList { get; set; }

        public UserViewModel()
        {
            this.LoadedMentor = new Mentor();
            this.LoadedCodecooler = new Codecooler();
        }
        ~UserViewModel() {}
    }
}
