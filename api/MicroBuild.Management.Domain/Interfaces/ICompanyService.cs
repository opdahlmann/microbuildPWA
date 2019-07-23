using MicroBuild.Management.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface ICompanyService
    {
        Task<List<Company>> GetCompaniesByProjectId(string projectId);
        Task<List<string>> GetUserProjectIdsByUserId(string userId);
        Task<List<string>> GetUserCompanyIdsByUserId(string userId);
        Task<List<Company>> GetUserCompaniesInProjectByUserId(string projectId, string userId);
        Task<Company> AddCompany(Company company, string projectId);
        Task<Company> UpdateCompany(Company company);
        Task<Company> RemoveUserById(string companyId, string userId);
        Task<Company> GetCompanyById(string companyId);
        Task<Company> RemoveCompany(string companyId, string projectId);
        Task<List<KeyValuePair<string, string>>> GetAllUserNamesInCompaniesInProject(string projectId);
        Task<List<KeyValuePair<string, string>>> GetAllCompanyNamesInProject(string projectId);
		Task DeleteBulk(string[] v, string projectId);
		Task<IEnumerable<Company>> GetCompaniesByUserId(string userId);
        Task<IEnumerable<Company>> GetProjectCompanyByUserId(string projectId, string userId);
        Task<CompanyUser> GetUserByUserId(string projectId, string userId);
        Task<Company> GetProjectCompanyForUser(string projectId, string userId);
    }
}
