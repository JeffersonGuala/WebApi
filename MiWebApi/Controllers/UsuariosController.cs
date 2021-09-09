using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConectarDatos;
// Agregaremos en este  controlador todos los metodos comoo GET, POST, DELETE, PUT para realizar las difeentes  consultas
//de todos los registros de la tabla en la base de datos.
namespace MiWebApi.Controllers
{
    public class UsuariosController : ApiController
    {
        private UsuariosEntities dbContext = new UsuariosEntities();
        // Visualiza todos los registros (api/usuarios)
        [HttpGet]
        public IEnumerable<usuario> Get()
        {
            using (UsuariosEntities usuariosentities = new UsuariosEntities())
            {
                return usuariosentities.usuario.ToList();
            }

        }
        // Visualiza solo un reistro (api/usuarios/I)
        [HttpGet]
        public usuario Get(int id)
        {
            using (UsuariosEntities usuariosentities = new UsuariosEntities())
            {
                return usuariosentities.usuario.FirstOrDefault(e => e.id == id);
            }

        }
        // Graba nuevos registros  en la base de datos usuarios
        [HttpPost]
        public IHttpActionResult AgregaUsuario([FromBody] usuario usu)
        {
            if (ModelState.IsValid)
            {
                dbContext.usuario.Add(usu);
                dbContext.SaveChanges();
                return Ok(usu);
            }
            else
            {
                return BadRequest();

            }
        }
        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, [FromBody] usuario usu)
        {
            if (ModelState.IsValid)
            {
                var UsuarioExiste = dbContext.usuario.Count(c => c.id == id) > 0;
                if (UsuarioExiste)
                {
                    dbContext.Entry(usu).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return Ok();

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();

            }


        }
        // Borra un registro (api/usuarios/i)
        [HttpDelete]
        public IHttpActionResult EliminarUsuario(int id)
        {
            var usu = dbContext.usuario.Find(id);
            if (usu != null)
            {
                dbContext.usuario.Remove(usu);
                dbContext.SaveChanges();
                return Ok(usu);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
