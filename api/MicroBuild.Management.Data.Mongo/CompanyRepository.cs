using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public async Task<Company> AddCompanyAsync(Company company)
        {
            return await GenericRepository.Add(company);
        }

        public async Task<List<Company>> GetCompaniesByProjectId(string projectId)
        {
            return await GenericRepository.GetAll(x => x.ProjectId.Equals(projectId));
        }

        public async Task<List<string>> GetUserCompanyIdsByUserId_Incorrect(string userId)
        {
            // TODO: this returns the PROJECT IDs. the name and usage is incorrect. fix this. duplicated underneath for the correct functionality requirements.
            var companies = await GenericRepository.GetAll(x => x.Users.Any(e => e.MBEUserId.Equals(userId)));
            return companies.Select(e => e.ProjectId).Distinct().ToList();
        }

        public async Task<List<string>> GetUserCompanyIdsByUserId(string userId)
        {
            var companies = await GenericRepository.GetAll(x => x.Users.Any(e => e.MBEUserId.Equals(userId)));
            return companies.Select(e => e.Id).Distinct().ToList();
        }

        public async Task<List<Company>> GetUserCompaniesInProjectByUserId(string projectId, string userId)
        {
            return await GenericRepository.GetAll(x => x.ProjectId.Equals(projectId) && x.Users.Any(e => e.MBEUserId.Equals(userId)));
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            await GenericRepository.UpdateById(company);
        }

        public async Task<Company> GetCompanyByIdAsync(string companyId)
        {
            return await GenericRepository.GetById(companyId);
        }

        public async Task<Company> RemoveUserByIdAsync(string companyId, string userId)
        {
            var collection = GenericRepository.GetCollection();
            var filter = Builders<Company>.Filter.Eq(c => c.Id, companyId);
            var update = Builders<Company>.Update.PullFilter("Users", Builders<CompanyUser>.Filter.Eq(u => u.MBEUserId, userId));
            Company company = await collection.FindOneAndUpdateAsync<Company>(filter, update);
            company = await collection.Find(filter).FirstOrDefaultAsync();
            return company;
        }

        public async Task RemoveByIdAsync(string companyId, string projectId)
        {
            await GenericRepository.RemoveAllByCriteria(c => c.Id.Equals(companyId) && c.ProjectId.Equals(projectId));
        }

		public async Task<IEnumerable<Company>> GetCompaniesByUserId(string userId)
		{
			var coll = GenericRepository.GetCollection();

			var filter = Builders<Company>.Filter.Or(
				Builders<Company>.Filter.ElemMatch(x => x.Users, u => u.MBEUserId == userId)
			);
           
			return await coll.Find(filter).ToListAsync();
		}

        public async Task<IEnumerable<Company>> GetProjectCompaniesByUserId(string projectId, string userId)
        {
            var coll = GenericRepository.GetCollection();

            var filter = Builders<Company>.Filter.And(
                Builders<Company>.Filter.Eq(x => x.ProjectId, projectId),
                Builders<Company>.Filter.ElemMatch(x => x.Users, u => u.MBEUserId == userId)
            );

            return await coll.Find(filter).ToListAsync();
        }

		public async Task<Company> GetProjectCompanyByUserId(string projectId, string userId)
		{
			return await GenericRepository.Collection.Find(c => c.ProjectId == projectId && c.Users.Any(u => u.MBEUserId == userId)).SingleAsync();
		}
	}
}
