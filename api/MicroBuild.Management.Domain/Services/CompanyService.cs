using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IDataUnitOfWork DataUnitOfWork;
        private readonly IObjectsLocator ObjectLocator;

        public CompanyService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            DataUnitOfWork = dataUnitOfWork;
            ObjectLocator = objectLocator;
        }

        public async Task<List<Company>> GetCompaniesByProjectId(string projectId)
        {
            return await DataUnitOfWork.CompanyRepository.GetCompaniesByProjectId(projectId);
        }

        public async Task<List<string>> GetUserProjectIdsByUserId(string userId)
        {
            return await DataUnitOfWork.CompanyRepository.GetUserCompanyIdsByUserId_Incorrect(userId);
        }

        public async Task<List<string>> GetUserCompanyIdsByUserId(string userId)
        {
            return await DataUnitOfWork.CompanyRepository.GetUserCompanyIdsByUserId(userId);
        }

        public async Task<List<Company>> GetUserCompaniesInProjectByUserId(string projectId, string userId)
        {
            return await DataUnitOfWork.CompanyRepository.GetUserCompaniesInProjectByUserId(projectId, userId);
        }

        public async Task<Company> AddCompany(Company company, string projectId)
        {
            company.ProjectId = projectId;
            new CompanyValidator().ValidateCompanyCreation(company);
            return await DataUnitOfWork.CompanyRepository.AddCompanyAsync(company);
        }

        public async Task<Company> UpdateCompany(Company company)
        {
            Company oldCompany = await GetCompanyById(company.Id);

            new CompanyValidator().ValidateCompanyUpdate(company, oldCompany, this.ObjectLocator.DoorService);

            await DataUnitOfWork.CompanyRepository.UpdateCompanyAsync(company);

            return company;
        }

        public async Task<Company> RemoveUserById(string companyId, string userId)
        {
            return await DataUnitOfWork.CompanyRepository.RemoveUserByIdAsync(companyId, userId);
        }

        public async Task<Company> GetCompanyById(string companyId)
        {
            return await DataUnitOfWork.CompanyRepository.GetCompanyByIdAsync(companyId);
        }

        public async Task<Company> RemoveCompany(string companyId, string projectId)
        {
            Company company = await DataUnitOfWork.CompanyRepository.GetCompanyByIdAsync(companyId);
            new CompanyValidator().ValidateCompanyDeletion(company, this.ObjectLocator.DoorService);
            await DataUnitOfWork.CompanyRepository.RemoveByIdAsync(companyId, projectId);
            return company;
        }

        #region View model transformations

        public async Task<List<KeyValuePair<string, string>>> GetAllUserNamesInCompaniesInProject(string projectId)
        {
            var userNamesList = new List<KeyValuePair<string, string>>();
            var companiesList = await GetCompaniesByProjectId(projectId);

            foreach (var company in companiesList)
            {
                foreach (var userInCompany in company.Users)
                {
                    if (!userNamesList.Any(userName => userName.Key.Equals(userInCompany.MBEUserId)))
                    {
                        userNamesList.Add(new KeyValuePair<string, string>(userInCompany.MBEUserId, userInCompany.Name));
                    }
                }
            }

            return userNamesList;
        }

        public async Task<List<KeyValuePair<string, string>>> GetAllCompanyNamesInProject(string projectId)
        {
            var companyNamesList = new List<KeyValuePair<string, string>>();
            var companiesList = await GetCompaniesByProjectId(projectId);

            foreach (var company in companiesList)
            {
                if (!companyNamesList.Any(companyName => companyName.Key.Equals(company.Id)))
                {
                    companyNamesList.Add(new KeyValuePair<string, string>(company.Id, company.Name));
                }
            }

            return companyNamesList;
        }

		public async Task DeleteBulk(string[] ids, string projectId)
		{
			foreach (var id in ids)
			{
				await DataUnitOfWork.CompanyRepository.RemoveByIdAsync(id, projectId);
			}
		}

		public async Task<IEnumerable<Company>> GetCompaniesByUserId(string userId)
		{
			return await DataUnitOfWork.CompanyRepository.GetCompaniesByUserId(userId);
		}

        public async Task<IEnumerable<Company>> GetProjectCompanyByUserId(string projectId, string userId)
        {
            var companies = await DataUnitOfWork.CompanyRepository.GetProjectCompaniesByUserId(projectId, userId);
            return companies;
        }

        public async Task<CompanyUser> GetUserByUserId(string projectId, string userId)
        {
            var companies = await DataUnitOfWork.CompanyRepository.GetProjectCompaniesByUserId(projectId, userId);
            var user= companies.FirstOrDefault().Users.Where(x => x.MBEUserId == userId).Select(x => x).ToList();
            return user.FirstOrDefault();
        }

        public async Task<Company> GetProjectCompanyForUser(string projectId, string userId)
        {
			return await DataUnitOfWork.CompanyRepository.GetProjectCompanyByUserId(projectId, userId);
		}

        #endregion
    }
}
