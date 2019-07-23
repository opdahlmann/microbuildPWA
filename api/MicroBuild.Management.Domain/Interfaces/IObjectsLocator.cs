using MicroBuild.Management.Domain.Factories.Interfaces;
using MicroBuild.Management.Domain.ImportAdapters;
using MicroBuild.Management.Domain.Services.Domain;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IObjectsLocator
	{
		IProjectsService ProjectService { get; }

        IAuthService AuthService { get; }

        IProjectImportAdapter ProjectImportAdapter { get; }

        DomainObjectFactory DomainObjectFactory { get; }

        IMBEDoorService MbeDoorService { get; }

        IDoorImportAdapter DoorImportAdapter { get; }

        IProjectDocumentImportAdapter ProjectDocumentImportAdapter { get; }

        IMbeProjectService MbeProjectService { get; }

        ISyncLogService SyncLogService { get; }

        IUserImportAdapter UserImportAdapter { get; }

		ICompanyImportAdapter CompanyImportAdapter { get; }

        ICompanyService CompanyService { get; }

        IMbeCompanyService MbeCompanyService { get; }

        IDoorDetailModelFactory DoorDetailModelFactory { get; }

        IDoorService DoorService { get; }

        IJsonToExcelAdapter JsonToExcelAdapter { get; }

        IMbeUsersService MbeUsersService { get; }

        IArchiveService ArchiveService { get; }

        IWorkorderService WorkorderService { get; }

        IWorkorderTemplateService WorkorderTemplateService { get; }

		IAdhocWorkorderService AdhocWorkorderService { get; }

		IChecklistService ChecklistService { get; }

        IWorkorderTemplateDoorService WorkorderTemplateDoorService { get; }

		IWorkorderDoorService WorkorderDoorService { get; }

        IWorkorderTemplateHardwareCollectionService WorkorderTemplateHardwareCollectionService { get; }

        IEmailService EmailService { get; }

        IEmailLogService EmailLogService { get; }

		IDoorNotesService DoorNotesService { get; }

        ILogService LogService { get; }

        IHardwareService HardwareService { get; }
        
        IMessageService MessageService { get; }

        IIssueMessageService IssueMessageService { get; }

        ISubscriptionService SubscriptionService { get; }

        INotificationService NotificationService { get; }
    }
}
