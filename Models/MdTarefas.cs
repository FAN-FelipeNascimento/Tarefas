using System;
using System.ComponentModel.DataAnnotations;

namespace Tarefas.Models
{
    public class MdTarefas
    {
        /// <summary>
        /// Identificação exclusiva da tarefa
        /// </summary>
        [Key]
        public int IdTarefa { get; set; }
        /// <summary>
        /// Descricao da tarefa
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Situação da tarefa (Pendente(false) ou Concluida(true))
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Campo para controle de LOG, rastreando quando a tarefa foi criada
        /// </summary>
        public DateTime DtLogCriacao { get; set; } = DateTime.Now;
    }
}
