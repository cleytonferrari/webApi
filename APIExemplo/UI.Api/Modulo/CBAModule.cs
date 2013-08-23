using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace UI.Api.Modulo
{
    public class CBAModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += context_AuthenticateRequest;
            context.EndRequest += context_EndRequest;
        }

        private void context_EndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
                response.Headers.Add("WWW-Authenticate", "Basic realm=\"Minha API\"");
        }

        private void context_AuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var header = request.Headers["Authorization"];
            if (header != null)
            {
                var parsedValued = AuthenticationHeaderValue.Parse(header);
                if (parsedValued.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    parsedValued.Parameter != null)
                {
                    Authenticate(parsedValued.Parameter);
                }
            }
        }

        private bool Authenticate(string credentialValues)
        {
            bool isValid = false;
            try
            {
                var credentials = Encoding
                    .GetEncoding("iso-8859-1")
                    .GetString(Convert.FromBase64String(credentialValues));
                var values = credentials.Split(':');
                isValid = CheckUser(userName: values[0], password: values[1]);
                if(isValid)
                    SetPrincipal(new GenericPrincipal(new GenericIdentity(values[0]),null));
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        private bool CheckUser(string userName, string password)
        {
            //aqui tu valida o usuario recebido
            return userName == "cleyton" && password == "171099";
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;
        }
    }
}