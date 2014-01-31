using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UI.Api.Aplicacao;
using UI.Api.Helpers;
using UI.Api.Models;
using UI.Api.Seguranca;

namespace UI.Api.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public Status Autenticar(Usuario user)
        {
            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Por favor informe as credencias de acesso.") });
            var usuario = new AplicacaoUsuario().UsuarioValido(user);
            if (usuario != null)
            {
                Token token = new Token(usuario.Id, Request.GetClientIP());
                return new Status { Successeded = true, Token = token.Criptografar(), Message = "Login realizado com sucesso." };
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent("Usuario ou senha invalidos.") });
            }
        }
    }
}
