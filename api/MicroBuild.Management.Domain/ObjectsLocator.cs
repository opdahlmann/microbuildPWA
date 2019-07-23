using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Services;
using MicroBuild.Management.Domain.ServiceClients;
using MicroBuild.Management.Domain.Services.Domain;
using MicroBuild.Management.Domain.ImportAdapters;
using MicroBuild.Management.Domain.Factories.Interfaces;
using MicroBuild.Management.Domain.Factories;
using System.Net.Http;

namespace MicroBuild.Management.Domain
{
    public class ObjectsLocator : IObjectsLocator
	{
		private readonly IDataUnitOfWork dataUnitOfWork;

		private readonly HttpRequestMessage httpRequest;

		public ObjectsLocator(IDataUnitOfWork dataUnitOfWork, HttpRequestMessage httpRequest)
		{
			this.dataUnitOfWork = dataUnitOfWork;
			this.httpRequest = httpRequest;
		}

		#region Service clients

		public MbeClient MbeClient
		{
			get
			{
				return new MbeClient(httpRequest);
			}
		}

		#endregion

		#region Services

		public IAuthService AuthService
		{
			get
			{
				return new AuthService();
			}
		}

		public IProjectsService ProjectService
		{
			get
			{
				return new ProjectsService(dataUnitOfWork, this);
			}
		}

        public IArchiveService ArchiveService
        {
            get
            {
                return new ArchiveService(dataUnitOfWork, this);
            }
        }

        public DomainObjectFactory DomainObjectFactory
        {
            get
            {
                return new DomainObjectFactory(dataUnitOfWork);
            }
        }

        public IMBEDoorService MbeDoorService
        {
            get
            {
                return new MbeDoorService(dataUnitOfWork, this);
            }
        }

        public IMbeProjectService MbeProjectService
        {
            get
            {
                return new MbeProjectService(this);
            }
        }

        public ISyncLogService SyncLogService
        {
            get
            {
                return new SyncLogService(dataUnitOfWork, this);
            }
        }

		public IDoorService DoorService
		{
			get
			{
				return new DoorService(dataUnitOfWork, this);
			}
		}

		public IMbeCompanyService MbeCompanyService
		{
			get
			{
				return new MbeCompanyService(this);
			}
		}

		public ICompanyService CompanyService
		{
			get
			{
				return new CompanyService(dataUnitOfWork, this);
			}
		}

		#endregion

		#region Import adapters

		public IDoorImportAdapter DoorImportAdapter
		{
			get
			{
				return new MbeDoorImportAdapter(httpRequest);
			}
		}

		public IProjectDocumentImportAdapter ProjectDocumentImportAdapter
		{
			get
			{
				return new MbeProjectDocumentImportAdapter(httpRequest);
			}
		}

		public IProjectImportAdapter ProjectImportAdapter
		{
			get
			{
				return new MbeProjectImportAdapter(httpRequest);
			}
		}

		public IUserImportAdapter UserImportAdapter
        {
            get
            {
                return new MbeUserImportAdapter(httpRequest);
            }
        }

		public ICompanyImportAdapter CompanyImportAdapter
		{
			get
			{
				return new MbeCompanyImportAdapter(httpRequest);
			}
		}

		#endregion

		#region Domain object factories

		public IDoorDetailModelFactory DoorDetailModelFactory
        {
            get
            {
                return new DoorDetailModelFactory();
            }
        }

        #endregion

        public IJsonToExcelAdapter JsonToExcelAdapter
        {
            get
            {
                return new JsonToExcelAdapter();
            }
        }

        public IMbeUsersService MbeUsersService
        {
            get
            {
                return new MbeUsersService(this);
            }
        }

        public IWorkorderService WorkorderService
        {
            get
            {
                return new WorkorderService(dataUnitOfWork, this);
            }
        }

		public IAdhocWorkorderService AdhocWorkorderService
		{
			get
			{
				return new AdhocWorkorderService(dataUnitOfWork, this);
			}
		}

		public IWorkorderTemplateService WorkorderTemplateService
        {
            get
            {
                return new WorkorderTemplateService(dataUnitOfWork, this);
            }
        }

        public IWorkorderTemplateDoorService WorkorderTemplateDoorService
        {
            get
            {
                return new WorkorderTemplateDoorService(dataUnitOfWork);
            }
        }

        public IWorkorderTemplateHardwareCollectionService WorkorderTemplateHardwareCollectionService
        {
            get
            {
                return new WorkorderTemplateHardwareCollectionService(dataUnitOfWork);
            }
        }

        public IChecklistService ChecklistService
        {
            get
            {
                return new ChecklistService(dataUnitOfWork);
            }
        }

        public IEmailService EmailService
        {
            get
            {
                return new EmailService(dataUnitOfWork, this);
            }
        }

        public IEmailLogService EmailLogService
        {
            get
            {
                return new EmailLogService(dataUnitOfWork, this);
            }
        }

		public IWorkorderDoorService WorkorderDoorService
		{
			get
			{
				return new WorkorderDoorService(dataUnitOfWork,this);
			}
		}

		public IDoorNotesService DoorNotesService
		{
			get
			{
				return new DoorNotesService(dataUnitOfWork, this);
			}
		}

        public ILogService LogService
        {
            get
            {
                return new LogService(this.dataUnitOfWork,this);
            }
        }

        public IHardwareService HardwareService
        {
            get
            {
                return new HardwareService(this);
            }
        }
    
        public IMessageService MessageService
        {
            get
            {
                return new MessageService(dataUnitOfWork, this);
            }
        }

        public IIssueMessageService IssueMessageService
        {
            get
            {
                return new IssueMessageService(dataUnitOfWork, this);
            }
        }

        public ISubscriptionService SubscriptionService
        {
            get
            {
                return new SubscriptionService(dataUnitOfWork, this);
            }
        }

        public INotificationService NotificationService
        {
            get
            {
                return new NotificationService(dataUnitOfWork, this);
            }
        }
    }
}
