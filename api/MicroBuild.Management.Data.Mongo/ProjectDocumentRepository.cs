using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Data.Mongo
{
    public class ProjectDocumentRepository : BaseRepository<ProjectDocument>, IProjectDocumentRepository
    {
        public async Task AddProjectDocumentsAsync(List<ProjectDocument> list)
        {
            await GenericRepository.Add(list);
        }
    }
}
