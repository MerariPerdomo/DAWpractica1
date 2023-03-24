using DAWpractica1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAWpractica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equipos_context _equipos_context;

        public marcasController(equipos_context equipos_context)
        {
            _equipos_context = equipos_context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult Get()
        {
            List<marcas> listadomarcas = (from e in _equipos_context.marcas
                                                  select e).ToList();
            if (listadomarcas.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadomarcas);
        }
        [HttpGet]
        [Route("ObtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            marcas? marcas1 = (from e in _equipos_context.marcas
                                      where e.id_marcas == id
                                      select e).FirstOrDefault();
            if (marcas1 == null)
            {
                return NotFound();
            }
            return Ok(marcas1);
        }
        [HttpGet]
        [Route("buscadorPorNombre/{filtro}")]
        public IActionResult Buscador(string filtro)
        {
            marcas? marcas1 = (from e in _equipos_context.marcas
                                      where e.nombre_marca.Contains(filtro)
                                      select e).FirstOrDefault();
            if (marcas1 == null)
            {
                return NotFound();
            }
            return Ok(marcas1);
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizar(int id, [FromBody] marcas marcaModificada)
        {
            marcas? marcas1 = (from e in _equipos_context.marcas
                                      where e.id_marcas == id
                                      select e).FirstOrDefault();
            if (marcas1 == null)
            {
                return NotFound();
            }
            marcas1.nombre_marca = marcaModificada.nombre_marca;
            marcas1.id_marcas = marcaModificada.id_marcas;
            marcas1.estados = marcaModificada.estados;
            _equipos_context.Entry(marcas1).State = EntityState.Modified;
            _equipos_context.SaveChanges();

            return Ok(marcas1);
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            marcas? marcas1 = (from e in _equipos_context.marcas
                                       where e.id_marcas == id
                                       select e).FirstOrDefault();
            if (marcas1 == null)
            {
                return NotFound();
            }
            _equipos_context.marcas.Attach(marcas1);
            _equipos_context.Remove(marcas1);
            _equipos_context.SaveChanges();
            return Ok(marcas1);
        }
    }
}
