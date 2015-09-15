using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Komut.Data.MongoDB.Core;
using MongoDB.Driver;

namespace Komut.Data.MongoDB
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : Entity<string>
    {
        private readonly IMongoCollection<TEntity> collection;
        public MongoDbRepository(IMongoDatabase database)
        {
            this.collection = database.GetCollection<TEntity>(typeof(TEntity).FullName);
        }

        public async Task Insert(TEntity entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async Task Update(TEntity update)
        {
            await collection.FindOneAndReplaceAsync(x => x.Id == update.Id, update);
        }

        public async Task<DeleteResult> Delete(FilterDefinition<TEntity> filter)
        {
            return await collection.DeleteOneAsync(filter);
        }

        public Task<IList<TEntity>> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IAsyncCursor<TEntity>> GetAll(FilterDefinition<TEntity> entity)
        {
            return await collection.FindAsync(entity);
        }

        public async Task<TEntity> GetById(string id)
        {
            return await collection.Find(x => x.Id == id).SingleAsync();
        }

    }
}