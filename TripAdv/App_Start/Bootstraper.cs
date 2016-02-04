using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TripAdv
{
    /// <summary>
    /// Povoleni vsech REST metod pro aplikaci
    /// </summary>
    public class XHttpMethodDelegatingHandler : DelegatingHandler
    {
        private static readonly string[] _allowedHttpMethods = { "PUT", "DELETE", "GET", "POST", "OPTIONS" };
        private static readonly string _httpMethodHeader = "X-HTTP-Method";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post && request.Headers.Contains(_httpMethodHeader))
            {
                string httpMethod = request.Headers.GetValues(_httpMethodHeader).FirstOrDefault();
                if (_allowedHttpMethods.Contains(httpMethod, StringComparer.InvariantCultureIgnoreCase))
                    request.Method = new HttpMethod(httpMethod);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }

    public static class Bootstraper
    {
        public static void Configure()
        {
            //ConfigureAutofacContainer();
            
            Mapper.Initialize(mapper =>
            {
                mapper.AddProfile<TripAdv.Automapper.TripMappingProfile>();
            });

        }

    }
}