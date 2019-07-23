using MicroBuild.Infrastructure.Models;
using MicroBuild.Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.PWA.Mongo
{
    public abstract class BaseRepository<TEntity> where TEntity : IEntity
    {
        protected GenericRepository<TEntity> GenericRepository { get; } = new GenericRepository<TEntity>();
    }
}
