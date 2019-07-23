using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services.Domain
{
    public class ProjectDocumentDomain
    {
        private IProjectDocumentRepository _projectDocumentRepository;

        public ProjectDocumentDomain(IProjectDocumentRepository projectDocumentRepository)
        {
            _projectDocumentRepository = projectDocumentRepository;
        }

        public async Task ImportProjectDocuments(string mbeProjectId, string projectId, IProjectDocumentImportAdapter projectDocumentImportAdapter)
        {
            List<ProjectDocument> projectDocuments = new List<ProjectDocument>();

            projectDocuments = await projectDocumentImportAdapter.GetProjectDocuments(mbeProjectId);

            var list = projectDocuments.Select(x => { x.Id = null; return x; }).ToList();
            list.ForEach(d => SetProjectId(d, projectId));

            await _projectDocumentRepository.AddProjectDocumentsAsync(list);
        }

        private void SetProjectId(ProjectDocument document, string projectId)
        {
            document.Id = string.Empty;
            document.ProjectId = projectId;
        }
    }
}
