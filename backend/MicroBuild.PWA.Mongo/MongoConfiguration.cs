using MicroBuild.Infrastructure.Mongo;
using MicroBuild.PWA.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MicroBuild.PWA.Mongo
{
    public class MongoConfiguration 
    {
        public void Configure()
        {
            MapEntities();

            MongoConfigurations.DatabaseName = ConfigurationManager.AppSettings.Get("DBName");
            MongoConfigurations.DbUserName = ConfigurationManager.AppSettings.Get("DBUserName");
            MongoConfigurations.DbPassword = ConfigurationManager.AppSettings.Get("DBPassWord");
            MongoConfigurations.DbServerHost = ConfigurationManager.AppSettings.Get("DBServerHost");
            MongoConfigurations.DbServerPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("DBServerPort"));
            MongoConfigurations.DbMaxConnectionIdleTime = Convert.ToDouble(ConfigurationManager.AppSettings.Get("DBMaxConnectionIdleTime"));

        }
        private void MapEntities()
        {
            MongoConfigurations.MapEntity<MBSubscription>();
        }

    }

   
}
