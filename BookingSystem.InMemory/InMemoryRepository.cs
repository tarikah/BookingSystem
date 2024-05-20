using BookingSystem.Entities;
using BookingSystem.Interfaces.Contracts;
using Mapster;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace BookingSystem.InMemory
{
    public class InMemoryRepository : IBaseRepository
    {
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, object>> DataDictionary = new();
        #region Private
        private ConcurrentDictionary<string, object> TryGetValue(string type)
        {
            DataDictionary.TryGetValue(type, out var objectsDictionary);
            return objectsDictionary;
        }
        private ConcurrentDictionary<string, object> InitializeDictForKey(string type)
        {
            DataDictionary.TryAdd(type, new ConcurrentDictionary<string, object>());
            return TryGetValue(type);
        }
        #endregion
        public T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
           var objects = TryGetValue(typeof(T).FullName);

            var value = objects.Values.AsQueryable().FirstOrDefault(expression);

            return TypeAdapter.Adapt<T>(value);
        }

        public IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);

            return objects.Values.AsQueryable().Select(x => (T)x).Where(expression);
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);

            return objects.Values.AsQueryable().Select(x => (T)x);
        }

        public T GetById<T>(string id) where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);

            objects.TryGetValue(id, out var objectToReturn);

            return TypeAdapter.Adapt<T>(objectToReturn);
        }

        public void Insert<T>(T entity) where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);

            if (objects is null)
                objects = InitializeDictForKey(typeof(T).FullName);

            objects.TryAdd(entity.Id, entity);
        }

        public void Replace<T>(T entity) where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);

            objects.TryGetValue(entity.Id, out var objectToUpdate);
            objects.TryUpdate(entity.Id, entity, objectToUpdate);
        }
        public void Delete<T>(string objectId) where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);
            objects.TryRemove(objectId, out var objectToDelete);
        }

        public void AddOrUpdate<T>(T entity) where T : BaseEntity
        {
            var objects = TryGetValue(typeof(T).FullName);

            if (objects is null) 
                objects = InitializeDictForKey(typeof(T).FullName);
            


            if (objects.ContainsKey(entity.Id))
            {
                objects.TryGetValue(entity.Id, out var objectToUpdate);
                objects.TryUpdate(entity.Id, entity, objectToUpdate);
                return;
            }

            objects.TryAdd(entity.Id, entity);
        }


        
    }
}