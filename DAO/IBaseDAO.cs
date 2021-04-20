using System.Collections.Generic;

namespace QuestStore_C_of_thieves.DAO
{
    public interface IBaseDAO<T>
    {
        public void CreateEntity(T EntityToCreate);
        public void UpdateEntity(T EntityToUpdate);
        public void DeleteEntity(T EntityToDelete);
        public T ReadEntityByID(int ID);
        public List<T> ReadEntityByName(string name);
        public List<T> ReadEntitiesByKeyword(string keyword);
        public List<T> ReadAllEntities();
        public List<T> SortAllEntitiesAlphabetically(string order);
        public List<T> SortAllEntitiesByIDs(string order);

    }

}


