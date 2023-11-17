using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tarefas.Data;
using Tarefas.Models;
using Tarefas.ViewModel;

namespace Tarefas.Controller
{
    [ApiController]
    [Route(template: "v1")]
    public class TipoTarefaController : ControllerBase
    {
        /// <summary>
        /// Lista todas os tipos de tarefas existentes
        /// </summary>
        /// <returns>ToListAsync permite retorno dos dados em uma lista</returns>
        [HttpGet]
        [Route(template: "ListarTipos")]
        public async Task<IActionResult> ListarTipos([FromServices] ClassDbContext ObjContext)
        {
            var mTipo = await ObjContext.objTbTipo.ToListAsync();
            return Ok(mTipo);
        }

        /// <summary>
        /// Selecionar Tipo pelo Id 
        /// (chave primaria na tabela tipo)
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="id">Id(identificação) exclusiva do tipo de tarefa</param>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "SelecionarTipoTarefa/{id}")]
        public async Task<IActionResult> SelecionarTipo(
            [FromServices] ClassDbContext ObjContext,
            [FromRoute] int id)
        {
            var mTipo = await ObjContext.objTbTipo.FirstOrDefaultAsync(x => x.IdTipo == id);
            return mTipo == null ? NotFound() : Ok(mTipo);
        }

        /// <summary>
        /// Criar novo tipo de tarefa
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="ObjCriar">Objeto para definir a descrição do novo tipo de tarefa</param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "CriarTipo")]
        public async Task<IActionResult> CriarTipo(
            [FromServices] ClassDbContext ObjContext,
            [FromBody] vmTipo ObjCriar)
        {
            if (!ModelState.IsValid) { return BadRequest(); }   //Certifica-se que o campo Required é valido

            MdTipo mTipo = new()
            {
                TipoTarefa = ObjCriar.DescricaoTipo,
            };

            try
            {
                await ObjContext.objTbTipo.AddAsync(mTipo);
                await ObjContext.SaveChangesAsync();
                return Created("v1/ListarTipos", mTipo);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Editar descrição do Tipo de tarefa
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="objEditar">Permite modificar a descrição do tipo</param>
        /// <param name="id">Identificação do Tipo que será modificada</param>
        /// <returns></returns>
        [HttpPut]
        [Route(template: "EditarTipo/{id}")]
        public async Task<IActionResult> EditarTipo(
            [FromServices] ClassDbContext ObjContext,
            [FromBody] vmTipo objEditar,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            //Instancia da model para preenchimento de dados
            MdTipo mTipo = await ObjContext.objTbTipo.FirstOrDefaultAsync(x => x.IdTipo == id);

            try
            {
                mTipo.TipoTarefa = objEditar.DescricaoTipo;

                ObjContext.objTbTipo.Update(mTipo);
                ObjContext.SaveChanges();
                return Ok(mTipo);
            }
            catch (Exception e)
            {
                return BadRequest(e) ;
            }
        }

        /// <summary>
        /// Permite excluir Tipos de Tarefas existentes
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="id">Identificação do Tipo de tarefa que será excluida</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(template: "ExcluirTipo/{id}")]
        public async Task<IActionResult> ExcluirTipo(
            [FromServices] ClassDbContext ObjContext,
            [FromRoute] int id
            )
        {
            var mTipo = await ObjContext.objTbTipo.FirstOrDefaultAsync(x => x.IdTipo == id);

            try
            {
                ObjContext.objTbTipo.Remove(mTipo);
                await ObjContext.SaveChangesAsync();
                return Ok("v1/ListarTipos");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
    }
}
