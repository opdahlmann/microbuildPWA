using MicroBuild.Infrastructure.Validation;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Domain.Interfaces;
using MicroBuild.Management.Domain.Validation.Specifications;

namespace MicroBuild.Management.Domain.Validation
{
    public class CompanyValidator
    {
        public void ValidateCompanyCreation(Company company)
        {
            IValidationSpecification<Company> creationSpec = new CompanyCreationSpec();
            new ObjectValidator<Company>().Validate(company, creationSpec);
        }

        public void ValidateCompanyDeletion(Company company, IDoorService doorService)
        {
            IValidationSpecification<Company> deletionSpec = new CompanyDeletionSpec(doorService);
            new ObjectValidator<Company>().Validate(company, deletionSpec);
        }

        public void ValidateCompanyUpdate(Company company, Company oldCompany, IDoorService doorService)
        {
            IValidationSpecification<Company> updateSpec = new CompanyUpdateSpec(doorService, oldCompany);
            new ObjectValidator<Company>().Validate(company, updateSpec);
        }
    }
}
