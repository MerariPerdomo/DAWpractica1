using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipoController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public tipo_equipoController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            var listadot_e = (from e in _equipos_context.tipo_equipo
                              join eq in _equipos_context.equipos on e.id_tipo_equipo equals eq.tipo_equipo_id
                                              select new
                                              {
                                                  e.id_tipo_equipo,
                                                  e.descripcion,
                                                  eq.vida_util,
                                                  eq.nombre,
                                                  eq.costo
                                              }).ToList();
            if (listadot_e.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadot_e);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            tipo_equipo? te1 = (from e in _equipos_context.tipo_equipo
                            where e.id_tipo_equipo == id
                            select e).FirstOrDefault();
            if (te1 == null)
            {
                return NotFound();
            }
            return Ok(te1);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        //buscar por nombre de usuario; usar join
        public IActionResult Buscador(string filtro)
        {
            tipo_equipo? te = (from e in _equipos_context.tipo_equipo
                               where e.descripcion.Contains(filtro)
                            select e).FirstOrDefault();
            if (te == null)
            {
                return NotFound();
            }
            return Ok(te);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] tipo_equipo teModificadoe)
        {
            tipo_equipo? te = (from e in _equipos_context.tipo_equipo
                            where e.id_tipo_equipo == id
                            select e).FirstOrDefault();
            if (te == null)
            {
                return NotFound();
            }
            te.id_tipo_equipo = teModificadoe.id_tipo_equipo;
            te.descripcion = teModificadoe.descripcion;
            te.estado = teModificadoe.estado;
            _equipos_context.Entry(te).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(te);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            tipo_equipo? te = (from e in _equipos_context.tipo_equipo
                            where e.id_tipo_equipo == id
                            select e).FirstOrDefault();
            if (te == null)
            {
                return NotFound();
            }
            _equipos_context.tipo_equipo.Attach(te);
            _equipos_context.Remove(te);
            _equipos_context.SaveChanges();
            return Ok(te);
        }
    }
}
