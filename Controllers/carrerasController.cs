using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carrerasController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public carrerasController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<carreras> listadoCarreras = (from e in _equipos_context.carreras
                                           select e).ToList();
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
            carreras? carrera = (from e in _equipos_context.carreras
                                where e.carrera_id == id
                                select e).FirstOrDefault();
            if (carrera == null)
            {
                return NotFound();
            }
            return Ok(carrera);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        public IActionResult Buscador(string filtro)
        {
            carreras? carrera = (from e in _equipos_context.carreras
                               where e.nombre_carrera.Contains(filtro)
                               select e).FirstOrDefault();
            if (carrera == null)
            {
                return NotFound();
            }
            return Ok(carrera);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] carreras carreraModificada)
        {
            carreras? carrera = (from e in _equipos_context.carreras
                                      where e.carrera_id == id
                                      select e).FirstOrDefault();
            if (carrera == null)
            {
                return NotFound();
            }
            carrera.nombre_carrera = carreraModificada.nombre_carrera;
            carrera.facultad_id = carreraModificada.facultad_id;
            carrera.carrera_id = carreraModificada.carrera_id;
            _equipos_context.Entry(carrera).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(carrera);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            carreras? carrera = (from e in _equipos_context.carreras
                                where e.carrera_id == id
                                select e).FirstOrDefault();
            if (carrera == null)
            {
                return NotFound();
            }
            _equipos_context.carreras.Attach(carrera);
            _equipos_context.Remove(carrera);
            _equipos_context.SaveChanges();
            return Ok(carrera);
        }
    }
}

