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
    public class CompanyUpdateSpec : IValidationSpecification<Company>
    {
        private IDoorService DoorService;

        private Company oldCompany;

        public CompanyUpdateSpec(IDoorService doorService, Company oldCompany)
        {
            DoorService = doorService;
            this.oldCompany = oldCompany;
        }

        public ValidationResult Validate(Company sourceCompany)
        {
            ValidationResult result = new ValidationResult() { Failure = false };

            //Task.Run(async () =>
            //{
            //   //
            //}).GetAwaiter().GetResult();

            return result;
        }
    }
}
