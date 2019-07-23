using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain
{
    public class MessageService : IMessageService
    {
        private IMessageRepository messageRepository;
        private IObjectsLocator ObjectLocator;
        private IIssueMessageRepository issueMessageRepository;

        public MessageService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            this.messageRepository = dataUnitOfWork.MessageRepository;
            this.issueMessageRepository = dataUnitOfWork.IssueMessageRepository;
            this.ObjectLocator = objectLocator;
        }

        public async Task<Message> AddServiceMessage(Message message, string userId)
        {
            int IsMailSentCount = 0;
            try
            {
                var companies = await ObjectLocator.CompanyService.GetCompaniesByProjectId(message.ProjectId);
                var userCompany = await ObjectLocator.CompanyService.GetProjectCompanyByUserId(message.ProjectId, userId);

                message.Recipients = new List<MessageUser>();
                var sendingUser = companies.SelectMany(c => c.Users).Where(u => u.MBEUserId.Equals(userId));

                var project = await ObjectLocator.ProjectService.GetProject(message.ProjectId);
                message.Sender.MBEUserId = userId;
                message.Sender.Timestamp = DateTime.UtcNow;

                foreach (var company in userCompany)
                {
                    var receivers = company.Users.Where(u => u.IsProjectLeader);
                    if (receivers != null)
                    {
                        foreach (var receiver in receivers)
                        {
                            message.Recipients.Add(new MessageUser()
                            {
                                MBEUserId = receiver.MBEUserId,
                                CompanyId = company.Id,
                                Timestamp = DateTime.UtcNow
                            });
                        }
                        var IsMailSent = await ObjectLocator.EmailService.SendEmailForDoorMessages(sendingUser.FirstOrDefault(), receivers.Select(r => r.Email).ToList(), project, message, userCompany.FirstOrDefault().Name);
                        if (IsMailSent)
                        {
                            IsMailSentCount++;
                        }
                    }
                }
                await messageRepository.AddMessage(message);
                return message;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IssueMessage> AddIssueMessage(IssueMessage issueMessage, string userId)
        {
            int IsMailSentCount = 0;
            try
            {
                var companies = await ObjectLocator.CompanyService.GetCompaniesByProjectId(issueMessage.ProjectId);
                issueMessage.Recipients = new List<MessageUser>();
                var sendingUser = companies.SelectMany(c => c.Users).Where(u => u.MBEUserId.Equals(userId));

                var project = await ObjectLocator.ProjectService.GetProject(issueMessage.ProjectId);
                issueMessage.Sender.MBEUserId = userId;
                issueMessage.Sender.Timestamp = DateTime.UtcNow;

                foreach (var companyId in issueMessage.RecipientCompanyIds)
                {
                    var userCompany = companies.Find(x => x.Id == companyId);
                    var receivers = userCompany.Users.Where(u => u.IsEmailRecipient);
                    if (receivers != null)
                    {
                        foreach (var receiver in receivers)
                        {
                            issueMessage.Recipients.Add(new MessageUser()
                            {
                                MBEUserId = receiver.MBEUserId,
                                CompanyId = companyId,
                                Timestamp = issueMessage.sentTime
                            });
                        }
                        var IsMailSent = await ObjectLocator.EmailService.SendEmailForIssueMessage(sendingUser.FirstOrDefault(), receivers.Select(r => r.Email).ToList(), project, issueMessage, userCompany.Name);
                        if (IsMailSent)
                        {
                            IsMailSentCount++;
                        }
                    }
                }
                await issueMessageRepository.AddIsssueMessage(issueMessage);
                return issueMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }
     
        public async Task<List<Message>> GetMessages(string workorderId, string doorId)
        {
            List<Message> messages = await messageRepository.GetMessagesByDoorId(doorId, workorderId);
            return messages;
        }

        public async Task<List<Message>> GetAllMessages(string workorderId)
        {
            List<Message> messages = await messageRepository.GetAllMessagesByWorkorderId(workorderId);
            return messages;
        }

        public async Task<List<Message>> GetMessagesByDoorId(string workorderId, string doorId)
        {
            List<Message> messages = await messageRepository.GetMessagesByDoorId(doorId, workorderId);
            return messages;
        }

        public async Task<List<IssueMessage>> GetIssueMessagesByDoorNo(string doorNo, string projectId)
        {
            List<IssueMessage> issueMessages = await issueMessageRepository.GetIssueMessagesByDoorNo(doorNo, projectId);
            return issueMessages;
        }

        public async Task<List<IssueMessage>> GetAllIssueMessages(string projectId)
        {
            List<IssueMessage> issueMessages = await issueMessageRepository.GetAllIssueMessages(projectId);
            return issueMessages;
        }

        public async Task<byte[]> GetServiceMessagePicture(string messageId)
        {
            byte[] imageBytes = null;
            var message = await messageRepository.GetMessageById(messageId);

            // Convert base 64 string to byte[]
            if (message.Picture != null)
                imageBytes = Convert.FromBase64String(message.Picture);

            return imageBytes;
        }

        public async Task<byte[]> GetIssueMessagePicture(string messageId)
        {
            byte[] imageBytes = null;
            var message = await issueMessageRepository.GetMessageById(messageId);

            // Convert base 64 string to byte[]
            if (message.Picture != null)
                imageBytes = Convert.FromBase64String(message.Picture);

            return imageBytes;
        }

        public async Task<long> GetServiceMessagesCount(string workorderId, string doorId)
        {
            return await messageRepository.GetMessagesCountByDoorId(doorId, workorderId);
        }
        public async Task<long> GetIssueMessagesCount(string doorNo, string projectId)
        {
            var issueMessages = await GetIssueMessagesByDoorNo(doorNo, projectId);
            return issueMessages.Count();
        }
        
        private class MailWrapper
        {
            public Project Project { get; set; }
            public Dictionary<Company, List<CompanyUser>> receiversList { get; set; }
            public string userCompany { get; set; }
        }

      
    }
}
