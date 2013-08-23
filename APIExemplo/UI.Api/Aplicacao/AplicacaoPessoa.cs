using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using UI.Api.Models;
using UI.Api.Repositorio;

namespace UI.Api.Aplicacao
{
    public class AplicacaoPessoa
    {
        private readonly RepositorioMongo<Pessoa> repositorio;

        public AplicacaoPessoa()
        {
            repositorio = new RepositorioMongo<Pessoa>();
        }

        public IEnumerable<Pessoa> Buscar()
        {
            var retorno = repositorio.Collection.AsQueryable().OrderBy(x => x.Nome);
            return retorno;
        }

        public Pessoa BuscarPorId(string id)
        {
            return repositorio.Collection.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public void Excluir(string id)
        {
            repositorio.Collection.Remove(Query.EQ("_id", id));
        }

        public virtual void Salvar(Pessoa entidade)
        {
            repositorio.Collection.Save(entidade);
        }

    }
}