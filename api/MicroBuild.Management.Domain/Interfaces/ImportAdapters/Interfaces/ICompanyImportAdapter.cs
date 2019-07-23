using MicroBuild.Management.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.ImportAdapters
{
    public interface ICompanyImportAdapter
	{
		Task<List<MBECompany>> GetExternalCompanies();
	}
}
