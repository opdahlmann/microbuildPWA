using MicroBuild.Management.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services.Domain
{
   public class DomainObjectFactory
    {
        private IDataUnitOfWork _dataUnitOfWork;

        public DomainObjectFactory(IDataUnitOfWork dataUnitOfWork)
        {
            _dataUnitOfWork = dataUnitOfWork;
        }

        public ProjectDomain ProjectDomain
        {
            get
            {
                return new ProjectDomain(_dataUnitOfWork.ProjectRepository);
            }
        }

        public ProjectDocumentDomain ProjectDocumentDomain
        {
            get
            {
                return new ProjectDocumentDomain(_dataUnitOfWork.ProjectDcoumentRepository);
            }
        }

        //public DoorDomain DoorDomain
        //{
        //    get
        //    {
        //        return new DoorDomain();
        //    }
        //}
    }
}
