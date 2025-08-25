using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class RedefinicaoSenhaModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Digite o email")]
        [EmailAddress(ErrorMessage = "O email precisa ser válido")]
        public required string Email { get; set; }
    }
}
