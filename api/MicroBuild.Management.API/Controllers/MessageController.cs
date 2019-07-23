using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using MicroBuild.Management.API.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;

namespace MicroBuild.Management.API.Controllers
{
    public class MessageController : ApiController
    {
        private IObjectsLocator ObjectsLocator;

        public MessageController(IObjectsLocator objectsLocator)
        {
            ObjectsLocator = objectsLocator;
        }

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("projects/door/messages")]
        public async Task<HttpResponseMessage> SendMessageForDoor(Message message)
        {
            string userId = RequestContext.GetUserId();

			var company = await ObjectsLocator.CompanyService.GetUserCompaniesInProjectByUserId(message.ProjectId, userId);

			var _message = await ObjectsLocator.MessageService.AddServiceMessage(message, userId);
			
			var messageViewModel = CreateViewModel(message.ProjectId, _message, company, userId);

			return Request.CreateResponse(HttpStatusCode.OK, messageViewModel);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/workorder/{workorderId}/door/{doorId}/messages")]
        public async Task<HttpResponseMessage> GetMessages(string projectId, string workorderId, string doorId)
        {
            string userId = RequestContext.GetUserId();
            
			var company = await ObjectsLocator.CompanyService.GetUserCompaniesInProjectByUserId(projectId,userId);
            var door = await ObjectsLocator.WorkorderDoorService.GetWorkOrderDoorByDoorId(workorderId, doorId);

            var messages = await ObjectsLocator.MessageService.GetMessages(workorderId, doorId);
            //var issueMessages = await ObjectsLocator.MessageService.GetIssueMessagesByDoorNo(door.Door.DoorNo, projectId);

            var serviceMessageDisplayList = messages.Select(x => CreateViewModel(projectId, x, company, userId)).ToArray();
            //var issueMessageDisplayList = issueMessages.Select(x => CreateViewModel(projectId, x, company, userId)).ToArray();
            //var doorMessageDisplayList = serviceMessageDisplayList.Concat(issueMessageDisplayList);
          

			return Request.CreateResponse(HttpStatusCode.OK, serviceMessageDisplayList);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/workorder/{workorderId}/messages")]
        public async Task<HttpResponseMessage> GetAllMessages(string projectId, string workorderId)
        {
            string userId = RequestContext.GetUserId();

			var company = await ObjectsLocator.CompanyService.GetUserCompaniesInProjectByUserId(projectId, userId);

			var messages = await ObjectsLocator.MessageService.GetAllMessages(workorderId);
            //var issueMessages = await ObjectsLocator.MessageService.GetAllIssueMessages(projectId);

           var serviceMessageDisplayList = messages.Select(x => CreateViewModel(projectId, x, company, userId)).ToArray();
           //var issueMessageDisplayList = issueMessages.Select(x => CreateViewModel(projectId, x, company, userId)).ToArray();
           //var doorMessageDisplayList = serviceMessageDisplayList.Concat(issueMessageDisplayList);

            return Request.CreateResponse(HttpStatusCode.OK, serviceMessageDisplayList);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("projects/{projectId}/workorder/{workorderId}/{doorId}/messages")]
        public async Task<HttpResponseMessage> GetMessagesByDoorId(string projectId, string workorderId,string doorId)
        {
            string userId = RequestContext.GetUserId();
            var company = await ObjectsLocator.CompanyService.GetUserCompaniesInProjectByUserId(projectId, userId);
            var door = await ObjectsLocator.WorkorderDoorService.GetWorkOrderDoorByDoorId(workorderId, doorId);

            var messages = await ObjectsLocator.MessageService.GetMessagesByDoorId(workorderId, doorId);
            //var issueMessages = await ObjectsLocator.MessageService.GetIssueMessagesByDoorNo(door.Door.DoorNo, projectId);

            var serviceMessageDisplayList = messages.Select(x => CreateViewModel(projectId, x, company, userId)).ToArray();
            //var issueMessageDisplayList = issueMessages.Select(x => CreateViewModel(projectId, x, company, userId)).ToArray();
            //var doorMessageDisplayList = serviceMessageDisplayList.Concat(issueMessageDisplayList);

            return Request.CreateResponse(HttpStatusCode.OK, serviceMessageDisplayList);
        }

        [HttpGet]
        [Route("projects/{projectId}/doors/messages/{messageId}/view/image")]
        public async Task<HttpResponseMessage> GetMessagesPicture(string projectId, string messageId)
        {
            var messagePictureAsByteArray = await ObjectsLocator.MessageService.GetServiceMessagePicture(messageId);

            return Request.CreateResponse(HttpStatusCode.OK, messagePictureAsByteArray);
        }

        //[HttpGet]
        //[Route("projects/{projectId}/issuemessages/{issuemessageId}/view/image")]
        //public async Task<HttpResponseMessage> GetIssueMessagesPicture(string projectId, string issuemessageId)
        //{
        //    var messagePictureAsByteArray = await ObjectsLocator.MessageService.GetIssueMessagePicture(issuemessageId);

        //    return Request.CreateResponse(HttpStatusCode.OK, messagePictureAsByteArray);
        //}

        private DoorMessageDisplayViewModel CreateViewModel<T>(string projectId, T message, List<Company> companies, string userId) where T:IMessage
		{
            var company = companies?.FirstOrDefault(c => c.Id.Equals(message.Sender.CompanyId));
			var sender = company?.Users?.FirstOrDefault(u => u.MBEUserId == message.Sender.MBEUserId);

            return new DoorMessageDisplayViewModel
            {
                Id = message.Id,
                DoorNo = message.DoorNo,
                Subject = message.Subject,
                Body = message.Body,
                IsHandled = message.IsHandled,
                SentTime = message.Sender.Timestamp,
                SenderName = sender?.Name,
                SenderCompanyName = company?.Name,
                HasPicture = !string.IsNullOrEmpty(message.Picture),
                //IsIssueMessage = (message.GetType()==typeof(IssueMessage)) ? true : false,
			};
	}

        //[MicroBuildAuthorize]
        //[HttpPost]
        //[Route("projects/door/issuemessages")]
        //public async Task<HttpResponseMessage> SendIssueMessageForDoor(IssueMessage message)
        //{
        //    string userId = RequestContext.GetUserId();

        //    var _issueMessage = await ObjectsLocator.MessageService.AddIssueMessage(message, userId);

        //    return Request.CreateResponse(HttpStatusCode.OK, _issueMessage);
        //}
    }
}
