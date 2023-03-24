using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estado_reservaController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public estado_reservaController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<estados_reserva> listadoReservas = (from e in _equipos_context.estados_reserva
                                                    select e).ToList();
            if (listadoReservas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoReservas);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            estados_reserva? estados = (from e in _equipos_context.estados_reserva
                                       where e.estado_res_id == id
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
            estados_reserva? estados = (from e in _equipos_context.estados_reserva
                                       where e.estado.Contains(filtro)
                                       select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            return Ok(estados);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] estados_reserva estadoModificado)
        {
            estados_reserva? estados = (from e in _equipos_context.estados_reserva
                                       where e.estado_res_id == id
                                       select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            estados.estado = estadoModificado.estado;
            estados.estado_res_id = estadoModificado.estado_res_id;
            _equipos_context.Entry(estados).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(estados);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            estados_reserva? estados = (from e in _equipos_context.estados_reserva
                                       where e.estado_res_id == id
                                       select e).FirstOrDefault();
            if (estados == null)
            {
                return NotFound();
            }
            _equipos_context.estados_reserva.Attach(estados);
            _equipos_context.Remove(estados);
            _equipos_context.SaveChanges();
            return Ok(estados);
        }
    }
}
