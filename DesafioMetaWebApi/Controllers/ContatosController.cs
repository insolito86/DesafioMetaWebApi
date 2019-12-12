using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DesafioMetaWebApi.Data;
using DesafioMetaWebApi.Models;

namespace DesafioMetaWebApi.Controllers
{
    public class ContatosController : Controller
    {
        private BaseContext db = new BaseContext();

        /// <summary>
        /// Retorna a View com links de exemplo de como comsumir a API
        /// </summary>
        /// <returns>View</returns>

        // GET: /Contatos
        // GET: /Contatos/Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retorna uma lista de registros de acordo com o informado nos parâmetros
        /// page e size. Se estes parâmetros não forem passados na consulta, os
        /// seguintes valores padrão serão utilizados: page = 0 e size = 10
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>

        // GET: /Contatos/Lista
        // GET: /Contatos/Lista?page=0&size=50
        [HttpGet]
        public ActionResult Lista(int page = 0, int size = 10)
        {
            List<Contatos> resultado = db.Contatos.OrderBy(c => c.ID).Skip(page * size).Take(size).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retorna um único objeto do tipo Contato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: /Contatos/Detalhes/1
        // GET: /Contatos/Detalhes?id=1
        [HttpGet]
        public ActionResult Detalhes(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Contatos resultado = db.Contatos.Find(id);
            if (resultado == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cria um novo objeto do tipo Contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>

        // POST: /Contatos/Criar?Nome=Delci&Canal=email&Valor=delci_procopio@hotmail.com&Obs=DesafioMetaWebAPI
        [HttpGet]
        public ActionResult Criar([Bind(Include = "Nome,Canal,Valor,Obs")] Contatos contato)
        {
            if (ModelState.IsValid)
            {
                db.Contatos.Add(contato);
                db.SaveChanges();
                return RedirectToAction("Lista");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Altera um objeto do tipo Contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>

        // GET: /Contatos/Editar?ID=5&Nome=Fulano&Canal=email&Valor=delci_procopio@hotmail.com&Obs=DesafioMetaWebAPI
        [HttpGet]
        public ActionResult Editar([Bind(Include = "ID,Nome,Canal,Valor,Obs")] Contatos contato)
        {
            if (ModelState.IsValid)
            {
                Contatos item = db.Contatos.Find(contato.ID);
                if (item == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                db.Contatos.Remove(item);
                db.Contatos.Add(contato);
                db.SaveChanges();
                return RedirectToAction("Lista");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Apaga um objeto do tipo Contato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: /Contatos/Deletar?id=11
        [HttpGet]
        public ActionResult Deletar(int id)
        {
            Contatos contato = db.Contatos.Find(id);
            if (contato == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            db.Contatos.Remove(contato);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }

        /// <summary>
        /// Libera recursos
        /// </summary>
        /// <param name="disposing"></param>

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
