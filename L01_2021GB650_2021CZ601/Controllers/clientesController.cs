using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2021GB650_2021CZ601.Models;

namespace L01_2021GB650_2021CZ601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly clientesContext _clientesContexto;

        public clientesController(clientesContext clientesContexto)
        {
            _clientesContexto = clientesContexto;
        }

        ///sumary
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// summary
        /// <return></return>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<clientes> listadoclientes = (from c in _clientesContexto.clientes
                                          select c).ToList();

            if (listadoclientes.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoclientes);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            clientes? clientes = (from c in _clientesContexto.clientes
                              where c.clienteId == id
                              select c).FirstOrDefault();
            if ( clientes == null)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            clientes? clientes = (from c in _clientesContexto.clientes
                              where c.direccion.Contains(filtro)
                              select c).FirstOrDefault();
            if (clientes == null)
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardare([FromBody] clientes clientes)
        {
            try
            {
                _clientesContexto.Add(clientes);
                _clientesContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult Actualizare(int id, [FromBody] clientes clientesModificar)
        {
            clientes? clientesActual = (from c in _clientesContexto.clientes
                                    where c.clienteId == id
                                    select c).FirstOrDefault();

            if (clientesActual == null)
            {
                return NotFound();
            }

            clientesActual.nombreCliente = clientesModificar.nombreCliente;
            clientesActual.direccion = clientesModificar.direccion;


            _clientesContexto.Entry(clientesActual).State = EntityState.Modified;
            _clientesContexto.SaveChanges();

            return Ok(clientesModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarE(int id)
        {
            clientes? clientes = (from p in _clientesContexto.clientes
                             where p.clienteId == id
                             select p).FirstOrDefault();

            if (clientes == null)
                return NotFound();

            _clientesContexto.clientes.Attach(clientes);
            _clientesContexto.clientes.Remove(clientes);
            _clientesContexto.SaveChanges();

            return Ok(clientes);
        }
    }

}
