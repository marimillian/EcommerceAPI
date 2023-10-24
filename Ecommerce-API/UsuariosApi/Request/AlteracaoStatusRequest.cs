using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Request
{
    public class AlteracaoStatusRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
