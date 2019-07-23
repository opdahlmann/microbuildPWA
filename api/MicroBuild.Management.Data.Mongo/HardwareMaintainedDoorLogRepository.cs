using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    class HardwareMaintainedDoorLogRepository : BaseRepository<HardwareMaintainedDoorLog>, IHardwareMaintainedDoorLogRepository
    {
        public async Task AddLogEntriesInBulk(List<HardwareMaintainedDoorLog> logs)
        {
            foreach (var lg in logs)
            {
                await AddLogEntry(lg);
            }
        }

        public async Task AddLogEntry(HardwareMaintainedDoorLog log)
        {
            await GenericRepository.Add(log);
        }

        public async Task<List<HardwareMaintainedDoorLog>> GetLogEntries(string projectId, string workordertemplateId, string workorderId)
        {
            return await GenericRepository.GetAll(lg => lg.ProjectId.Equals(projectId)
                && lg.WorkorderTemplateId.Equals(workordertemplateId)
                && lg.WorkorderId.Equals(workorderId));
        }
    }
}
