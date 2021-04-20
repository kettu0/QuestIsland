using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models
{
    public class CodecoolerQuest
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public int CodecoolerId { get; set; }
        public string CodecoolerNames { get; set; }
        public int? ApproverId { get; set; }
        public string AprroverNames { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletionDate { get; set; }

        public CodecoolerQuest(int id, int questId, int codecoolerId, int? approverId, bool isCompleted, DateTime completionDate )
        {
            this.Id = id;
            this.QuestId = questId;
            this.CodecoolerId = codecoolerId;
            this.ApproverId = approverId;
            this.IsCompleted = isCompleted;
            this.CompletionDate = completionDate;
        }
        public CodecoolerQuest(int questId, int codecoolerId)
        {
            this.QuestId = questId;
            this.CodecoolerId = codecoolerId;
        }
    }
}
