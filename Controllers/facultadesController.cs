using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public facultadesController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<facultades> listadofacultades = (from e in _equipos_context.facultades
                                                     select e).ToList();
            if (listadofacultades.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadofacultades);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            facultades? facultades = (from e in _equipos_context.facultades
                                        where e.facultad_id == id
                                        select e).FirstOrDefault();
            if (facultades == null)
            {
                return NotFound();
            }
            return Ok(facultades);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        public IActionResult Buscador(string filtro)
        {
            facultades? facultades = (from e in _equipos_context.facultades
                                        where e.nombre_facultad.Contains(filtro)
                                        select e).FirstOrDefault();
            if (facultades == null)
            {
                return NotFound();
            }
            return Ok(facultades);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] facultades facultadModificada)
        {
            facultades? facultades = (from e in _equipos_context.facultades
                                        where e.facultad_id == id
                                        select e).FirstOrDefault();
            if (facultades == null)
            {
                return NotFound();
            }
            facultades.nombre_facultad = facultadModificada.nombre_facultad;
            facultades.facultad_id = facultadModificada.facultad_id;
            _equipos_context.Entry(facultades).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(facultades);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            facultades? facultades1 = (from e in _equipos_context.facultades
                                        where e.facultad_id == id
                                        select e).FirstOrDefault();
            if (facultades1 == null)
            {
                return NotFound();
            }
            _equipos_context.facultades.Attach(facultades1);
            _equipos_context.Remove(facultades1);
            _equipos_context.SaveChanges();
            return Ok(facultades1);
        }
    }
}
