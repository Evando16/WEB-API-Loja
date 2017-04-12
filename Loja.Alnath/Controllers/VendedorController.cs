using Loja.Business.Interfaces;
using Loja.DTO;
using System;
using System.Web.Http;

namespace Loja.Alnath.Controllers
{
    [RoutePrefix("api/vendedor")]
    public class VendedorController : ApiController
    {
        private IPessoaBO _vendedorBO;
        public VendedorController(IPessoaBO vendedorBO)
        {
            this._vendedorBO = vendedorBO;
        }

        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarVendedor([FromBody]VendedorDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vendedor = this._vendedorBO.AddVendedor(value);

                    if (vendedor != null)
                    {
                        return this.Ok(vendedor);
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
        public IHttpActionResult AlterarVendedor([FromBody] VendedorDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {                                        
                    this._vendedorBO.AlterarVendedor(value);
                    return this.Ok("Vendedor alterado.");
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
        public IHttpActionResult ObterVendedores()
        {
            try
            {
                return this.Ok(this._vendedorBO.ObterVendedores());
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obterporemail")]
        public IHttpActionResult ObterVendedorPorEmail(string email)
        {
            try
            {
                return this.Ok(this._vendedorBO.ObterVendedorEmail(email));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletar")]
        public IHttpActionResult DeletarVendedor(int idVendedor)
        {
            try
            {
                this._vendedorBO.DeletarVendedor(idVendedor);
                return this.Ok("Vendedor deletado.");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}
