using L01_2021GB650_2021CZ601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021GB650_2021CZ601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly pedidosContext _pedidosContext;

        public pedidosController(pedidosContext pedidosContexto)
        {
            _pedidosContext = pedidosContexto;
        }

        /// <summary>
        /// Obtener todos los pedidods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllpedidos")]
        public IActionResult GetPedidos()
        {
            List<pedidos> listadoPedidos = _pedidosContext.pedidos.ToList();

            if (listadoPedidos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedidos);
        }

        /// <summary>
        /// Filtrar por cliente
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("FindIdCliente/{ID}")]

        public IActionResult GetIdCliente(int ID)
        {
            pedidos? ClienteName = (from e in _pedidosContext.pedidos
                                    where e.clienteId == ID
                                    select e).FirstOrDefault();

            if (ClienteName == null)
            {
                return NotFound();
            }
            return Ok(ClienteName);
        }

        /// <summary>
        /// Filtrar por cliente
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("FindIdMotorista/{ID}")]

        public IActionResult GetIdMotorista(int ID)
        {
            pedidos? ClienteName = (from e in _pedidosContext.pedidos
                                    where e.motoristaId == ID
                                    select e).FirstOrDefault();

            if (ClienteName == null)
            {
                return NotFound();
            }
            return Ok(ClienteName);
        }

        /// <summary>
        /// Añadir a tabla
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] pedidos pedidos)
        {
            try
            {
                _pedidosContext.pedidos.Add(pedidos);
                _pedidosContext.SaveChanges();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar pedidos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ModificarEquipo(int id, [FromBody] pedidos pedidosModificar)
        {
            pedidos? PedidosActual = (from e in _pedidosContext.pedidos
                                      where e.pedidoId == id
                                      select e).FirstOrDefault();

            if (PedidosActual == null)
            {
                return NotFound();
            }

            PedidosActual.motoristaId = pedidosModificar.motoristaId;
            PedidosActual.clienteId = pedidosModificar.clienteId;
            PedidosActual.platoId = pedidosModificar.platoId;
            PedidosActual.cantidad = pedidosModificar.cantidad;
            PedidosActual.precio = pedidosModificar.precio;

            _pedidosContext.Entry(PedidosActual).State = EntityState.Modified;
            _pedidosContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Eliminar/{id}")]

        public ActionResult Delete(int id)
        {
            pedidos? PedidosDelete = (from e in _pedidosContext.pedidos
                                      where e.pedidoId == id
                                      select e).FirstOrDefault();

            if (PedidosDelete == null)
            {
                return NotFound();
            }
            _pedidosContext.pedidos.Attach(PedidosDelete);
            _pedidosContext.pedidos.Remove(PedidosDelete);
            _pedidosContext.SaveChanges();
            return Ok(PedidosDelete);
        }


    }
}