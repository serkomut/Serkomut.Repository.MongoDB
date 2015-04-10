using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Komut.Data.MongoDB.Core
{
    public interface IRepository<T>
    {
        Task Insert(T entity);
        Task<UpdateResult> Update(FilterDefinition<T> entity, UpdateDefinition<T> update);
        Task<DeleteResult> Delete(FilterDefinition<T> entity);
        Task<IList<T>> SearchFor(Expression<Func<T, bool>> predicate);
        Task<IAsyncCursor<T>> GetAll(FilterDefinition<T> entity);
        Task<T> GetById(Guid id);
    }
}