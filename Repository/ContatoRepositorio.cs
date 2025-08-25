using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repository
{
    public class ContatoRepositorio : IContatoRepositorio
    {

        private readonly AppDbContext _appDbContext;

        public ContatoRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            return _appDbContext.Contato.FirstOrDefault(x => x.Id == id);
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoConsulta = ListarPorId(id);

            if (contatoConsulta == null) throw new Exception("Houve um erro na Deleção");

            _appDbContext.Contato.Remove(contatoConsulta);
            _appDbContext.SaveChanges();

            return true;
        }


        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoConsulta = ListarPorId(contato.Id);

            if (contatoConsulta == null || contatoConsulta.Id != contato.Id) throw new Exception("Houve um erro na atualização");

            contatoConsulta.Nome = contato.Nome;
            contatoConsulta.Email = contato.Email;  
            contatoConsulta.Celular = contato.Celular;

            _appDbContext.Update(contatoConsulta);
            _appDbContext.SaveChanges();

            return contatoConsulta;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _appDbContext.Contato.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _appDbContext.Contato.Add(contato);
            _appDbContext.SaveChanges();

            return contato;
        }

        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _appDbContext.Contato.
                Where(x => x.UsuarioId == usuarioId).
                ToList();
        }
    }
}
