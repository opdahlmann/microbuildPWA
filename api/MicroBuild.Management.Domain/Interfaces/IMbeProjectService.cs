using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.DTO.Models;
using MicroBuild.Management.Common.MBEModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IMbeProjectService
	{
        Task<List<MbeProject>> GetMbeProjectsAsync(string userId, HttpRequestMessage request);
        Task<List<FieldHeader>> GetMbeProjectFieldHeaders(string mbeProjectId);
		Task<ProjectImportSuccessStatus> ImportFromMbe(MbeProject project, string userId);
	}
}
