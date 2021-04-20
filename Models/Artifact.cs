using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class Artifact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }


        public Artifact(int id, string name, string type, int price, int amount, string description, string image)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.Type = type;
            this.Amount = amount;
            this.Image = image;
        }

        public Artifact()
        {

        }

    }
}
