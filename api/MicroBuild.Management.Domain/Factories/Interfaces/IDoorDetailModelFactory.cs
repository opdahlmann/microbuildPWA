using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Factories.Interfaces
{
    public interface IDoorDetailModelFactory
    {
        DoorDetailModel GetDoorDetailsViewWithCompanyPermissions(WorkorderDoor door, List<FieldHeader> fieldHeaders, string userName);
    }
}
