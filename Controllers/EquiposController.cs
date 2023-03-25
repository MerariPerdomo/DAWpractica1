using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAWpractica1.Models;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public EquiposController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            var listadoEquipo =(from e in _equipos_context.equipos
                                join m in _equipos_context.marcas on e.marca_id equals m.id_marcas
                                          select new
                                          {
                                              eq= ("Nombre del equipo: "+ e.nombre),
                                              e.vida_util,
                                              e.costo,
                                              e.descripcion,
                                              mar= ("Marca: "+ m.nombre_marca)
                                          }).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            Equipos? equipos = (from e in _equipos_context.equipos
                                where e.id_equipos == id
                                select e).FirstOrDefault();
            if (equipos == null)
            {
                return NotFound();
            }
            return Ok(equipos);
        }
        [HttpGet]
        [Route("buscador/{filtro}")]
        public IActionResult Buscador(string filtro)
        {
            Equipos? equipo = (from e in _equipos_context.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] Equipos equipoModificar)
        {
            Equipos? equiposActual = (from e in _equipos_context.equipos
                                      where e.id_equipos == id
                                     select e).FirstOrDefault();
            if (equiposActual == null)
            {
                return NotFound();
            }
            equiposActual.nombre= equipoModificar.nombre;
            equiposActual.descripcion = equipoModificar.descripcion;
            equiposActual.marca_id = equipoModificar.marca_id;
            equiposActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equiposActual.anio_compra = equipoModificar.anio_compra;
            equiposActual.costo= equipoModificar.costo;

            _equipos_context.Entry(equiposActual).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(equiposActual);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            Equipos? equipos = (from e in _equipos_context.equipos
                                where e.id_equipos == id
                                select e).FirstOrDefault();
            if (equipos == null)
            {
                return NotFound();
            }
            _equipos_context.equipos.Attach(equipos);
            _equipos_context.Remove(equipos);
            _equipos_context.SaveChanges();
            return Ok(equipos);
        }
    }

}
