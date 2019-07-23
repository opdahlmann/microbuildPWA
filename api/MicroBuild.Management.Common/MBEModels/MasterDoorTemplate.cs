using System.Collections.Generic;

namespace MicroBuild.Management.Common.MBEModels
{
	public class MasterDoorTemplate
	{
		public List<MasterDoorTemplateStructure> DoorTemplateStructure { get; set; }
	}

	public class MasterDoorTemplateStructure
	{
		public string FieldName { get; set; }
		public List<LanguageHeader> LanguageHeaders { get; set; }
        public DoorFieldType FieldType { get; set; }

    }

	public class LanguageHeader
	{
		public int Language { get; set; }
		public string Header { get; set; }
	}
}
