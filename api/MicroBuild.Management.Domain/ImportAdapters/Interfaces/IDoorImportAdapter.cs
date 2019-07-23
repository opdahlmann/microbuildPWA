using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public interface IDoorImportAdapter
    {
        Task<DoorImportSuccessStatus> ImportDoorsAsync(string mbeProjectId, string projectId, string userId);
        Task<List<Door>> GetDoorsByEngProjectId(string projectId);
    }
}
