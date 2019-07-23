using MicroBuild.Management.Data.API;

namespace MicroBuild.Management.Data.Mongo
{
	public class MongoUnitOfWork : IDataUnitOfWork
	{
		public IProjectRepository ProjectRepository
		{
			get
			{
				return new ProjectRepository();
			}
		}

        public IProjectDocumentRepository ProjectDcoumentRepository
        {
            get
            {
                return new ProjectDocumentRepository();
            }
        }

        public IDoorRepository DoorRepository
        {
            get
            {
                return new DoorRepository();
            }
        }

        public ISyncRepository SyncRepository
        {
            get
            {
                return new SyncRepository();
            }
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                return new CompanyRepository();
            }
        }

        public IWorkorderRepository WorkorderRepository
        {
            get
            {
                return new WorkorderRepository();
            }
        }

        public IChecklistRepository ChecklistRepository
        {
            get
            {
                return new ChecklistRepository();
            }
        }

        public IWorkorderDoorRepository WorkorderDoorRepository
        {
            get
            {
                return new WorkorderDoorRepository();
            }
        }

        public IWorkorderTemplateRepository WorkorderTemplateRepository
        {
            get
            {
                return new WorkorderTemplateRepository();
            }
        }

        public IWorkorderTemplateDoorRepository WorkorderTemplateDoorRepository
        {
            get
            {
                return new WorkorderTemplateDoorRepository();
            }
        }

        public IWorkorderTemplateHardwareCollectionRepository WorkorderTemplateHardwareCollectionRepository
        {
            get
            {
                return new WorkorderTemplateHardwareCollectionRepository();
            }
        }

        public IEmailLogRepository EmailLogRepository
        {
            get
            {
                return new EmailLogRepository();
            }
        }

		public IDoorNoteRepository DoorNoteRepository
		{
			get
			{
				return new DoorNoteRepository();
			}
		}

        public IHardwareMaintainedLogRepository HardwareMaintainedLogRepository
        {
            get
            {
                return new HardwareMaintainedLogRepository();
            }
        }

        public IHardwareMaintainedDoorLogRepository HardwareMaintainedDoorLogRepository
        {
            get
            {
                return new HardwareMaintainedDoorLogRepository();
            }
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                return new MessageRepository();
            }
        }

        public IIssueMessageRepository IssueMessageRepository
        {
            get
            {
                return new IssueMessageRepository();
            }
        }
    }
}
