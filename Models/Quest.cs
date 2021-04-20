using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CoolpointsValue { get; set; }
        public int ExpValue { get; set; }
        public string Description { get; set; }

        public Quest()
        {

        }
        public Quest(int id, string name, string type, int coolPoints, int expValue, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.CoolpointsValue = coolPoints;
            this.ExpValue = expValue;
            this.Description = description;

        }

        public Quest(int id, string name, int coolPoints, int expValue)
        {
            this.Id = id;
            this.Name = name;
            this.CoolpointsValue = coolPoints;
            this.ExpValue = expValue;
        }

    }
}
