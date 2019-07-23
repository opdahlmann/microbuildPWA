using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class ProjectDocument : IEntity
    {
        public string Id { get; set; }
        public byte[] BinaryData { get; set; }

        public string FileName { get; set; }

        public string FileDescription { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ProjectId { get; set; }
        public User UploadedUser { get; set; }
        public string MetaData { get; set; }

        public string ProjectDocumentName { get; set; }
        public string DocumentType { get; set; }
    }
}
