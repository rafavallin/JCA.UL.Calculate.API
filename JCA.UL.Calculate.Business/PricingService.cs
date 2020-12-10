using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace JCA.UL.Calculate.Business
{
    public class PricingService
    {

        public PricingService()
        {
    
        }

        public BsonDocument GetMongoDocument(string strProduct)
        {
            var db = Domain.Context.PricingContext.conMongo.GetConnection().GetDatabase("PricingDB");

            var collection = db.GetCollection<BsonDocument>("PricingComposition");

            return collection.Find(new BsonDocument{ {"Sistema",strProduct}  }).FirstOrDefault();
        }
        

    }
}