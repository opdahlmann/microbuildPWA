using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public interface IJsonToExcelAdapter
    {
        Task<string> CreateExcelViewForAdmin(string mbeProjectId, object content);

        string API_URL { get; }
    }
}
