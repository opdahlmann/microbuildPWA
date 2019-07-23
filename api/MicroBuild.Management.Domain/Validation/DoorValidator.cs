using MicroBuild.Infrastructure.Validation;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Validation.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Validation
{
    public class DoorValidator
    {
        public void ValidateDoorUpdate(List<Door> doors, string property)
        {
            doors.ForEach(door => ValidateDoorUpdate(door, property));
        }

        public void ValidateDoorUpdate(Door door, string property)
        {
            //var validator = new ObjectValidator<Door>();
            //validator.Validate(door, templateValidationSpec);
        }
    }
}
