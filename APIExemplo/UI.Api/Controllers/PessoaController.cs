using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Providers.Entities;
using UI.Api.Aplicacao;
using UI.Api.Models;


namespace UI.Api.Controllers
{
    public class PessoaController : ApiController
    {

        private readonly AplicacaoPessoa pessoaApp;

        public PessoaController()
        {
            pessoaApp = new AplicacaoPessoa();
        }

        
        public IEnumerable<Pessoa> Get()
        {
            return pessoaApp.Buscar();
        }

        // POST api/pessoa
        public HttpResponseMessage Post(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            pessoaApp.Salvar(pessoa);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, pessoa);
            return response;
        }

        // PUT api/pessoa/5
        public HttpResponseMessage Put(string id, Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != pessoa.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            //Alterar
            var pessoaBanco = pessoaApp.BuscarPorId(id);
            if (pessoaBanco == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            pessoaApp.Salvar(pessoa);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/pessoa/5
        public HttpResponseMessage Delete(string id)
        {
            var pessoaBanco = pessoaApp.BuscarPorId(id);

            if (pessoaBanco == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            pessoaApp.Excluir(id);

            return Request.CreateResponse(HttpStatusCode.OK, pessoaBanco);
        }
    }
}
