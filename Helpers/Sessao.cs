using ControleContatos.Models;
using Newtonsoft.Json;

namespace ControleContatos.Helpers
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
            string usuarioString = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("userLogged", usuarioString);
        }

        public UsuarioModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("userLogged");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("userLogged");
        }
    }
}
