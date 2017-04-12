using Loja.Business.Interfaces;
using Loja.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Loja.Alnath.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private IPessoaBO _clienteBO;

        public ClienteController(IPessoaBO clienteBO)
        {
            this._clienteBO = clienteBO;
        }

        /// <summary>
        /// Add a new person as customer
        /// </summary>
        /// <param name="value">The person</param>
        /// <returns></returns>
        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarCliente([FromBody]ClienteDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = this._clienteBO.AddCliente(value);

                    if (cliente != null)
                    {
                        return this.Ok(cliente);
                    }
                    else
                    {
                        return this.BadRequest("O E-mail já está sendo usado.");
                    }
                    
                }

                this.ModelState.AddModelError("Error", "Objeto inválido!");
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("alterar")]
        public IHttpActionResult AlterarCliente([FromBody] ClienteDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._clienteBO.AlterarCliente(value);
                    return this.Ok("Cliente alterado.");
                }

                this.ModelState.AddModelError("Error", "Objeto inválido!");
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult ObterClientes()
        {
            try
            {
                return this.Ok(this._clienteBO.ObterClientes());
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obterporemail")]
        public IHttpActionResult ObterClientesPorEmail(string Email)
        {
            try
            {
                return this.Ok(this._clienteBO.ObterClienteEmail(Email));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }            
        }

        [HttpDelete]
        [Route("deletar")]
        public IHttpActionResult DeletarCliente(int idCliente)
        {
            try
            {
                this._clienteBO.DeletarCliente(idCliente);
                return this.Ok("Cliente deletado.");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}