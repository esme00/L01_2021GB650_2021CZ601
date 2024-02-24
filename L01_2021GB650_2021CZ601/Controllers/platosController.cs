using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021GB650_2021CZ601.Models;
namespace L01_2021GB650_2021CZ601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        
        private readonly platosContext _platosContexto;

        public platosController(platosContext platosContexto)
        {
            _platosContexto = platosContexto;
        }

        ///sumary
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// summary
        /// <return></return>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoplatos = (from p in _platosContexto.platos
                                           select p).ToList();

            if (listadoplatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoplatos);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
           platos? platos = (from p in _platosContexto.platos
                               where p.platoId == id
                               select p).FirstOrDefault();
            if (platos == null)
            {
                return NotFound();
            }
            return Ok(platos);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            platos? platos = (from p in _platosContexto.platos
                               where p.nombrePlato.Contains(filtro)
                               select p).FirstOrDefault();
            if (platos == null)
            {
                return NotFound();
            }
            return Ok(platos);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardare([FromBody] platos platos)
        {
            try
            {
                _platosContexto.platos.Add(platos);
                _platosContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult Actualizare(int id, [FromBody] platos platosModificar)
        {
            platos? platosActual = (from p in _platosContexto.platos
                                     where p.platoId == id
                                     select p).FirstOrDefault();

            if (platosActual == null)
            {
                return NotFound();
            }

            platosActual.nombrePlato = platosModificar.nombrePlato;
            platosActual.precio = platosModificar.precio;


            _platosContexto.Entry(platosActual).State = EntityState.Modified;
            _platosContexto.SaveChanges();

            return Ok(platosModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarE(int id)
        {
            platos? plato = (from p in _platosContexto.platos
                               where p.platoId == id
                               select p).FirstOrDefault();

            if (plato == null)
                return NotFound();

            _platosContexto.platos.Attach(plato);
            _platosContexto.platos.Remove(plato);
            _platosContexto.SaveChanges();

            return Ok(plato);
        }
    }
}
