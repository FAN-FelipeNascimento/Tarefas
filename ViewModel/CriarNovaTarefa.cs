using System.ComponentModel.DataAnnotations;

namespace Tarefas.ViewModel
{
    public class CriarNovaTarefa
    {
        [Required]
        public string DescricaoTarefa { get; set; }
    }
}
