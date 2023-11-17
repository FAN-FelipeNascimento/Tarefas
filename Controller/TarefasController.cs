using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Data;
using Tarefas.ViewModel;
using Tarefas.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Tarefas.Controller
{
    /// <summary>
    /// Classe de controle das requisições para envio a View's
    /// Objetos que serão usados nos metodos
    /// * ObjContext é a instancia da classe de contexto
    /// * objTbTarefas é a minha representação da tabela, via DbSet
    /// * AsNoTracking desabilita rastreamento do registro que estamos trabalhando
    /// obs: essa funcionalidade não será usada em metodos de NoQuery, pois é necessário permitir rastrear o envio dos dados 
    /// para persistencia no banco.
    /// </summary>
    [ApiController]
    [Route(template: "v1")]  //controla versão e mantem integridade com a View
    public class TarefasController : ControllerBase
    {
        /// <summary>
        /// Lista todas as tarefas existentes na base
        /// </summary>
        /// <returns>ToListAsync permite retorno dos dados em uma lista</returns>
        [HttpGet]
        [Route(template: "ListarTarefas")] //define a url com a especificação do versionamento - ex: vi/ListaTarefas
        public async Task<IActionResult> TodasAsTarefas([FromServices] ClassDbContext ObjContext)
        {
            var varTarefas = await ObjContext.objTbTarefas.AsNoTracking().ToListAsync();
            return Ok(varTarefas);
        }

        /// <summary>
        /// Exibe Tarefa selecionado pelo Id 
        /// (chave primaria na tabela de tarefas)
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="id">Id(identificação) exclusiva da tarefa a ser exibida</param>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "SelecionarTarefa/{id}")]
        public async Task<IActionResult> SelecionarTarefa(
            [FromServices] ClassDbContext ObjContext,
            [FromRoute] int id)
        {
            var varTarefa = await ObjContext.objTbTarefas.FirstOrDefaultAsync(x => x.IdTarefa == id);
            return varTarefa == null ? NotFound() : Ok(varTarefa);      //Mensagem padrão informando que não encontrou o item ou exibe o item encontrado
        }

        /// <summary>
        /// Permite criar uma nova tarefa
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="ObjCriar">Recebe a descrição da tarefa para gerar um novo registro</param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "CriarTarefa")]
        public async Task<IActionResult> CriarTarefa(
            [FromServices] ClassDbContext ObjContext,
            [FromBody] CriarNovaTarefa ObjCriar)
        {
            if (!ModelState.IsValid) { return BadRequest(); }   //Certifica-se que o campo Required é valido

            //Instancia da model para preenchimento de dados que geram o novo registro de Tarefa na base
            MdTarefas vTarefa = new()
            {
                Status = false,
                Descricao = ObjCriar.DescricaoTarefa,
                DtLogCriacao = System.DateTime.Now
            };

            try
            {
                await ObjContext.objTbTarefas.AddAsync(vTarefa);
                await ObjContext.SaveChangesAsync();
                return Created("v1/ListarTarefas", vTarefa);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Permite editar a descrição das tarefas
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="objEditar">Permite modificar a descrição da tarefa</param>
        /// <param name="id">Identificação da tarefa que será modificada</param>
        /// <returns></returns>
        [HttpPut]
        [Route(template: "EditarTarefa/{id}")]
        public async Task<IActionResult> EditarTarefa(
            [FromServices] ClassDbContext ObjContext,
            [FromBody] EditarTarefa objEditar,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid) { return BadRequest(); }   //Certifica-se que o campo Required é valido

            //Instancia da model para preenchimento de dados que geram o novo registro de Tarefa na base
            MdTarefas vTarefa = await ObjContext.objTbTarefas.FirstOrDefaultAsync(x => x.IdTarefa == id);

            try
            {
                vTarefa.Descricao = objEditar.DescricaoTarefa;
                vTarefa.Status = objEditar.Status;
                ObjContext.objTbTarefas.Update(vTarefa);
                ObjContext.SaveChanges();
                return Ok(vTarefa);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Permite excluir tarefas existentes
        /// </summary>
        /// <param name="ObjContext">Objeto contexto para interação com a base de dados</param>
        /// <param name="id">Identificação da tarefa que será excluida</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(template: "ExcluirTarefa/{id}")]
        public async Task<IActionResult> ExcluirTarefa(
            [FromServices] ClassDbContext ObjContext,
            [FromRoute] int id
            )
        {
            var vTarefa = await ObjContext.objTbTarefas.FirstOrDefaultAsync(x => x.IdTarefa == id);

            try
            {
                ObjContext.objTbTarefas.Remove(vTarefa);
                await ObjContext.SaveChangesAsync();
                return Ok("v1/ListarTarefas");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
