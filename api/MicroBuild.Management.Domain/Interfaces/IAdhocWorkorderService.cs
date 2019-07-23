using MicroBuild.Management.Common.DTO;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
	public interface IAdhocWorkorderService
	{
		Task<Workorder> CreateWordorderForDoor(string projectId, string companyId, string doorNr, string userId);
	}
}
