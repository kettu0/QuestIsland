using System;

namespace QuestStore_C_of_thieves.Models
{
    public class CodecoolerArtifact
    {
        public int Id { get; set; }
        public int ArtifactId { get; set; }
        public int CodecoolerID { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsUsed { get; set; }
        public string ArtifactName { get; set; }
        public string CodecoolerName { get; set; }
        public int ArtifactValue { get; set; }
        public int ArtifactAmount { get; set; }
        public string ArtifactType { get; set; }

        public CodecoolerArtifact(int id, int artifactId, int codecoolerId, DateTime transactionDate, bool isUsed)
        {
            this.Id = id;
            this.ArtifactId = artifactId;
            this.CodecoolerID = codecoolerId;
            this.TransactionDate = transactionDate;
            this.IsUsed = isUsed;
        }

        public CodecoolerArtifact(int artifactId, int codecoolerId, DateTime transactionDate, bool isUsed)
        {
            this.ArtifactId = artifactId;
            this.CodecoolerID = codecoolerId;
            this.TransactionDate = transactionDate;
            this.IsUsed = isUsed;
        }

        public CodecoolerArtifact(int artifactId, int codecoolerId)
        {
            this.ArtifactId = artifactId;
            this.CodecoolerID = codecoolerId;
        }

    }

}
