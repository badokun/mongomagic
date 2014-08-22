using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NI.Data;
using NI.Data.SqlClient;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);

            var server = client.GetServer();
            var database = server.GetDatabase("test"); // "test" is the name of the database

            // "entities" is the name of the collection
            var collection = database.GetCollection<Entity>("entities");

            var entity = new Entity { Name = "Tom" };
            collection.Insert(entity);
            var id = entity.Id; // Insert will set the Id if necessary (as it was in this example)

            var query = Query<Entity>.EQ(e => e.Id, id);
            entity = collection.FindOne(query);

            entity.Name = "Dick";
            collection.Save(entity);

            query = Query<Entity>.EQ(e => e.Id, id);
            var update = Update<Entity>.Set(e => e.Name, "Harry"); // update modifiers
            collection.Update(query, update);

            query = Query<Entity>.EQ(e => e.Id, id);
            collection.Remove(query);

        }
    }
    public class Entity
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }

}
