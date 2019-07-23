using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationsAsync();

    }
}
