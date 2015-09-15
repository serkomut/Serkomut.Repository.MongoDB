using System;
using Komut.Data.MongoDB.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;

namespace Komut.Data.MongoDB.Test
{
    [TestFixture]
    public class MongoDbTestFixture
    {
        PersonRepository repository;
        [TestFixtureSetUp]
        public void Setup()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            BsonClassMap.RegisterClassMap<BaseEntity>(map => map.AutoMap());
            repository = new PersonRepository(client.GetDatabase("KomutMongo"));
        }

        [Test]
        public async void CreatePerson()
        {
            var person = new Person
            {
                BirthDate = DateTime.Now,
                Name = "Test",
                Surname = "MongoDB"
            };

            await repository.Insert(person);
        }
    }

    public class Person: Entity
    {
        public Person()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public interface IPersonRepotsitory : IRepository<Person>
    {
        
    }

    public class PersonRepository : MongoDbRepository<Person>, IPersonRepotsitory
    {
        public PersonRepository(IMongoDatabase database) : base(database)
        {
            
        }
    }
}
