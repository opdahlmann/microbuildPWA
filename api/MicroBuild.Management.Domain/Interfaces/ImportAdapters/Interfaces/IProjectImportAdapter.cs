using System.Collections.Generic;
using System.Threading.Tasks;
using MicroBuild.Management.Common.MBEModels;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public interface IProjectImportAdapter
    {
		Task<List<MbeProject>> GetUserProjectsAsync();

		Task<MbeProject> GetProjectByIdAsync(string projectId);

        Task<MasterDoorTemplate> GetMasterTemplate();

        Task<bool> SetProgressProjectId(string engProjectId, string progProjectId);

		Task<bool> SetManagementProjectId(string engProjectId, string progProjectId);
    }
}
