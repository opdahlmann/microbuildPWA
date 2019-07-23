using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.Util;
using MicroBuild.Management.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public partial class DoorService : IDoorService
    {
        private IDataUnitOfWork DataUnitOfWork { get; set; }
        private IObjectsLocator ObjectLocator { get; set; }

        public DoorService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            this.DataUnitOfWork = dataUnitOfWork;
            this.ObjectLocator = objectLocator;
        }

        #region GET DOORS

        public async Task<List<Door>> GetDoorsByProjectIdAsync(string projectId)
        {
            return await DataUnitOfWork.DoorRepository.GetDoorsByProjectIdAsync(projectId);
        }

        public async Task<Door> GetDoorById(string doorId)
        {
            return await this.DataUnitOfWork.DoorRepository.GetSpecificDoorAsync(doorId);
        }

        public async Task<List<DoorDetailModel>> GetDoorsByProjectIdAsync(string projectId, string userId)
        {
            var fieldHeaders = await this.ObjectLocator.MbeProjectService.GetMbeProjectFieldHeaders(projectId);

            var userCompanies = await this.ObjectLocator.CompanyService.GetUserCompaniesInProjectByUserId(projectId, userId);
            var userCompanyIds = userCompanies.Select(_ => _.Id).ToList();

            var doors = await GetDoorsByProjectIdAsync(projectId);
            var DoorDetailModels = (new DoorUtil()).GetDoorDetailModels(doors, fieldHeaders, userCompanyIds);
            return DoorDetailModels;
        }

        private async Task<List<Door>> GetSpecificDoorsInProject(string projectId, List<string> doorIds)
        {
            return await this.DataUnitOfWork.DoorRepository.GetSpecificDoorsAsync(projectId, doorIds);
        }

		public async Task<Door> GetDoorByDoorNo(string projectId, string doorNr)
		{
			return await DataUnitOfWork.DoorRepository.GetDoorByDoorNo(projectId, doorNr);
		}

		#endregion GET DOORS

		#region UPDATE DOORS

		public async Task<long> BulkUpdateAsync<T>(string projectId, string changingProperty, T valueForProperty, List<string> changingDoorIds)
        {
            var changingDoors = await GetSpecificDoorsInProject(projectId, changingDoorIds);
            return await BulkUpdateAsync(changingProperty, valueForProperty, changingDoors, changingDoorIds);
        }

        private async Task<long> BulkUpdateAsync<T>(string changingProperty, T valueForProperty, List<Door> changingDoors, List<string> changingDoorIds)
        {
            new DoorValidator().ValidateDoorUpdate(changingDoors, changingProperty);
            return await this.DataUnitOfWork.DoorRepository.UpdateManyAsync(changingDoorIds, changingProperty, valueForProperty);
        }

        #endregion UPDATE DOORS

        #region CREATE DOORS

        public async Task<Dictionary<string, string>> CreateDoorsBulk(string projectId, IEnumerable<Door> doors)
        {
            foreach (var door in doors)
            {
                door.ProjectId = projectId;
            }
            return await this.DataUnitOfWork.DoorRepository.AddDoorsAsyncWithIdMap(doors);
        }

        #endregion

        #region DELETE DOORS

        public async Task DeleteDoorsByProject(string projectId, string userId)
        {
            await this.DataUnitOfWork.DoorRepository.DeleteByProjectId(projectId);
        }

		public async Task DeleteBulk(string newProjectId)
		{
			await this.DataUnitOfWork.DoorRepository.DeleteByProjectId(newProjectId);
		}

		#endregion
	}
}
