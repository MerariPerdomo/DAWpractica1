using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public estados_equipoController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            var listadoCarreras = (from e in _equipos_context.estados_equipo
                                   join s in _equipos_context.equipos on e.id_estados_equipo equals s.tipo_equipo_id
                                              select new
                                              {
                                                  s.nombre,
                                                  e.descripcion,
                                                  e.estado,
                                                  s.vida_util,
                                                  s.costo
                                                  

                                              }).ToList();
            if (listadoCarreras.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoCarreras);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            estados_equipo? estados = (from e in _equipos_context.estados_equipo
                                 where e.id_estados_equipo == id
                                 select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            return Ok(estados);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        public IActionResult Buscador(string filtro)
        {
            estados_equipo? estados = (from e in _equipos_context.estados_equipo
                                 where e.descripcion.Contains(filtro)
                                 select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            return Ok(estados);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] estados_equipo estadoModificado)
        {
            estados_equipo? estados = (from e in _equipos_context.estados_equipo
                                 where e.id_estados_equipo == id
                                 select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            estados.descripcion = estadoModificado.descripcion;
            estados.estado = estadoModificado.estado;
            estados.id_estados_equipo = estadoModificado.id_estados_equipo;
            _equipos_context.Entry(estados).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(estados);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            estados_equipo? estados = (from e in _equipos_context.estados_equipo
                                 where e.id_estados_equipo == id
                                 select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            _equipos_context.estados_equipo.Attach(estados);
            _equipos_context.Remove(estados);
            _equipos_context.SaveChanges();
            return Ok(estados);
        }
    }
}
