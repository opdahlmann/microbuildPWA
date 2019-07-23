using MicroBuild.Infrastructure.Validation;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Validation.Specifications
{
    internal class CompanyDeletionSpec : IValidationSpecification<Company>
    {
        private IDoorService DoorService;

        public CompanyDeletionSpec(IDoorService doorClient)
        {
            DoorService = doorClient;
        }

        public ValidationResult Validate(Company sourceCompany)
        {
            ValidationResult result = new ValidationResult() { Failure = false };

            //Task.Run(async () =>
            //{
            //  //
            //}).GetAwaiter().GetResult();

            return result;
        }
    }
}
