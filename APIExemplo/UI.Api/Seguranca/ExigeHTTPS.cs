using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Api.Seguranca
{
    public class ExigeHTTPS : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.RequestUri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
            {
                HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Por razões de segurança o HTTPS é requerido!");
                return Task.FromResult(reply);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}