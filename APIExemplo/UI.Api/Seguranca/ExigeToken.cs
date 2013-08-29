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

            return base.SendAsync(request, cancellationToken);
        }
    }
}