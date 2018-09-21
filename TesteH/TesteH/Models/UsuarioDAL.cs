using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteH.Models;

namespace TesteH.Models
{
    public class UsuarioDAL
    {
        public static bool VerificarEmail(string email)
        {
            using (CadastroUsuariosEntities dc = new CadastroUsuariosEntities())
            {
                var existeEmail = (from u in dc.Usuarios
                                   where u.Email == email
                                   select u).FirstOrDefault();

                if (existeEmail != null)
                    return true;
                else
                    return false;
            }
        }

        public static bool VerificarCartao(decimal cartao)
        {
            using (CadastroCartaoEntities dc = new CadastroCartaoEntities())
            {
                var existeCartao = (from c in dc.Cartoes
                                   where c.NrCartao == cartao
                                   select c).FirstOrDefault();

                if (existeCartao != null)
                    return true;
                else
                    return false;
            }
        }
    }
}