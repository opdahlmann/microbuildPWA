using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroBuild.Management.Common.DTO;

namespace MicroBuild.Management.Data.API
{
    public interface IProjectDocumentRepository
    {
        Task AddProjectDocumentsAsync(List<ProjectDocument> list);
    }
}
