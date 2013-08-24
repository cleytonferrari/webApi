using System;

namespace UI.Api.Seguranca
{
    public class Token
    {
        public Token(string usuarioId, string ip)
        {
            UsuarioId = usuarioId;
            Ip = ip;
        }

        public string UsuarioId { get; private set; }
        public string Ip { get; private set; }

        public string Criptografar()
        {
            var cripo = new Criptografica();
            return cripo.Criptografar(this.ToString());
        }

        public override string ToString()
        {
            return String.Format("{0};{1}", this.UsuarioId, this.Ip);
        }

        public static Token Descriptografar(string tokenCriptografado)
        {
            var cripo = new Criptografica();
            var descriptografado = cripo.Descriptografar(tokenCriptografado);
            var token = descriptografado.Split(';');

            return new Token(token[0], token[1]);
        }
    }
}