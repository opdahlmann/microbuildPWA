using MicroBuild.Infrastructure.Validation;
using MicroBuild.Management.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Validation.Specifications
{
    public class CompanyCreationSpec : IValidationSpecification<Company>
    {
        public CompanyCreationSpec()
        { }

        public ValidationResult Validate(Company source)
        {
            ValidationResult result = new ValidationResult() { Failure = false };

            return result;
        }
    }
}
