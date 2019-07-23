using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class WorkorderTemplateHardwareCollectionService : IWorkorderTemplateHardwareCollectionService
    {
        private IWorkorderTemplateHardwareCollectionRepository _workorderTemplateHardwareCollectionRepository;

        public WorkorderTemplateHardwareCollectionService(IDataUnitOfWork dataUnitOfWork)
        {
            _workorderTemplateHardwareCollectionRepository = dataUnitOfWork.WorkorderTemplateHardwareCollectionRepository;
        }

		public async Task<Dictionary<string, string>> CreateBulkAsync(string newProjectId, IEnumerable<WorkorderTemplateHardwareCollection> items)
		{
			foreach (var item in items)
			{
				item.ProjectId = newProjectId;
			}
			return await _workorderTemplateHardwareCollectionRepository.AddWithIdMapAsync(items);
		}

		public async 
			Task<IEnumerable<WorkorderTemplateHardwareCollection>> 
			CreateHardwareCollections(
				IEnumerable<WorkorderTemplateHardwareCollection> hardwareCollections, 
				string workorderTemplateId, 
				string projectId
			)
        {
            try
            {
				hardwareCollections = hardwareCollections
					.Where(mapping => mapping.FieldName != null && mapping.Content != null)
					.GroupBy(mapping => $"{mapping.FieldName}###{mapping.Content}")
					.Select(group => group.First());

				foreach (var item in hardwareCollections)
                {
					item.TemplateId = workorderTemplateId;
					item.ProjectId = projectId;
					await _workorderTemplateHardwareCollectionRepository.CreateHardwareCollection(item);
                }

                return hardwareCollections;
            }
            catch (Exception)
            {
                return null;
            }
        }

		public async Task DeleteBulk(string[] ids)
		{
			await _workorderTemplateHardwareCollectionRepository.DeleteBulk(ids);
		}

		public async Task DeleteHardwareCollections(string templateId)
		{
			await _workorderTemplateHardwareCollectionRepository.RemoveAllHardwareCollections(templateId);
		}

		public async Task<IEnumerable<WorkorderTemplateHardwareCollection>> GetHardwareCollections(string templateId)
		{
			try
			{
				return await _workorderTemplateHardwareCollectionRepository.GetHardwareCollections(templateId);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<WorkorderTemplateHardwareCollection>> GetHardwareCollectionsByProjectId(string projectId)
		{
			try
			{
				return await _workorderTemplateHardwareCollectionRepository.GetHardwareCollectionsByProjectId(projectId);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}