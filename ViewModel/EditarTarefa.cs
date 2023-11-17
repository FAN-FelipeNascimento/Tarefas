using System.ComponentModel.DataAnnotations;

namespace Tarefas.ViewModel
{
    public class EditarTarefa
    {
        [Required]
        public string DescricaoTarefa { get; set; }
        public bool Status { get; set; }
    }
}
