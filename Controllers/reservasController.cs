using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public reservasController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<reservas> listadoreservas = (from e in _equipos_context.reservas
                                          select e).ToList();
            if (listadoreservas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoreservas);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            reservas? R1 = (from e in _equipos_context.reservas
                               where e.reserva_id == id
                               select e).FirstOrDefault();
            if (R1 == null)
            {
                return NotFound();
            }
            return Ok(R1);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        //buscar por nombre de usuario; usar join
        public IActionResult Buscador(string filtro)
        {
            reservas? r1 = (from e in _equipos_context.reservas
            //                   where e.usuario_id.Contains(filtro)
                               select e).FirstOrDefault();
            if (r1 == null)
            {
                return NotFound();
            }
            return Ok(r1);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] reservas reservaModificada)
        {
            reservas? r1 = (from e in _equipos_context.reservas
                               where e.reserva_id == id
                               select e).FirstOrDefault();
            if (r1 == null)
            {
                return NotFound();
            }
            r1.usuario_id = reservaModificada.usuario_id;
            r1.reserva_id = reservaModificada.reserva_id;
            r1.estado_reserva_id = reservaModificada.estado_reserva_id;
            r1.tiempo_reserva = reservaModificada.tiempo_reserva;
            r1.equipo_id = reservaModificada.equipo_id;
            r1.fecha_retorno = reservaModificada.fecha_retorno;
            r1.hora_retorno = reservaModificada.hora_retorno;
            r1.hora_salida = reservaModificada.hora_salida;
            r1.fecha_salida = reservaModificada.fecha_salida;
            
            _equipos_context.Entry(r1).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(r1);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            reservas? r1 = (from e in _equipos_context.marcas
                               where e.id_marcas == id
                               select e).FirstOrDefault();
            if (r1 == null)
            {
                return NotFound();
            }
            _equipos_context.marcas.Attach(r1);
            _equipos_context.Remove(r1);
            _equipos_context.SaveChanges();
            return Ok(r1);
        }
    }
}
