using BookingSystem.Entities;
using System.Linq.Expressions;

namespace BookingSystem.Interfaces.Contracts
{
    public interface IBaseRepository
    {
        T GetById<T>(string id) where T : BaseEntity;

        T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

        IQueryable<T> GetAll<T>() where T : BaseEntity;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        void Insert<T>(T entity) where T : BaseEntity;
        void Replace<T>(T entity) where T : BaseEntity;
        void Delete<T>(string objectId) where T : BaseEntity;
        void AddOrUpdate<T>(T entity) where T : BaseEntity;
    }
}
