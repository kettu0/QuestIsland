using System;

namespace QuestStore_C_of_thieves.Models
{
    public class Codecooler
    {
        public int CodecoolerId { get; set; }
        public int TeamId { get; set; }
        public int ClassId { get; set; }
        public int CoolpointsWallet { get; set; }
        public int Exp { get; set; }
        public int UserId { get; set; }

        public Codecooler(int codecoolerId, int teamId, int classId, int userID, int coolpointsWallet, int exp)
        {
            this.CodecoolerId = codecoolerId;
            this.ClassId = classId;
            this.TeamId = teamId;
            this.CoolpointsWallet = coolpointsWallet;
            this.Exp = exp;
            this.UserId = userID;
        }

        public Codecooler()
        {
        }
    }
}