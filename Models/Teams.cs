using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class TeamResources
    {
        public int Id { get; set; }
        public List<Codecooler> MembersOfTeam { get; set; }
        public List<Quest> TeamQuests { get; set; }
        public List<Artifact> TeamArtifacts { get; set; }
        public int TeamMoney { get; set; }


        public TeamResources(int id, List<Codecooler> members, List<Quest> quests, List<Artifact> artifacts, int money)
        {
            this.Id = id;
            this.MembersOfTeam = members;
            this.TeamQuests = quests;
            this.TeamArtifacts = artifacts;
            this.TeamMoney = money;
        }
    }
}
