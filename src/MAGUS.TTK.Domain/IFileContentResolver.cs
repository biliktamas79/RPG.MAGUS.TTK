using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAGUS.TTK.Domain
{
    public interface IFileContentResolver
    {
        //string ReadFileAsText(string filePath);
        Task<string> ReadFileAsTextAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
