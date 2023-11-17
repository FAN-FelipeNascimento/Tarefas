using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tarefas.Data;
using Tarefas.Models;
using Tarefas.ViewModel;
using Tarefas.Interface;
using System.Collections.Generic;

namespace Tarefas.Controller
{
    [ApiController]
    [Route(template: "v1")]
    public class TipoTarefaController : ControllerBase
    {
        private readonly ITipo _tipoRepositorio;

        public TipoTarefaController(ITipo tipoRepositorio)
        {
            _tipoRepositorio = tipoRepositorio;
        }

        /// <summary>
        /// Lista todos os Tipos de Tarefas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "ListarTipos")]
        public async Task<ActionResult<IEnumerable<MdTipo>>> ListarTipos()
        {
            return Ok(await _tipoRepositorio.ListarTipo());
        }

        /// <summary>
        /// Exibe Tipo selecionado pelo Id - (chave primaria na tabela de Tipo)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "SelecionarTipo/{id}")]
        public async Task<IActionResult> SelecionarTipo(int id)
        {
            var pTipo = await _tipoRepositorio.SelecionarTipo(id);
            if (pTipo == null)
            {
                return NotFound("Tipo não encontrado");
            }
            return Ok(pTipo);
        }

        /// <summary>
        /// Permite criar um novo Tipo
        /// </summary>
        /// <param name="pTipo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "CriarTipo")]
        public async Task<IActionResult> CriarTipo(MdTipo pTipo)
        {
            _tipoRepositorio.CadastrarTipo(pTipo);
            if (await _tipoRepositorio.SaveAllAsync())
            {
                return Ok("Novo tipo criado com sucesso.");
            }

            return BadRequest("Ocorreu um erro ao salvar um novo tipo");
        }

        /// <summary>
        /// Permite editar a descrição do Tipo
        /// </summary>
        /// <param name="pTipo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(template: "EditarTipo/{id}")]
        public async Task<IActionResult> EditarTipo(MdTipo pTipo)
        {
            _tipoRepositorio.EditarTipo(pTipo);
            if (await _tipoRepositorio.SaveAllAsync())
            {
                return Ok("Tipo alterado com sucesso.");
            }

            return BadRequest("Ocorreu um erro ao tentar alterar o tipo");
        }

        /// <summary>
        /// Permite excluir Tipo existentes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(template: "ExcluirTipo/{id}")]
        public async Task<IActionResult> ExcluirTipo(int id)
        {
            var pTipo = await _tipoRepositorio.SelecionarTipo(id);
            _tipoRepositorio.ExcluirTipo(pTipo);
            if (await _tipoRepositorio.SaveAllAsync())
            {
                return Ok("Tipo excluido com sucesso.");
            }

            return BadRequest("Ocorreu um erro ao tentar excluir o tipo");
        }
    }
}
