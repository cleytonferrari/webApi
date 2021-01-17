using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


using System.ComponentModel.DataAnnotations;namespace UI.Api.Models
{
    public class Pessoa
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }
    }
}