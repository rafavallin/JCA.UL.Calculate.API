using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace JCA.UL.Calculate.Domain.Context
{
    public class PricingContext : DbContext
    {
        public PricingContext()
        {
            
        }

        public static class conMongo
        {
            static MongoClient conn = null;  
            public static MongoClient GetConnection()
            {
                if(conn == null)
                    conn = new MongoClient("mongodb://root:P%40%24%24w0rd01@10.61.32.19:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false");  

                return conn;   
            }
            
        }
    }
}