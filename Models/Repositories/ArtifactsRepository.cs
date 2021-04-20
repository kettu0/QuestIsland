using QuestStore_C_of_thieves.DAO;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;
using System.Collections.Generic;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public class ArtifactsRepository : IRepository<Artifact>
    {
        public List<Artifact> Entities { get; set; }
        public Artifact Entity { get; set; }
        public ArtifactDAO ArtifactDAO { get; set; }
        public CodecoolerArtifactDAO CodecoolerArtifactDAO { get; set; }
        public List<CodecoolerArtifact> CodecoolersArtifactsList { get; set; }

        public ArtifactsRepository()
        {
            this.ArtifactDAO = new ArtifactDAO();
            this.CodecoolerArtifactDAO = new CodecoolerArtifactDAO();
        }

        public void SeedArtifacts()
        {
            this.Entities = ArtifactDAO.ReadAllEntities();
        }

        public void SeedArtifactsByType(string type)
        {
            this.Entities = ArtifactDAO.ReadEntityByType(type);
        }

        public void SeedArtifactByPrice(string order)
        {
            this.Entities = ArtifactDAO.SortAllEntitiesByPrice(order);
        }


        public void SeedArtifactsByKeyword(string keyword)
        {
            this.Entities = ArtifactDAO.ReadEntitiesByKeyword(keyword);
        }

        public void SeedArtifactsByID(int ID)
        {
            this.Entity = ArtifactDAO.ReadEntityByID(ID);
        }

        public void SeedArtifactsbyName(string order)
        {
            this.Entities = ArtifactDAO.SortAllEntitiesAlphabetically(order);
        }

        public void SeedArtifactsByTypeAndSort(string type, string orderParam, string order)
        {
            this.Entities = ArtifactDAO.SortEntitiesByType(type, orderParam, order);
        }

        public void SeedCodecoolerArtifacts()
        {
            CodecoolerArtifactDAO codecoolerArtifactDAO = new CodecoolerArtifactDAO();
            var allCodecoolersArtifacts = codecoolerArtifactDAO.ReadAllEntities();
            var allCodecoolersArtifactsNames = new List<CodecoolerArtifact>();

            foreach (var artifact in allCodecoolersArtifacts)
            {
                artifact.ArtifactName = ArtifactDAO.ReadEntityByID(artifact.ArtifactId).Name;
                allCodecoolersArtifactsNames.Add(artifact);
            }

            this.CodecoolersArtifactsList = allCodecoolersArtifacts;

        }

        public void SeedCodecoolerArtifactsById(int id)
        {
            CodecoolerArtifactDAO codecoolerArtifactDAO = new CodecoolerArtifactDAO();
            var allCodecoolersArtifacts = codecoolerArtifactDAO.ReadAllEntitiesByCodecoolerID(id);
            var allCodecoolersArtifactsNames = new List<CodecoolerArtifact>();

            foreach (var artifact in allCodecoolersArtifacts)
            {
                artifact.ArtifactName = ArtifactDAO.ReadEntityByID(artifact.ArtifactId).Name;
                artifact.ArtifactType = ArtifactDAO.ReadEntityByID(artifact.ArtifactId).Type;
                artifact.ArtifactValue = ArtifactDAO.ReadEntityByID(artifact.ArtifactId).Price;
                artifact.ArtifactAmount = ArtifactDAO.ReadEntityByID(artifact.ArtifactId).Amount;

                allCodecoolersArtifactsNames.Add(artifact);
            }

            this.CodecoolersArtifactsList = allCodecoolersArtifacts;
        }


        public void SeedCodecoolerArtifactById(int id)
        {

        }

        public bool EntityExists(Artifact entity)
        {
            throw new System.NotImplementedException();
        }
    }
}


