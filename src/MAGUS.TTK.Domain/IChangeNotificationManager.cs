using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MAGUS.TTK.Domain
{
    public interface IChangeNotificationManager
    {
        Task Changed(string field, object prevValue, object newValue);

        void Subscribe(Func<string, object, object, Task> changeNotificationCallback, Func<string, bool> fieldMatch = null);

        void Unsubscribe(Func<string, object, object, Task> changeNotificationCallback);
    }
}
