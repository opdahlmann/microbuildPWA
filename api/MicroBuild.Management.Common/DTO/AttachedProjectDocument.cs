using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class AttachedProjectDocument
    {
        public string Id { get; set; }

        private string _documentType;

        public string DocumentType
        {
            get { return _documentType; }
            set { if (value != _documentType) { _documentType = value; } }
        }
    }
}
