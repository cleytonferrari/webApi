using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Api.Models;

namespace UI.Api.Aplicacao
{
    public class AplicacaoUsuario
    {
        
        public List<Usuario> UsuariosFake { get; private set; }

        public AplicacaoUsuario()
        {
            UsuariosFake = new List<Usuario>
                               {
                                   new Usuario{Id = "1", Login = "cleyton", Senha = "171099"},
                                   new Usuario{Id = "2", Login = "anderson", Senha = "123"}
                               };
        }


        public Usuario UsuarioValido(Usuario user)
        {
            return UsuariosFake.FirstOrDefault(x => x.Login == user.Login && x.Senha == user.Senha);
        }

        public bool UsuarioIdValido(string userId)
        {
            return UsuariosFake.Any(x => x.Id == userId);
        }
    }
}