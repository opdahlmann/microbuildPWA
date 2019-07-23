using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ExceptionHandling;
using MicroBuild.Management.Common.Models;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.WebApiUtils.Authentication;
using MicroBuild.WebApiUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroBuild.Management.API.Controllers
{
    [ControllerExceptionFilter("Management.admin")]
    public class ChecklistController : ApiController
    {
        private IObjectsLocator ObjectsLocator;

        public ChecklistController(IObjectsLocator objectsLocator)
        {
            ObjectsLocator = objectsLocator;
        }

        [MicroBuildAuthorize]
        [HttpPost]
        [Route("checklist")]
        public async Task<HttpResponseMessage> CreateChecklist(Checklist checklist)
        {
            var createdChecklist = await this.ObjectsLocator.ChecklistService.CreateChecklist(checklist);
            return Request.CreateResponse(HttpStatusCode.OK, createdChecklist);
        }

        [MicroBuildAuthorize]
        [HttpPut]
        [Route("checklist")]
        public async Task<HttpResponseMessage> UpdateChecklist(Checklist checklist)
        {
            var createdChecklist = await this.ObjectsLocator.ChecklistService.UpdateChecklist(checklist);
            return Request.CreateResponse(HttpStatusCode.OK, createdChecklist);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("project/{projectId}/checklist/getall")]
        public async Task<HttpResponseMessage> GetAll(string projectId)
        {
            var createdChecklist = await this.ObjectsLocator.ChecklistService.GetAll(projectId);
            return Request.CreateResponse(HttpStatusCode.OK, createdChecklist);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("project/{projectId}/checklist/{checklistId}/isexist")]
        public async Task<HttpResponseMessage> IsExist(string projectId, string checklistId)
        {
            var createdChecklist = await this.ObjectsLocator.ChecklistService.IsExist(projectId, checklistId);
            return Request.CreateResponse(HttpStatusCode.OK, createdChecklist);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("project/{projectId}/checklist/unique/{checklistName}")]
        public async Task<HttpResponseMessage> IsUniqueName(string projectId, string checklistName)
        {
            var createdChecklist = await this.ObjectsLocator.ChecklistService.IsUniqueName(projectId, checklistName);
            return Request.CreateResponse(HttpStatusCode.OK, createdChecklist);
        }

        [MicroBuildAuthorize]
        [HttpPut]
        [Route("doors/hardware/checklist/{checklistId}/attach")]
        public async Task<HttpResponseMessage> SetChecklistForHardware(string checklistId, List<HardwareInDoorsRequestModel> hardwareList)
        {
            var updatedItemCount = await this.ObjectsLocator.ChecklistService.AttachChecklistForHardwareCollectionItems(checklistId, hardwareList);
            return Request.CreateResponse(HttpStatusCode.OK, updatedItemCount);
        }

        [MicroBuildAuthorize]
        [HttpDelete]
        [Route("project/checklist/{checklistId}")]
        public async Task<HttpResponseMessage> DeleteChecklist(string checklistId)
        {
            var savedChecklist = await this.ObjectsLocator.ChecklistService.DeleteChecklist(checklistId);
            return Request.CreateResponse(HttpStatusCode.OK, savedChecklist);
        }

        [MicroBuildAuthorize]
        [HttpGet]
        [Route("project/workorder/checklist/{checklistId}")]
        public async Task<HttpResponseMessage> GetChecklistByChecklistId(string checklistId)
        {
            var checklist = await this.ObjectsLocator.ChecklistService.GetChecklistByChecklistId(checklistId);
            return Request.CreateResponse(HttpStatusCode.OK, checklist);
        }

    }
}
