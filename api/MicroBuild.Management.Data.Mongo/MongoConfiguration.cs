using MicroBuild.Infrastructure.Mongo;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Configuration;

namespace MicroBuild.Management.Data.Mongo
{
	public class MongoConfiguration : IConfiguration
	{
		public void Configure()
		{
			MapEntities();

			MongoConfigurations.DatabaseName = ConfigurationManager.AppSettings.Get("DBName");
			MongoConfigurations.DbUserName = ConfigurationManager.AppSettings.Get("DBUserName");
			MongoConfigurations.DbPassword = ConfigurationManager.AppSettings.Get("DBPassword");
			MongoConfigurations.DbServerHost = ConfigurationManager.AppSettings.Get("DBServerHost");
			MongoConfigurations.DbServerPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("DBServerPort"));
			MongoConfigurations.DbMaxConnectionIdleTime = Convert.ToDouble(ConfigurationManager.AppSettings.Get("DBMaxConnectionIdleTime"));
		}

		private void MapEntities()
		{
			MongoConfigurations.MapEntity<Company>();
			MongoConfigurations.MapEntity<Project>();
			MongoConfigurations.MapEntity<Door>();
			MongoConfigurations.MapEntity<UserInProject>();
			MongoConfigurations.MapEntity<StatusMessages>();
			MongoConfigurations.MapEntity<Sync>();
            MongoConfigurations.MapEntity<Workorder>();
            MongoConfigurations.MapEntity<WorkorderTemplate>();
            MongoConfigurations.MapEntity<WorkorderTemplateDoor>();
            MongoConfigurations.MapEntity<WorkorderDoor>();
            MongoConfigurations.MapEntity<WorkorderTemplateHardwareCollection>();
            MongoConfigurations.MapEntity<Checklist>();
            MongoConfigurations.MapEntity<DoorNote>();
            MongoConfigurations.MapEntity<HardwareMaintainedLog>();
            MongoConfigurations.MapEntity<HardwareMaintainedDoorLog>();
            MongoConfigurations.MapEntity<Message>();
            MongoConfigurations.MapEntity<IssueMessage>();
        }
	}
}
