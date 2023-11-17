using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tarefas.Data;
using Tarefas.ViewModel;
using Tarefas.Models;
using Tarefas.Interface;

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
        private readonly ITarefas _tarefaRepositorio;

        public TarefasController(ITarefas tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        /// <summary>
        /// Lista todas as tarefas existentes na base
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "ListarTarefas")] //define a url com a especificação do versionamento - ex: vi/ListaTarefas
        public async Task<ActionResult<IEnumerable<MdTarefas>>> TodasAsTarefas()
        {
            return Ok(await _tarefaRepositorio.ListarTarefas());
        }

        /// <summary>
        /// Exibe Tarefa selecionado pelo Id - (chave primaria na tabela de tarefas)
        /// </summary>
        /// <param name="ObjContext"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "SelecionarTarefa/{id}")]
        public async Task<IActionResult> SelecionarTarefa(int id)
        {
            var pTarefa = await _tarefaRepositorio.SelecionarTarefas(id);
            if(pTarefa == null)
            {
                return NotFound("Tarefa não encontrada");
            }
            return Ok(pTarefa);
        }

        /// <summary>
        /// Permite criar uma nova tarefa
        /// </summary>
        /// <param name="pTarefa"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "CriarTarefa")]
        public async Task<IActionResult> CriarTarefa(MdTarefas pTarefa)
        {
            _tarefaRepositorio.IncluirTarefa(pTarefa);
            if (await _tarefaRepositorio.SaveAllAsync())
            {
                return Ok("Nova tarefa criada com sucesso.");
            }

            return BadRequest("Ocorreu um erro ao salvar uma nova tarefa");
        }

        /// <summary>
        /// Permite editar a descrição das tarefas
        /// </summary>
        /// <param name="pTarefa"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(template: "EditarTarefa/{id}")]
        public async Task<IActionResult> EditarTarefa(MdTarefas pTarefa)
        {
            _tarefaRepositorio.EditarTarefa(pTarefa);
            if (await _tarefaRepositorio.SaveAllAsync())
            {
                return Ok("Tarefa alterada com sucesso.");
            }

            return BadRequest("Ocorreu um erro ao tentar alterar uma tarefa");
        }

        /// <summary>
        /// Permite excluir tarefas existentes
        /// </summary>
        /// <param name="pTarefa"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(template: "ExcluirTarefa/{id}")]
        public async Task<IActionResult> ExcluirTarefa(int id)
        {
            var pTarefa = await _tarefaRepositorio.SelecionarTarefas(id);
            _tarefaRepositorio.ExcluirTarefa(pTarefa);
            if (await _tarefaRepositorio.SaveAllAsync())
            {
                return Ok("Tarefa excluida com sucesso.");
            }

            return BadRequest("Ocorreu um erro ao tentar excluir a tarefa");
        }
    }
}
