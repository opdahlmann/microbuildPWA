using MicroBuild.Infrastructure.Models;
using MicroBuild.Infrastructure.Mongo;

namespace MicroBuild.Management.Data.Mongo
{
	public abstract class BaseRepository<TEntity> where TEntity : IEntity
	{
		protected GenericRepository<TEntity> GenericRepository { get; } = new GenericRepository<TEntity>();
	}
}
