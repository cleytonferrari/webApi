using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace UI.Api.Repositorio
{
    public class RepositorioMongo<T>
    {
        private readonly MongoDatabase database;

        private static string GetMongoDbConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MONGOLAB_URI") ??
                   ConfigurationManager.ConnectionStrings["WebAPI"].ConnectionString;
        }

        public RepositorioMongo()
        {
            var url = new MongoUrl(GetMongoDbConnectionString());
            var client = new MongoClient(url);
            var server = client.GetServer();
            database = server.GetDatabase(url.DatabaseName);
            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
            
            //Ajuda no migration, se tiver campo a mais no banco ele ignora
            var conventions = new ConventionProfile();
            conventions.SetIgnoreExtraElementsConvention(new AlwaysIgnoreExtraElementsConvention());
            BsonClassMap.RegisterConventions(conventions, (type) => true);
        }

        public MongoCollection<T> Collection { get; private set; }
    }
}