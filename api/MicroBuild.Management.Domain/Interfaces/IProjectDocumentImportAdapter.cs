using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface IProjectDocumentImportAdapter
    {
        Task<List<ProjectDocument>> GetProjectDocuments(string projectId);
    }
}
