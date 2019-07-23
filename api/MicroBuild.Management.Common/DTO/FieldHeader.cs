using MicroBuild.Management.Common.MBEModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    public class FieldHeader
    {
        public string FieldName;
        public string Header;
        public DoorFieldType FieldType;

        public FieldHeader(string fieldName, string header, DoorFieldType fieldType)
        {
            FieldName = fieldName;
            Header = header;
            FieldType = fieldType;
        }
    }
}
