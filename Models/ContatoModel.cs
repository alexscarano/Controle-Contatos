using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class ContatoModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Digite o email do contato")]
        [EmailAddress(ErrorMessage = "O email informado é inválido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "O celular informado não é válido")]
        public required string Celular { get; set; }
        public int ?UsuarioId { get; set; }
        public UsuarioModel ?Usuario { get; set; }
    }
}
