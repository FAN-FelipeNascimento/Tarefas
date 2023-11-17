using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Tarefas.Interface;
using Tarefas.Models;
using Tarefas.Data;
using Microsoft.EntityFrameworkCore;

namespace Tarefas.Repositorio
{
    /// <summary>
    /// Classe com a implementação dos metodos da Interface
    /// </summary>
    public class TarefasRepositorio : ITarefas
    {
        private readonly ClassDbContext _context;

        public TarefasRepositorio(ClassDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Implementação Listar toda as tarefas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MdTarefas>> ListarTarefas()
        {
            return await _context.objTbTarefas.ToListAsync();
        }

        /// <summary>
        /// Implementação Selecioanr uma tarefa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MdTarefas> SelecionarTarefas(int id)
        {
            return await _context.objTbTarefas.Where(x => x.IdTarefa == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Implementação Editar tarefa selecionada
        /// </summary>
        /// <param name="pTarefa"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void EditarTarefa(MdTarefas pTarefa)
        {
            _context.Entry(pTarefa).State = EntityState.Modified;
        }

        /// <summary>
        /// Implementação excluir tarefas selecionada
        /// </summary>
        /// <param name="pTarefa"></param>
        public void ExcluirTarefa(MdTarefas pTarefa)
        {
            _context.objTbTarefas.Remove(pTarefa);
        }

        /// <summary>
        /// Implementação incluir nova tarefa
        /// </summary>
        /// <param name="pTarefa"></param>
        public void IncluirTarefa(MdTarefas pTarefa)
        {
            _context.objTbTarefas.Add(pTarefa);
        }

        /// <summary>
        /// Implementação salva cada persistencia executada
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
