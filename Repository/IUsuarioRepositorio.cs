using ControleContatos.Models;

namespace ControleContatos.Repository
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorEmailOuLogin(string email, string login);
        
        UsuarioModel BuscarPorLogin(string login);
        
        List<UsuarioModel> BuscarTodos();

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel ListarPorId(int id);

        UsuarioModel Atualizar(UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);

        bool Apagar(int id);


    }
}
