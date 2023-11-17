using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;

namespace Tarefas.Interface
{
    public interface ITarefas
    {
        /// <summary>
        /// Interface - retorna todas as tarefas
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MdTarefas>> ListarTarefas();
        /// <summary>
        /// Interface - retorna tarefa selecionada por ID
        /// </summary>
        /// <param name="id">Identificação exclusiva da tarefa</param>
        /// <returns></returns>
        Task<MdTarefas> SelecionarTarefas(int id);

        /// <summary>
        /// Interface - Inclui tarefa
        /// </summary>
        /// <param name="pTarefa"></param>
        void IncluirTarefa(MdTarefas pTarefa);

        /// <summary>
        /// Interface - Altera tarefa
        /// </summary>
        /// <param name="pTarefa"></param>
        void EditarTarefa(MdTarefas pTarefa);
        /// <summary>
        /// Interface - Exclui tarefa
        /// </summary>
        /// <param name="id">Identificação exclusiva da tarefa</param>
        void ExcluirTarefa(MdTarefas pTarefa);
        /// <summary>
        /// Para cada operação de persistencia de dados.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAllAsync();

    }
}
