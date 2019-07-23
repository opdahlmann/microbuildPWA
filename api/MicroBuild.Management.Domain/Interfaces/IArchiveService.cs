using MicroBuild.Management.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IArchiveService
    {
        Task<MemoryStream> CreateProjectBackup(string projectId, string userId, HttpRequestMessage httpReq);

        Task<bool> RestoreArchive(string contentType, Stream fileContent, string userId);
    }
}
