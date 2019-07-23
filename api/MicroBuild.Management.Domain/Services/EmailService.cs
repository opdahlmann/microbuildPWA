using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.Models;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.ServiceClients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class EmailService :IEmailService
    {
        private IEmailLogRepository _emailLogRepository;
        private IObjectsLocator ObjectLocator;

        public EmailService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            _emailLogRepository = dataUnitOfWork.EmailLogRepository;
            this.ObjectLocator = objectLocator;
        }

        public async Task<bool> SendEmailNotificationForNotStartedWorkorders(UserViewModel user,string projectName,DateTime projectPlanedStartDate)
        {
            EmailClient emailClient = new EmailClient();

            var postData = new EmailRequest
            {
                Email = new Email
                {
                    Cc = new List<string>(),
                    Params = new string[] { user.Name, projectName, projectPlanedStartDate.ToLocalTime().ToString() },
                    Subject = "SERVICEORDER NOT STARTED",
                    To = new string[] { user.Email },
                    Attachments = new List<string[]>()
                },
                Type = EMailTypeEnum.SERVICEORDER_NOT_STARTED
            };

            string json = JsonConvert.SerializeObject(postData, Formatting.Indented);

            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await emailClient.SendMail(byteContent);
        }

        public async Task<bool> SendEmailReminderNotificationsForWorkorderTemplates(UserViewModel user, string projectName, DateTime projectPlanedStartDate)
        {
            EmailClient emailClient = new EmailClient();

            var PostData = new EmailRequest
            {
                Email = new Email
                {
                    Cc = new List<string>(),
                    Params = new string[] { user.Name, projectName, projectPlanedStartDate.ToLocalTime().ToString() },
                    Subject = "SERVICEORDER NOTIFICATION",
                    To = new string[] { user.Email },
                    Attachments = new List<string[]>()
                },
                Type = EMailTypeEnum.SERVICEORDER_TEMPLATE_REMINDER_NOTIFICATION
            };

            string json = JsonConvert.SerializeObject(PostData, Formatting.Indented);

            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await emailClient.SendMail(byteContent);
        }


        public async Task<bool> SendEmailForDoorMessages(CompanyUser sendingUser, List<string> emailAddresses, Project project, Message message, string companyName)
        {
            EmailClient emailClient = new EmailClient(sendingUser.MBEUserId);

            var PostData = new EmailRequest
            {
               
                Email = new Email
                {
                    Cc = new List<string>(),
                    Params = new string[] { message.Subject, message.Body, project.Name, message.DoorNo, sendingUser.Name, sendingUser.Email, companyName },
                    Subject = message.Subject+" - "+project.Name,
                    To = emailAddresses?.ToArray(),
                    From = sendingUser.Email,
                    Attachments = new List<string[]> { },
					Images = 
						(message.Picture != null) 
						? new List<string[]> { new string[] { ".jpg", "image/jpeg", message.Picture }, }
						: new List<string[]> { },
				},
                Type = 
					(message.Picture != null) 
					? EMailTypeEnum.SERVICE_MESSAGE_MAIL_WITH_PHOTO 
					: EMailTypeEnum.SERVICE_MESSAGE_MAIL,
            };

            string json = JsonConvert.SerializeObject(PostData, Formatting.Indented);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await emailClient.SendMailForDoorMessages(sendingUser.MBEUserId, byteContent);
        }

        public async Task<bool> SendEmailForIssueMessage(CompanyUser sendingUser, List<string> emailAddresses, Project project, IssueMessage issueMessage, string companyName)
        {
            EmailClient emailClient = new EmailClient(sendingUser.MBEUserId);

            var PostData = new EmailRequest
            {

                Email = new Email
                {
                    Cc = new List<string>(),
                    Params = new string[] { issueMessage.Subject, issueMessage.Body, project.Name, issueMessage.DoorNo, sendingUser.Name, sendingUser.Email, companyName },
                    Subject = issueMessage.Subject + " - " + project.Name,
                    To = emailAddresses?.ToArray(),
                    From = sendingUser.Email,
                    Attachments = new List<string[]> { },
                    Images =
                        (issueMessage.Picture != null)
                        ? new List<string[]> { new string[] { ".jpg", "image/jpeg", issueMessage.Picture }, }
                        : new List<string[]> { },
                },
                Type =
                    (issueMessage.Picture != null)
                    ? EMailTypeEnum.ISSUE_MESSAGE_MAIL_WITH_PHOTO
                    : EMailTypeEnum.ISSUE_MESSAGE_MAIL,
            };

            string json = JsonConvert.SerializeObject(PostData, Formatting.Indented);

            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await emailClient.SendMailForIssueMessages(sendingUser.MBEUserId, byteContent);
        }

    }
}