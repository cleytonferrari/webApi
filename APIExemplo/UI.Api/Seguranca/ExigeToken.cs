using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using UI.Api.Aplicacao;
using UI.Api.Helpers;

namespace UI.Api.Seguranca
{
    public class ExigeToken : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            const string tokenName = "X-Token";

            if (request.Headers.Contains(tokenName))
            {
                var tokenCriptografado = request.Headers.GetValues(tokenName).First();
                try
                {
                    var token = Token.Descriptografar(tokenCriptografado);
                    var usuarioIdValido = new AplicacaoUsuario().UsuarioIdValido(token.UsuarioId);
                    var ip = request.GetClientIP();
                    var ipValido = token.Ip.Equals(ip);

                    if (!usuarioIdValido || !ipValido)
                    {
                        var reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Identidade apresentada não é válida!");
                        return Task.FromResult(reply);
                    }
                }
                catch (Exception ex)
                {
                    var reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token usado não é válido!");
                    return Task.FromResult(reply);
                }
            }
            else
            {
                var reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Esta requisição exige um token de autorização!");
                return Task.FromResult(reply);
            }

            //return base.SendAsync(request, cancellationToken);


            var request2 = new RequestEnvelope(request);

            if (!request2.IsCorsPreflight)
                return base.SendAsync(request, cancellationToken);


            request.Headers.Add("Access-Control-Allow-Origin", "*");
            const string supportedMethods = "POST, PUT, GET, DELETE, X-Token";
            request.Headers.Add("Access-Control-Allow-Methods", supportedMethods);
            return base.SendAsync(request, cancellationToken); 

        }

        struct RequestEnvelope
        {
            private readonly HttpRequestMessage _request;
            public RequestEnvelope(HttpRequestMessage request) :
                this()
            {
                _request = request;
            }

            public bool IsCorsPreflight
            {
                get
                {
                    return (
                        _request.Headers.Contains("Origin") &&
                        _request.Method == HttpMethod.Options
                        );
                }
            }
        }
    }
}