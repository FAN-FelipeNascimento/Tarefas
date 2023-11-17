using System.ComponentModel.DataAnnotations;

namespace Tarefas.ViewModel
{
    public class vmTipo
    {
        [Required]
        public string DescricaoTipo { get; set; }
    }
}
