using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MAGUS.TTK.Domain
{
    public class DictionaryBasedChangeNotificationManager : IChangeNotificationManager
    {
        private readonly Dictionary<Func<string, object, object, Task>, Func<string, bool>> Subscriptions = new Dictionary<Func<string, object, object, Task>, Func<string, bool>>();

        public async Task Changed(string field, object prevValue, object newValue)
        {
            if ((this.Subscriptions != null) && (this.Subscriptions.Count > 0))
            {
                foreach (var kvp in this.Subscriptions)
                {
                    // if there is no filter or this change matches the filter
                    if ((kvp.Value == null) || kvp.Value(field))
                    {
                        // then we notify that subscriber about this change
                        await kvp.Key(field, prevValue, newValue);
                    }
                }
            }
        }

        public void Subscribe(Func<string, object, object, Task> changeNotificationCallback, Func<string, bool> fieldMatch = null)
        {
            this.Subscriptions.Add(changeNotificationCallback, fieldMatch);

            if (fieldMatch == null)
                System.Console.WriteLine($"Subscription {changeNotificationCallback} added.");
            else
                System.Console.WriteLine($"Subscription {changeNotificationCallback} added with filter {fieldMatch}.");
        }

        public void Unsubscribe(Func<string, object, object, Task> changeNotificationCallback)
        {
            bool removed = this.Subscriptions.Remove(changeNotificationCallback);

            if (removed)
                System.Console.WriteLine($"Subscription {changeNotificationCallback} removed.");
            else
                System.Console.WriteLine($"Subscription {changeNotificationCallback} could not be removed.");
        }
    }
}
