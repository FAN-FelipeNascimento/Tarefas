using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Models;

namespace Tarefas.Interface
{
    public interface ITipo
    {
        /// <summary>
        /// Interface - retorna todas os Tipos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MdTipo>> ListarTipo();
        /// <summary>
        /// Interface - retorna Tipo selecionada por ID
        /// </summary>
        /// <param name="id">Identificação exclusiva do Tipo</param>
        /// <returns></returns>
        Task<MdTipo> SelecionarTipo(int id);

        /// <summary>
        /// Interface - Inclui tipo
        /// </summary>
        /// <param name="pTipo"></param>
        void CadastrarTipo(MdTipo pTipo);

        /// <summary>
        /// Interface - Altera tipo
        /// </summary>
        /// <param name="pTipo"></param>
        void EditarTipo(MdTipo mdTipo);
        /// <summary>
        /// Interface - Exclui tipo
        /// </summary>
        /// <param name="id">Identificação exclusiva do tipo</param>
        void ExcluirTipo(MdTipo pTipo);
        /// <summary>
        /// Para cada operação de persistencia de dados.
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAllAsync();

    }
}
