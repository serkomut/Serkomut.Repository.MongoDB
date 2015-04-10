using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Komut.Data.MongoDB.Core;
using MongoDB.Driver;

namespace Komut.Data.MongoDB
{
    public class MongoDbRepository<T> : IRepository<T> where T : BaseEntity
    {
        readonly IMongoDatabase database;

        public MongoDbRepository()
        {
            //GetDatabase();
            //GetCollection();
            var client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("KomutMongo");
        }

        public async Task Insert(T entity)
        {
            entity.Id = Guid.NewGuid();
            await Collection.InsertOneAsync(entity);
        }

        public async Task<UpdateResult> Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return await Collection.UpdateOneAsync(filter, update);
        }

        public async Task<DeleteResult> Delete(FilterDefinition<T> filter)
        {
            return await Collection.DeleteOneAsync(filter);
        }

        public Task<IList<T>> SearchFor(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IAsyncCursor<T>> GetAll(FilterDefinition<T> entity)
        {
            return await Collection.FindAsync(entity);
        }

        public async Task<T> GetById(Guid id)
        {
            return await Collection.Find(x => x.Id == id).SingleAsync();
        }

        //private void GetDatabase()
        //{
        //    var connectionString = GetConnectionString();
        //    var databaseName = GetDatabaseName();
        //    var client = new MongoClient("mongodb://localhost:27017");
        //    database = client.GetDatabase("KomutMongo");
        //}

        //private string GetConnectionString()
        //{
        //    return ConfigurationManager
        //        .AppSettings
        //            .Get("MongoConnectionString")
        //                .Replace("{DB_NAME}", GetDatabaseName());
        //}

        //private string GetDatabaseName()
        //{
        //    return ConfigurationManager
        //        .AppSettings
        //            .Get("MongoDbDataBaseName");
        //}

        IMongoCollection<T> Collection
        {
            get { return database.GetCollection<T>(typeof (T).FullName); }
        } 

    }
}