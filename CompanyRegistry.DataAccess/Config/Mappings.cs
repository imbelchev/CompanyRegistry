using CompanyRegistry.DataAccess.Entitites;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyRegistry.DataAccess.Config
{
    public class Mappings
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Company>(map =>
            {
                map.AutoMap();

                map.SetIgnoreExtraElements(true);

                map.MapIdMember(x => x.Id);

                map.MapMember(x => x.Name).SetIsRequired(true);

                map.MapMember(x => x.Vat).SetIsRequired(true);
            });
        }
    }
}
