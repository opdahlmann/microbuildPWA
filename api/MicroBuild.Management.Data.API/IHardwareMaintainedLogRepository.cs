using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IHardwareMaintainedLogRepository
    {
        Task AddLogEntry(HardwareMaintainedLog log);

        Task AddLogEntriesInBulk(List<HardwareMaintainedLog> logs);

        Task<List<HardwareMaintainedLog>> GetLogEntries(string projectId, string workordertemplateId, string workorderId);
    }
}
