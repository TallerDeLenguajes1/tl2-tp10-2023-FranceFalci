using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class LoginController : Controller
{
  // List<Usuario> Usuarios = new List<Usuario>();
  private IUsuarioRepository usuarioRepository;

  private readonly ILogger<LoginController> _logger;
  public LoginController(ILogger<LoginController> logger)
  {
    _logger = logger;
    usuarioRepository = new UsuarioRepository();
  }

  public IActionResult Index()
  {
    return View(new LoginViewModel());
  }

[HttpPost]
  public IActionResult Login(Usuario usuario)
  {
    //existe el usuario?
    var existeUsuario = usuarioRepository.validarUsuario(usuario);
    System.Diagnostics.Debug.WriteLine($"Usuario: {usuario.NombreUsuario}, Contraseña: {usuario.Contrasenia}");

    // si el usuario no existe devuelvo al index
    if (existeUsuario== null) return RedirectToAction("Index");

    //Registro el usuario
    logearUsuario(existeUsuario);

    //Devuelvo el usuario al Home
    return RedirectToRoute(new { controller = "Home", action = "Index" });
  }

  private void logearUsuario(Usuario usuario)
  {
    Debug.WriteLine($"Usuario: {usuario.NombreUsuario}");
    Debug.WriteLine($"NivelAcceso: {usuario.Rol}");
    Debug.WriteLine($"ID: {usuario.Id}");
    HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);
    HttpContext.Session.SetInt32("NivelAcceso", (int)usuario.Rol);
    HttpContext.Session.SetInt32("ID", usuario.Id);

  }
}
