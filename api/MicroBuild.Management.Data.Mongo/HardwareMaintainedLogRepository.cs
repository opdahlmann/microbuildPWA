using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    class HardwareMaintainedLogRepository : BaseRepository<HardwareMaintainedLog>, IHardwareMaintainedLogRepository
    {
        public async Task AddLogEntriesInBulk(List<HardwareMaintainedLog> logs)
        {
            foreach (var lg in logs)
            {
                await AddLogEntry(lg);
            }
        }

        public async Task AddLogEntry(HardwareMaintainedLog log)
        {
            await GenericRepository.Add(log);
        }

        public async Task<List<HardwareMaintainedLog>> GetLogEntries(string projectId, string workordertemplateId, string workorderId)
        {
            return await GenericRepository.GetAll(lg => lg.Metadata.ProjectId.Equals(projectId)
                && lg.Metadata.WorkorderTemplateId.Equals(workordertemplateId)
                && lg.Metadata.WorkorderId.Equals(workorderId));
        }
    }
}
