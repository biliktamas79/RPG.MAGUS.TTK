using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAGUS.TTK.Data
{
    public interface IDataInitializer<TDataContext>
        where TDataContext : class
    {
        Task InitializeData(TDataContext dataContext, CancellationToken cancellationToken = default);
    }
}
