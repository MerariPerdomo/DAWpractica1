using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public usuariosController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsers = (from e in _equipos_context.usuarios
                                            select e).ToList();
            if (listadoUsers.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsers);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            usuarios? us = (from e in _equipos_context.usuarios
                                where e.usuario_id == id
                                select e).FirstOrDefault();
            if (us == null)
            {
                return NotFound();
            }
            return Ok(us);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        //buscar por nombre de usuario; usar join
        public IActionResult Buscador(string filtro)
        {
            usuarios? us = (from e in _equipos_context.usuarios
                               where e.nombre.Contains(filtro)
                               select e).FirstOrDefault();
            if (us == null)
            {
                return NotFound();
            }
            return Ok(us);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] usuarios UsModificado)
        {
            usuarios? us = (from e in _equipos_context.usuarios
                               where e.usuario_id == id
                               select e).FirstOrDefault();
            if (us == null)
            {
                return NotFound();
            }
            us.usuario_id = UsModificado.usuario_id;
            us.nombre = UsModificado.nombre;
            us.carrera_id = UsModificado.carrera_id;
            us.carnet = UsModificado.carnet;
            us.tipo = UsModificado.tipo;
            us.documento = UsModificado.documento;
            _equipos_context.Entry(us).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(us);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            usuarios? us = (from e in _equipos_context.usuarios
                               where e.usuario_id == id
                               select e).FirstOrDefault();
            if (us == null)
            {
                return NotFound();
            }
            _equipos_context.usuarios.Attach(us);
            _equipos_context.Remove(us);
            _equipos_context.SaveChanges();
            return Ok(us);
        }
    }
}
