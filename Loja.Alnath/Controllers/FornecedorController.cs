using Loja.Business.Interfaces;
using Loja.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

namespace Loja.Alnath.Controllers
{
    [RoutePrefix("api/fornecedor")]
    public class FornecedorController : ApiController
    {
        private IFornecedorBO _fornecedorBO;
        public FornecedorController(IFornecedorBO fornecedorBO)
        {
            this._fornecedorBO = fornecedorBO;
        }

        /// <summary>
        /// Add a new Fornecedor
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// Return one HttpActionResult with Ok or Badrequest, in case of Ok the return have 
        /// the new Fornecedor
        /// </returns>
        [HttpPost]
        [Route("adicionar")]
        public IHttpActionResult AdicionarFornecedor([FromBody] FornecedorDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fornecedor = this._fornecedorBO.AddFornecedor(value);

                    if (fornecedor != null)
                    {
                        return this.Ok(fornecedor);
                    }
                    else
                    {
                        return this.BadRequest("O E - mail já está sendo usado.");
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
        public IHttpActionResult AlterarFornecedor([FromBody]FornecedorDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._fornecedorBO.AlterarFornecedor(value);
                    return this.Ok("Fornecedor alterado.");
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
        public IHttpActionResult ObterFornecedores()
        {
            try
            {
                return this.Ok(this._fornecedorBO.ObterFornecedores());
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("listarporemail")]
        public IHttpActionResult ObterFornecedorPorEmail(string Email)
        {
            try
            {
                return this.Ok(this._fornecedorBO.ObterFornecedorEmail(Email));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletar")]
        public IHttpActionResult DeletarFornecedor(int idFornecedor)
        {
            try
            {
                this._fornecedorBO.DeletarFornecedor(idFornecedor);
                return this.Ok("Fornecedor deletado.");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
