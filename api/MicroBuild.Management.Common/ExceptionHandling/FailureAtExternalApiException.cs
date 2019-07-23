using MicroBuild.Infrastructure.Exceptions;

namespace MicroBuild.Management.Common.ExceptionHandling
{
	public class FailureAtExternalApiException : BaseException
	{
		public FailureAtExternalApiException(string message) : base(message) { }
	}
}
