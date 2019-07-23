using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;

namespace MicroBuild.Management.Domain.Services
{
	public abstract class BaseService
	{
		public IDataUnitOfWork DataUnitOfWork { get; }
		public IObjectsLocator ObjectLocator { get; }

		public BaseService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
		{
			DataUnitOfWork = dataUnitOfWork;
			ObjectLocator = objectLocator;
		}

		public BaseService(IDataUnitOfWork dataUnitOfWork)
		{
			DataUnitOfWork = dataUnitOfWork;
		}

		public BaseService(IObjectsLocator objectLocator)
		{
			ObjectLocator = objectLocator;
		}

		public BaseService()
		{ }
	}
}
