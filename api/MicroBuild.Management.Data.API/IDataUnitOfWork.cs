namespace MicroBuild.Management.Data.API
{
	public interface IDataUnitOfWork
	{
		IProjectRepository ProjectRepository { get; }
        IProjectDocumentRepository ProjectDcoumentRepository { get; }
        IDoorRepository DoorRepository { get; }
        ISyncRepository SyncRepository { get; }
        ICompanyRepository CompanyRepository { get; }

        IWorkorderTemplateRepository WorkorderTemplateRepository { get; }
        IWorkorderTemplateDoorRepository WorkorderTemplateDoorRepository { get; }
        IWorkorderTemplateHardwareCollectionRepository WorkorderTemplateHardwareCollectionRepository { get; }

        IWorkorderRepository WorkorderRepository { get; }
        IChecklistRepository ChecklistRepository { get; }
        IWorkorderDoorRepository WorkorderDoorRepository { get; }

        IEmailLogRepository EmailLogRepository { get; }

		IDoorNoteRepository DoorNoteRepository { get; }


        IHardwareMaintainedLogRepository HardwareMaintainedLogRepository { get; }

        IHardwareMaintainedDoorLogRepository HardwareMaintainedDoorLogRepository { get; }
        
        IMessageRepository MessageRepository { get; }
        IIssueMessageRepository IssueMessageRepository { get; }

        ISubscriptionRepository SubscriptionRepository { get; }
    }
}
