using HH.ApplicationServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HH.ApplicationServices.Services.Implementations
{
    public class HttpClientCancellationService: IHttpClientServiceImplementation
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly JsonSerializerOptions options;
        public HttpClientCancellationService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
