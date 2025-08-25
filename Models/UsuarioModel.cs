using System.ComponentModel.DataAnnotations;
using ControleContatos.Enums;
using ControleContatos.Helpers;

namespace ControleContatos.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Usuário")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do Usuário")]
        public required string Login {  get; set; }

        [Required(ErrorMessage = "Digite o email do Usuário")]
        [EmailAddress(ErrorMessage = "O email informado é inválido")]
        public required string Email { get; set; }

        public required PerfilEnum TipoUsuario { get; set; }

        [Required(ErrorMessage = "Digite a senha do Usuário")]
        public required string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime ?DataAtualizacao { get; set; }

        public virtual List<ContatoModel> ?Contatos { get; set; }

        public bool senhaValida(string senha)
        {
            return Senha.Equals(senha.GerarHash());
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();

            return novaSenha;
        }
        

    }
}
