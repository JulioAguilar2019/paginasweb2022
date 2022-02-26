using equiposWebAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace equiposWebAPi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {

        private readonly prestamosContext _contexto;

        public equiposController(prestamosContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get()
        {
            IEnumerable<equipos> equiposList = (from e in _contexto.equipos
                                                select e);

            if (equiposList.Count()>0)
            {
                return Ok(equiposList);
            }
            return NotFound(); 
        }    

    }
}
