using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DesafioMetaWebApi.Data;
using DesafioMetaWebApi.Models;

namespace DesafioMetaWebApi.Controllers
{
    [System.Web.Http.Route("Contatos")]
    public class ContatosController : ApiController
    {
        private BaseContext db = new BaseContext();

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
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Contatos/Lista")]
        public async Task<List<Contatos>> Lista(int Page = 0, int Size = 10)
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = Funcoes.ValidaUsuario(Request.Headers.GetValues("Authorization").FirstOrDefault(), Request.Headers.GetValues("HTTP_AUTHORIZATION").FirstOrDefault());
                if (usuarioValido) return await db.Contatos.OrderBy(c => c.ID).Skip(Page * Size).Take(Size).ToListAsync(); else throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unauthorized") throw new HttpException(401, "Unauthorized");
                throw new HttpException(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Retorna um único objeto do tipo Contato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: /Contatos/Detalhes?id=1
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Contatos/Detalhes")]
        public async Task<List<Contatos>> Detalhes(int ID = -1)
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = Funcoes.ValidaUsuario(Request.Headers.GetValues("Authorization").FirstOrDefault(), Request.Headers.GetValues("HTTP_AUTHORIZATION").FirstOrDefault());
                if (ID == -1 || db.Contatos.Find(ID) == null) throw new Exception("Bad Request");
                if (usuarioValido) return await db.Contatos.Where(co => co.ID == ID).ToListAsync(); else throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unauthorized") throw new HttpException(401, "Unauthorized");
                if (ex.Message == "Bad Request") throw new HttpException(400, "Bad Request");
                throw new HttpException(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Cria um novo objeto do tipo Contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>

        // POST: /Contatos/Criar?Nome=Ciclano&Canal=email&Valor=ciclano@hotmail.com&Obs=DesafioMetaWebAPI
        // POST: /Contatos/Criar?Nome=Beltrano&Canal=email&Valor=Beltrano@hotmail.com&Obs=DesafioMetaWebAPI
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Contatos/Criar")]
        public HttpStatusCodeResult Criar(string Nome = "", string Canal = "", string Valor = "", string Obs = "")
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = Funcoes.ValidaUsuario(Request.Headers.GetValues("Authorization").FirstOrDefault(), Request.Headers.GetValues("HTTP_AUTHORIZATION").FirstOrDefault());
                if (usuarioValido)
                {
                    if (Nome == "" || db.Contatos.FirstOrDefault(co => co.Nome == Nome) != null || !ModelState.IsValid) throw new Exception("Bad Request");
                    Contatos contato = new Contatos() { Nome = Nome, Canal = Canal, Valor = Valor, Obs = Obs };
                    db.Contatos.Add(contato);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK, "OK");
                }
                else throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unauthorized") return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Unauthorized");
                if (ex.Message == "Bad Request") return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        /// <summary>
        /// Altera um objeto do tipo Contato
        /// </summary>
        /// <param name="contato"></param>
        /// <returns></returns>

        // PUT: /Contatos/Editar?ID=5&Nome=Taurano&Canal=email&Valor=taurano@hotmail.com&Obs=DesafioMetaWebAPI
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("Contatos/Editar")]
        public HttpStatusCodeResult Editar(int ID = -1, string Nome = "", string Canal = "", string Valor = "", string Obs = "")
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = Funcoes.ValidaUsuario(Request.Headers.GetValues("Authorization").FirstOrDefault(), Request.Headers.GetValues("HTTP_AUTHORIZATION").FirstOrDefault());
                if (usuarioValido)
                {
                    Contatos contato = db.Contatos.Find(ID);
                    if (Nome == "" || ID == -1 || contato == null || !ModelState.IsValid) throw new Exception("Bad Request");
                    db.Contatos.Remove(contato);
                    contato = new Contatos() { Nome = Nome, Canal = Canal, Valor = Valor, Obs = Obs };
                    db.Contatos.Add(contato);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK, "OK");
                }
                else throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unauthorized") return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Unauthorized");
                if (ex.Message == "Bad Request") return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        /// <summary>
        /// Apaga um objeto do tipo Contato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // DELETE: /Contatos/Deletar?id=11
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("Contatos/Deletar")]
        public HttpStatusCodeResult Deletar(int ID = -1)
        {
            bool usuarioValido = false;
            try
            {
                usuarioValido = Funcoes.ValidaUsuario(Request.Headers.GetValues("Authorization").FirstOrDefault(), Request.Headers.GetValues("HTTP_AUTHORIZATION").FirstOrDefault());
                if (usuarioValido)
                {
                    Contatos contato = db.Contatos.Find(ID);
                    if (ID == -1 || contato == null || !ModelState.IsValid) throw new Exception("Bad Request");
                    db.Contatos.Remove(contato);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK, "OK");
                }
                else throw new Exception("Unauthorized");
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unauthorized") return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Unauthorized");
                if (ex.Message == "Bad Request") return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
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
