using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Komut.Data.MongoDB.Core
{
    public interface IRepository<T, in TKey> where T : IEntity<TKey>
    {
        Task Insert(T entity);
        Task<UpdateResult> Update(FilterDefinition<T> entity, UpdateDefinition<T> update);
        Task<DeleteResult> Delete(FilterDefinition<T> entity);
        Task<IList<T>> SearchFor(Expression<Func<T, bool>> predicate);
        Task<IAsyncCursor<T>> GetAll(FilterDefinition<T> entity);
        Task<T> GetById(string id);
    }

    public interface IRepository<T> : IRepository<T, string> where T : IEntity<string>
    {

    } 

    public interface IEntity<out T>
    {
        T Id { get; }
    }

    public abstract class Entity : Entity<string>
    {
        protected Entity() { }
        protected Entity(string id) : base(id) { }
    }

    public abstract class Entity<T> : IEntity<T>, IEquatable<Entity<T>>
    {
        protected Entity()
        {
     
        }

        protected Entity(T id) : this()
        {
            Id = id;
        }
        public T Id { get; set; }

        public bool Equals(Entity<T> other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<T>);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool IsTransient
        {
            get { return Equals(Id, default(T)); }
        }
    }
}