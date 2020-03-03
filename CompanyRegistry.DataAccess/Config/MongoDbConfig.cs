using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyRegistry.DataAccess.Config
{
    public class MongoDbConfig
    {
        public static void Configure()
        {
            Mappings.Configure();

            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Conventions
            var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };

            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
    }
}
