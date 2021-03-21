using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MAGUS.TTK.Domain
{
    public class LocalFileContentResolver : IFileContentResolver
    {
        private readonly string pathPrefix;

        public LocalFileContentResolver(string pathPrefix = null)
        {
            this.pathPrefix = pathPrefix;
        }

        public string ReadFileAsText(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(this.pathPrefix))
                filePath = Path.Combine(this.pathPrefix, filePath);

            return File.ReadAllText(filePath);
        }

        public Task<string> ReadFileAsTextAsync(string filePath, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(this.pathPrefix))
                filePath = Path.Combine(this.pathPrefix, filePath);

            return File.ReadAllTextAsync(filePath, cancellationToken);
        }
    }
}
