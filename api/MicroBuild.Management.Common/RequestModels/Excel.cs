using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.RequestModels
{
    public class Excel
    {
        public ExcelHeaderSection Header { get; set; }
        public List<ExcelColumnHeader> ColumnHeaders { get; set; }

        public IEnumerable<object> RowValues { get; set; }

        public List<ExcelCellOption> Options { get; set; }
        public List<ExcellSheetConfig> Configs { get; set; }
    }

    public class ExcelHeaderSection
    {
        public List<ExcelPageHeader> LeftHeader { get; set; }
        public List<ExcelPageHeader> RightHeader { get; set; }
    }

    public class ExcelPageHeader
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }

    public class ExcelColumnHeader
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }

    public class ExcelCellOption
    {
        public int Index { get; set; }
        public string FieldName { get; set; }
        public ExcelCellOptionValue Value { get; set; }

        public ExcelCellOption(int index, string fieldName, ExcelCellOptionValue value)
        {
            Index = index;
            FieldName = fieldName;
            Value = value;
        }
    }

    public class ExcellSheetConfig
    {
        public string Prop { get; set; }
        public string Value { get; set; }

        public ExcellSheetConfig(string prop, string value)
        {
            Prop = prop;
            Value = value;
        }
    }

    public class ExcelCellOptionValue
    {
        public string StyleProperty { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public ExcelCellOptionValue(string styleProp, string type, string value)
        {
            StyleProperty = styleProp;
            Type = type;
            Value = value;
        }
    }

    public class ExcelExportResult
    {
        public string url { get; set; }

        //TODO: Map other fields too, if required.
    }
}
