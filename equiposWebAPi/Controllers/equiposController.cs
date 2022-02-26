using equiposWebAPi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace equiposWebAPi.Controllers
{
        //[Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {

        //configurar mi variable de conexion al contexto db
        private readonly prestamosContext _contexto;

        public EquiposController(prestamosContext miContexto)
        {
            _contexto = miContexto;
        }

        [HttpGet]
        [Route("api/equipos")]
        public IActionResult Get()
        {
            IEnumerable<equipos> equiposList = (from e in _contexto.equipos
                                                select e);

            if (equiposList.Count() > 0)
            {
                return Ok(equiposList);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/equipos{idUsuarios}")]

        public IActionResult Get(int idUsuario)
        {
            equipos equipo = (from e in _contexto.equipos
                              where e.id_equipos == idUsuario
                              select e
                                                ).FirstOrDefault();

            if (equipo != null)
            {
                return Ok(equipo);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/equipo")]
        public IActionResult guardarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                _contexto.equipos.Add(equipoNuevo);
                _contexto.SaveChanges();
                return Ok(equipoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/equipos")]
        public IActionResult updateEquipo([FromBody] equipos equipoAModificar)
        {
            //Para actualizar un registro, se obtiene el regitro original de la base de datos
            equipos equipoExiste = (from e in _contexto.equipos
                                    where e.id_equipos == equipoAModificar.id_equipos
                                    select e).FirstOrDefault();

            if (equipoExiste is null)
            {
                //Si no existe el registro de retorna un NO ENCONTRADO
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos a modificar
            equipoExiste.nombre = equipoAModificar.nombre;
            equipoExiste.descripcion = equipoAModificar.descripcion;

            //Se envia el objeto a la base de datos
            _contexto.Entry(equipoExiste).State = EntityState.Modified;
            _contexto.SaveChanges();


            return Ok(equipoExiste);

        }

    }
}
