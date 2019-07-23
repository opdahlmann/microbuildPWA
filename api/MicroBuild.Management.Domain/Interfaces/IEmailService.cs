using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailNotificationForNotStartedWorkorders(UserViewModel user, string projectName, DateTime projectPlanedStartDate);
        Task<bool> SendEmailReminderNotificationsForWorkorderTemplates(UserViewModel user, string projectName, DateTime projectPlanedStartDate);
        Task<bool> SendEmailForDoorMessages(CompanyUser sendingUser, List<string> emailAddresses, Project project, Message message,string companyName);
        Task<bool> SendEmailForIssueMessage(CompanyUser sendingUser, List<string> emailAddresses, Project project, IssueMessage message, string companyName);
    }
}