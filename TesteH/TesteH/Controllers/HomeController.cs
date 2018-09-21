using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteH.Models;

namespace TesteH.Controllers
{
    public class HomeController : Controller
    {

        private log4net.ILog logger;


        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CadUsuarios()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadUsuarios(Usuario _usuario)
        {
            logger = log4net.LogManager.GetLogger("LogInFile");
            if (ModelState.IsValid)
            {
                using (CadastroUsuariosEntities dc = new CadastroUsuariosEntities())
                {
                    if (!UsuarioDAL.VerificarEmail(_usuario.Email))
                    { 
                        dc.Usuarios.Add(_usuario);
                        dc.SaveChanges();
                        ModelState.Clear();
                        _usuario = null;
                        logger.Info("Usuario cadastro com sucesso.");
                        ViewBag.Message = "Usuário registrado com sucesso.";
                    }
                    else
                    {
                        logger.Info("Cadastro de usuario não efetuado, cartão já cadastrado.");
                        ViewBag.Message = "E-mail já cadastrado.";
                    }
                }
            }
            return View(_usuario);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario _usuario)
        {
            logger = log4net.LogManager.GetLogger("LogInFile");
            if (ModelState.IsValid)
            {
                using (CadastroUsuariosEntities dc = new CadastroUsuariosEntities())
                {
                    var v = dc.Usuarios.Where(a => a.Nome.Equals(_usuario.Nome) && a.Senha.Equals(_usuario.Senha)).FirstOrDefault();
                    if (v != null)
                    {
                        logger.Info("Login com sucesso.");
                        Session["usuarioLogadoID"] = v.IdUsuario.ToString();
                        Session["nomeLogado"] = v.Nome.ToString();
                        return RedirectToAction("index");
                    }
                }
            }
            logger.Info("Login não efetuado.");
            return View(_usuario);
        }

        public ActionResult CadCartoes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadCartoes(Carto _cartao)
        {
            if (ModelState.IsValid)
            {
                using (CadastroCartaoEntities dc = new CadastroCartaoEntities())
                {

                    logger = log4net.LogManager.GetLogger("LogInFile");

                    if (!UsuarioDAL.VerificarCartao(_cartao.NrCartao))
                    {
                        dc.Cartoes.Add(_cartao);
                        dc.SaveChanges();
                        ModelState.Clear();
                        _cartao = null;
                        ViewBag.Message = "Cartão registrado com sucesso.";
                        logger.Info("Cadastro de cartão com sucesso.");
                    }
                    else
                    {
                        ViewBag.Message = "Cartão já cadastrado.";
                        logger.Info("Cadastro de cartão não efetuado, cartão já cadastrado.");
                    }
                }
            }
            return View(_cartao);
        }

        static List<Carto> cartoes = new List<Carto>();

        public ActionResult ConCartoes()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConCartoes(decimal Cartao)
        {
            Carto c = new Carto();
            foreach (Carto cr in cartoes)
            {
                if (cr.NrCartao == Cartao)
                {
                    c.NrCartao = cr.NrCartao;
                    c.NrLote = cr.NrLote;
                }
            }

            return View(c);
        }

    }
}