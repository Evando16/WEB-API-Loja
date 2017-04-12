using Loja.Business.Interfaces;
using Loja.DTO;
using System;
using System.Web.Http;

namespace Loja.Alnath.Controllers
{
    [RoutePrefix("api/venda")]
    public class VendaController : ApiController
    {
        private IVendaBO _vendaBO;
        public VendaController(IVendaBO vendaBO)
        {
            this._vendaBO = vendaBO;
        }

        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarVenda(VendaDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _vendaBO.AddVenda(value);
                    return this.Ok("Venda cadastrada.");
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
        public IHttpActionResult AlterarVenda(VendaDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._vendaBO.AlterarVenda(value);
                    return this.Ok("Venda alterada");
                }
                this.ModelState.AddModelError("Error", "Objeto inválido!");
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletar")]
        public IHttpActionResult DeletarVenda(int idVenda)
        {
            try
            {
                this._vendaBO.DeletarVenda(idVenda);

                return this.Ok("Venda deletada.");

            }
            catch (Exception ex)
            {

                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult ObterVendas()
        {
            try
            {
                return this.Ok(this._vendaBO.ObterVendas());
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarporvendedor")]
        public IHttpActionResult ObterVendasPorVendedor(int idVendedor)
        {
            try
            {
                return this.Ok(this._vendaBO.ObterVendasDoVendedor(idVendedor));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarporcliente")]
        public IHttpActionResult ObterVendasPorCliente(int idCliente)
        {
            try
            {
                return this.Ok(this._vendaBO.ObterVendasDoCliente(idCliente));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarporid")]
        public IHttpActionResult ObterVendasPorId(int idvenda)
        {
            try
            {
                return this.Ok(this._vendaBO.ObterVendaPorID(idvenda));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }


        //Item venda

        [HttpPost]
        [Route("adicionaritemvenda")]
        public IHttpActionResult AdicionarItemVenda([FromBody] ItemVendaDTO value)
        {
            try
            {
                return this.Ok(this._vendaBO.AddItemVenda(value));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletaritemvenda")]
        public IHttpActionResult DeletarItemVenda([FromBody] ItemVendaDTO value)
        {
            try
            {
                this._vendaBO.DeletarItemVenda(value);
                return this.Ok("Item deletado.");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

    }
}
