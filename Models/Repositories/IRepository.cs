using System.Collections.Generic;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public interface IRepository<T>
    {
        public List<T> Entities { get; set; }
        bool EntityExists(T entity);
    }
    
}
