using System.ComponentModel.DataAnnotations;

namespace Tarefas.Models
{
    public class MdTipo
    {
        /// <summary>
        /// Identificação exclusiva do Tipo de Tarefa
        /// </summary>
        [Key]
        public int IdTipo { get; set; }
        /// <summary>
        /// Descrição do Tipo de Tarefa
        /// </summary>
        public string TipoTarefa { get; set; }

    }
}