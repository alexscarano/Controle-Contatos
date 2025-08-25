using ControleContatos.Data;
using ControleContatos.Helpers;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatos.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly AppDbContext _appDbContext;

        public UsuarioRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _appDbContext.Usuario.FirstOrDefault(x => x.Id == id);
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioConsulta = ListarPorId(id);

            if (usuarioConsulta == null) throw new Exception("Houve um erro na Deleção");

            _appDbContext.Usuario.Remove(usuarioConsulta);
            _appDbContext.SaveChanges();

            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioConsulta = ListarPorId(usuario.Id);

            if (usuarioConsulta == null || usuarioConsulta.Id != usuarioConsulta.Id) throw new Exception("Houve um erro na atualização");

            usuarioConsulta.Nome = usuario.Nome;
            usuarioConsulta.Email = usuario.Email;
            usuarioConsulta.Login = usuario.Login;
            usuarioConsulta.TipoUsuario = usuario.TipoUsuario;
            usuarioConsulta.DataAtualizacao = DateTime.Now;

            _appDbContext.Update(usuarioConsulta);
            _appDbContext.SaveChanges();

            return usuarioConsulta;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _appDbContext.Usuario
                .Include(x => x.Contatos)
                .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _appDbContext.Usuario.Add(usuario);
            _appDbContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _appDbContext.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailOuLogin(string email, string login)
        {
            return _appDbContext.Usuario.FirstOrDefault(
                x => x.Email.ToUpper() == email.ToUpper() 
                && x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioConsulta = ListarPorId(alterarSenhaModel.Id);

            if (usuarioConsulta == null) 
                throw new Exception("Houve um erro na atualização da senha, usuário não encontrado");
            
            if (!usuarioConsulta.senhaValida(alterarSenhaModel.SenhaAtual))
                throw new Exception("Senha atual não confere");

            if (usuarioConsulta.senhaValida(alterarSenhaModel.NovaSenha))
                throw new Exception("A nova senha deve ser diferente da senha atual");

            usuarioConsulta.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioConsulta.DataAtualizacao = DateTime.Now;

            _appDbContext.Usuario.Update(usuarioConsulta);
            _appDbContext.SaveChanges();

            return usuarioConsulta;
        }
    }
}
