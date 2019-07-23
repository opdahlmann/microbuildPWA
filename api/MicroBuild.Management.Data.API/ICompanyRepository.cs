using System.Collections.Generic;
using System.Threading.Tasks;

using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Data.API
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompaniesByProjectId(string projectId);

        Task<List<string>> GetUserCompanyIdsByUserId_Incorrect(string userId);

        Task<List<string>> GetUserCompanyIdsByUserId(string userId);

        Task<List<Company>> GetUserCompaniesInProjectByUserId(string projectId, string userId);

        Task<Company> AddCompanyAsync(Company company);

        Task UpdateCompanyAsync(Company company);

        Task<Company> GetCompanyByIdAsync(string companyId);

        Task<Company> RemoveUserByIdAsync(string companyId, string userId);

        Task RemoveByIdAsync(string companyId, string projectId);

		Task<IEnumerable<Company>> GetCompaniesByUserId(string userId);

        Task<IEnumerable<Company>> GetProjectCompaniesByUserId(string projectId, string userId);
		
		Task<Company> GetProjectCompanyByUserId(string projectId, string userId);
	}
}
