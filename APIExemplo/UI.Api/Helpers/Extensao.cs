using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace UI.Api.Helpers
{
    public static class Extensao
    {
        public static string GetClientIP(this HttpRequestMessage request)
        {
            return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
        }
    }
}