using Autofac;
using Autofac.Integration.WebApi;
using MicroBuild.Management.Domain;
using MicroBuild.Management.Domain.ImportAdapters;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.Services;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Data.Mongo;
using System.Reflection;
using System.Web.Http;

namespace MicroBuild.Management.API.App_Start
{
	public class IocConfig
	{
		public static IContainer Configure(HttpConfiguration config)
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			/* Service and repository access */

			containerBuilder.RegisterType<MongoUnitOfWork>().As<IDataUnitOfWork>().InstancePerRequest();
			containerBuilder.RegisterType<ObjectsLocator>().As<IObjectsLocator>().InstancePerRequest();

			/* Projects */

			containerBuilder.RegisterType<ProjectsService>().InstancePerRequest();
			containerBuilder.RegisterType<ProjectsService>().As<IProjectsService>().InstancePerRequest();

            containerBuilder.RegisterType<ArchiveService>().InstancePerRequest();
            containerBuilder.RegisterType<ArchiveService>().As<IArchiveService>().InstancePerRequest();

            containerBuilder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerRequest();
            containerBuilder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerRequest();
            containerBuilder.RegisterType<DoorRepository>().As<IDoorRepository>().InstancePerRequest();
            containerBuilder.RegisterType<MessageRepository>().As<IMessageRepository>().InstancePerRequest();
            containerBuilder.RegisterType<IssueMessageRepository>().As<IIssueMessageRepository>().InstancePerRequest();

            /* Auth */

            containerBuilder.RegisterType<AuthService>().InstancePerRequest();
			containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();

			/* Users */

			containerBuilder.RegisterType<MbeUsersService>().InstancePerRequest();
			containerBuilder.RegisterType<MbeUsersService>().As<IMbeUsersService>().InstancePerRequest();

            /* // */

            containerBuilder.RegisterType<MbeProjectService>().InstancePerRequest();
            containerBuilder.RegisterType<MbeProjectService>().As<IMbeProjectService>().InstancePerRequest();

            containerBuilder.RegisterType<CompanyService>().InstancePerRequest();
            containerBuilder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();

            containerBuilder.RegisterType<MbeCompanyService>().InstancePerRequest();
            containerBuilder.RegisterType<MbeCompanyService>().As<IMbeCompanyService>().InstancePerRequest();

            containerBuilder.RegisterType<WorkorderService>().InstancePerRequest();
            containerBuilder.RegisterType<WorkorderService>().As<IWorkorderService>().InstancePerRequest();

			containerBuilder.RegisterType<AdhocWorkorderService>().InstancePerRequest();
			containerBuilder.RegisterType<AdhocWorkorderService>().As<IAdhocWorkorderService>().InstancePerRequest();

			containerBuilder.RegisterType<WorkorderDoorService>().InstancePerRequest();
            containerBuilder.RegisterType<WorkorderDoorService>().As<IWorkorderDoorService>().InstancePerRequest();

            containerBuilder.RegisterType<ChecklistService>().InstancePerRequest();
            containerBuilder.RegisterType<ChecklistService>().As<IChecklistService>().InstancePerRequest();

            containerBuilder.RegisterType<EmailLogService>().InstancePerRequest();
            containerBuilder.RegisterType<EmailLogService>().As<IEmailLogService>().InstancePerRequest();

			containerBuilder.RegisterType<EmailLogService>().InstancePerRequest();
			containerBuilder.RegisterType<EmailLogService>().As<IEmailLogService>().InstancePerRequest();
            containerBuilder.RegisterType<MessageService>().InstancePerRequest();
            containerBuilder.RegisterType<MessageService>().As<IMessageService>().InstancePerRequest();

            containerBuilder.RegisterType<IssueMessageService>().InstancePerRequest();
            containerBuilder.RegisterType<IssueMessageService>().As<IIssueMessageService>().InstancePerRequest();
            /* Request context */

            containerBuilder.RegisterType<DoorNotesService>().InstancePerRequest();
			containerBuilder.RegisterType<DoorNotesService>().As<IDoorNotesService>().InstancePerRequest();

            containerBuilder.RegisterType<LogService>().InstancePerRequest();
            containerBuilder.RegisterType<LogService>().As<ILogService>().InstancePerRequest();

            containerBuilder.RegisterType<SubscriptionService>().InstancePerRequest();
            containerBuilder.RegisterType<SubscriptionService>().As<ISubscriptionService>().InstancePerRequest();

            containerBuilder.RegisterType<NotificationService>().InstancePerRequest();
            containerBuilder.RegisterType<NotificationService>().As<INotificationService>().InstancePerRequest();
            /* Request context */

            containerBuilder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);

			/* Init */

			var container = containerBuilder.Build();
			var webApiResolver = new AutofacWebApiDependencyResolver(container);
			GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

			return container;
		}
	}
}
