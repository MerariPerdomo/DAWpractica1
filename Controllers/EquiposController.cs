using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAWpractica1.Models;

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
            List<Equipos> listadoEquipo =(from e in _equipos_context.equipos
                                          select e).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
    }

}
