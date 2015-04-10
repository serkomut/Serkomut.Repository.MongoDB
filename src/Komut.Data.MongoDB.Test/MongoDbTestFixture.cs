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
        [TestFixtureSetUp]
        public void Setup()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(map => map.AutoMap());
        }

        [Test]
        public void CreatePerson()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Test",
                Surname = "MongoDB"
            };

            PersonRepository.Insert(person);
        }

        static IRepository<Person> PersonRepository
        {
            get
            {
                return new MongoDbRepository<Person>();
            }
        }

        [Test]
        public async void MongoNew()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("foo");
            var collection = database.GetCollection<Person>("bar");

            await collection.InsertOneAsync(new Person { Name = "Jack" });

            var list = await collection.Find(x => x.Name == "Jack").ToListAsync();

            foreach (var person in list)
            {
                Console.WriteLine(person.Name);
            }
        }
    }

    public class Person: BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
