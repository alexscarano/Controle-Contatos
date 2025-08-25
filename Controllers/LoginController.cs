using ControleContatos.Helpers;
using ControleContatos.Models;
using ControleContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController
        (
            IUsuarioRepositorio usuarioRepositorio, 
            ISessao sessao,
            IEmail email
        ) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            // Se o usuário estiver logado, redirecionar para a home diretamente
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");
            
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost] 
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario != null)
                    {
                        if (usuario.senhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                             return RedirectToAction("Index", "Home");
                        }
                    }
                    TempData["MensagemErro"] = $"Login ou senha inválidos";
                }
                return View("Index");
            }
            catch (Exception err)
            {
                TempData["MensagemErro"] = $"Ops: Não consegui entrar, mais detalhes do erro: {err.Message}";
                return RedirectToAction("Error");
            }
        
        }
    
        public IActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarLinkRedefinirSenha(RedefinicaoSenhaModel redefinicaoSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailOuLogin(redefinicaoSenhaModel.Email, redefinicaoSenhaModel.Login);
                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();

                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de contatos - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        }
                        else
                        { 
                            TempData["MensagemErro"] = $"Não conseguimos enviar o email, Por favor, tente novamente.";
                        }

                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir a sua senha, verifique os dados informados.";
                }
                return View("Index");
            }
            catch (Exception err)
            {
                TempData["MensagemErro"] = $"Ops, não consegui redefinir a senha, mais detalhes do erro: {err.Message}";
                return RedirectToAction("Error");
            }
        }


    }
}
