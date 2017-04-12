using Loja.Business.Interfaces;
using Loja.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Loja.Alnath.Controllers
{
    [RoutePrefix("api/estoque")]
    public class EstoqueController : ApiController
    {
        private IEstoqueBO _estoqueBO;
        public EstoqueController(IEstoqueBO estoqueBO)
        {
            this._estoqueBO = estoqueBO;
        }

        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarEstoque([FromBody] EstoqueDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return this.Ok(this._estoqueBO.AddEstoque(value));
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
        [Route("obterporid")]
        public IHttpActionResult ObterEstoquePorId(int idEstoque)
        {
            try
            {
                return this.Ok(this._estoqueBO.ObterEstoqueId(idEstoque));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult ObterEstoque(string nomeProduto = null)
        {
            try
            {
                return this.Ok(this._estoqueBO.ObterEstoqueNomeProduto(nomeProduto));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("alterar")]
        public IHttpActionResult AlterarEstoque([FromBody] EstoqueDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._estoqueBO.AlterarEstoque(value);
                    return this.Ok("Estoque alterado.");
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
        public IHttpActionResult DeletarEstoque( int idEstoque)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._estoqueBO.DeletarEstoque(idEstoque);
                    return this.Ok("Estoque deletado.");
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
        [Route("incrementar")]
        public IHttpActionResult IncrementarEstoque([FromBody] EstoqueDTO value)
        {
            if (value.Quantidade > 0)
            {
                try
                {
                    return this.Ok(this._estoqueBO.IncrementarEstoque(value));
                }
                catch (Exception ex)
                {
                    return this.BadRequest(ex.Message);
                }
            }
            else
            {
                return this.BadRequest("Quantidade não pode ser menor ou igual a zero.");
            }
        }

        [HttpPut]
        [Route("decrementar")]
        public IHttpActionResult DecrementarEstoque([FromBody] EstoqueDTO value)
        {
            if (value.Quantidade > 0)
            {
                try
                {
                    return this.Ok(this._estoqueBO.DecrementarEstoque(value));
                }
                catch (Exception ex)
                {
                    return this.BadRequest(ex.Message);
                } 
            }
            else
            {
                return this.BadRequest("Quantidade não pode ser menor ou igual a zero.");
            }
        }

    }
}
