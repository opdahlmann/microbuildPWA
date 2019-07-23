using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.API
{
    public interface IHardwareMaintainedDoorLogRepository
    {
        Task AddLogEntry(HardwareMaintainedDoorLog log);
        Task AddLogEntriesInBulk(List<HardwareMaintainedDoorLog> logs);
        Task<List<HardwareMaintainedDoorLog>> GetLogEntries(string projectId, string workordertemplateId, string workorderId);
    }
}
