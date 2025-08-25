using ControleContatos.Filters;
using ControleContatos.Helpers;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {

        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            var usuarioLogado = _sessao.BuscarSessaoUsuario();
            List<ContatoModel> listaSql = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            
            return View(listaSql);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar o seu contato...";
                }
                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar o seu contato, mais detalhes do erro: {err.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioLogado = _sessao.BuscarSessaoUsuario();

                    contato.UsuarioId = usuarioLogado.Id;
                    contato = _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception err)
            {
                TempData["MensagemErro"] = $"Ops...Não conseguimos cadastrar o seu contato, tente novamente, detalhe do erro: {err.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception err)
            {
                TempData["MensagemErro"] = $"Ops...Não conseguimos atualizar o seu contato, tente novamente, detalhe do erro: {err.Message}";
                return RedirectToAction("Index");
                throw;
            }
        }


    }

}
