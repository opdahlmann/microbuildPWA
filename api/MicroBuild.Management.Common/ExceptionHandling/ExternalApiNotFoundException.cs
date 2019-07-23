using MicroBuild.Infrastructure.Exceptions;

namespace MicroBuild.Management.Common.ExceptionHandling
{
	public class ExternalApiNotFoundException : BaseException
	{
		public ExternalApiNotFoundException(string message) : base(message) { }
	}
}
