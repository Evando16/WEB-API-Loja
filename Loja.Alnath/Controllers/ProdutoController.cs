using Loja.Business.Interfaces;
using Loja.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Loja.Alnath.Controllers
{
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        private IProdutoBO _produtoBO;

        public ProdutoController(IProdutoBO produtoBO)
        {
            this._produtoBO = produtoBO;
        }
        
        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarProduto([FromBody] ProdutoDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var produto = this._produtoBO.AddProduto(value);
                    if (produto != null)
                    {
                        return this.Ok(produto);
                    }
                    else
                    {
                        return this.BadRequest("Produto existente para o mesmo fornecedor");
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
        public IHttpActionResult AlterarProduto(ProdutoDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._produtoBO.AlterarProduto(value);
                    return this.Ok("Produto alterado.");
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
        public IHttpActionResult ObterProdutos()
        {
            try
            {
                return this.Ok(this._produtoBO.ObterProduto());
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);                
            }
        }

        [HttpGet]
        [Route("listarpornome")]
        public IHttpActionResult ObterProdutosPorNome(string Nome)
        {
            try
            {
                return this.Ok(this._produtoBO.ObterProduto(Nome));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("obterporid")]
        public IHttpActionResult ObterProdutosPorId(int idProduto)
        {
            try
            {
                return this.Ok(this._produtoBO.ObterProdutoID(idProduto));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletar")]
        public IHttpActionResult DeletarProduto(int idProduto)
        {
            try
            {
                this._produtoBO.DeletarProduto(idProduto);
                return this.Ok();                
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
