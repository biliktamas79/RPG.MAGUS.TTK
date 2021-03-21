using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using MAGUS.TTK.Domain;

namespace MAGUS.TTK.CharacterEditor.BlazorPWA
{
    public class HttpFileContentResolver : IFileContentResolver
    {
        private readonly HttpClient httpClient;
        private readonly string pathPrefix;

        public HttpFileContentResolver(HttpClient httpClient, string pathPrefix = null)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.pathPrefix = pathPrefix;
        }

        public string ReadFileAsText(string filePath)
        {
            return httpClient.GetStringAsync(pathPrefix + filePath)
                .GetAwaiter().GetResult();
        }

        public Task<string> ReadFileAsTextAsync(string filePath, CancellationToken cancellationToken)
        {
            return httpClient.GetStringAsync(pathPrefix + filePath, cancellationToken);
        }
    }
}
