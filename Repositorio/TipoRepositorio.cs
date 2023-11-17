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
    public class TipoRepositorio : ITipo
    {
        private readonly ClassDbContext _context;

        public TipoRepositorio(ClassDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Implementação Listar toda os tipos
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MdTipo>> ListarTipo()
        {
            return await _context.objTbTipo.ToListAsync();
        }

        /// <summary>
        /// Implementação Selecioanr um Tipo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MdTipo> SelecionarTipo(int id)
        {
            return await _context.objTbTipo.Where(x => x.IdTipo == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Implementação Editar Tipo selecionado
        /// </summary>
        /// <param name="pTipo"></param>
        public void EditarTipo(MdTipo pTipo)
        {
            _context.Entry(pTipo).State = EntityState.Modified;
        }

        /// <summary>
        /// Implementação excluir Tipo selecionado
        /// </summary>
        /// <param name="pTipo"></param>
        public void ExcluirTipo(MdTipo pTipo)
        {
            _context.objTbTipo.Remove(pTipo);
        }

        /// <summary>
        /// Implementação incluir novo Tipo
        /// </summary>
        /// <param name="pTipo"></param>
        public void CadastrarTipo(MdTipo pTipo)
        {
            _context.objTbTipo.Add(pTipo);
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
