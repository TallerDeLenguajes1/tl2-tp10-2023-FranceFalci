using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class LoginController : BaseController
{
  // List<Usuario> Usuarios = new List<Usuario>();
  private readonly IUsuarioRepository usuarioRepository;

  private readonly ILogger<LoginController> _logger;
  public LoginController(ILogger<LoginController> logger,IUsuarioRepository usuarioRepository)
  {
    _logger = logger;
    this.usuarioRepository = usuarioRepository;
  }

  public IActionResult Index()
  {
    return View(new LoginViewModel());
  }

[HttpPost]
  public IActionResult Login(Usuario usuario)
  {    
    var existeUsuario = usuarioRepository.validarUsuario(usuario);

    if (existeUsuario== null) {
      SweetAlert("Datos incorrectos.", NotificationType.Error, "Error!");
      return RedirectToAction("Index");

    }

    logearUsuario(existeUsuario);
    return RedirectToRoute(new { controller = "Home", action = "Index" });
  }

  // [HttpPost]
  // public IActionResult Logout()
  // {
  //   // Cerrar la sesión y eliminar los datos de la sesión
  //   HttpContext.Session.Clear(); // Clear all keys
  //   HttpContext.Session.Remove("Usuario"); // Remove specific key
  //   HttpContext.Session.Remove("NivelAcceso"); 
  //   HttpContext.Session.Remove("ID");
  //   // Redirigir a la página de inicio de sesión o a donde desees después de cerrar sesión
  //   return RedirectToAction("Index", "Home");
  // }
  private void logearUsuario(Usuario usuario)
  {
    // Debug.WriteLine($"Usuario: {usuario.NombreUsuario}");
    // Debug.WriteLine($"NivelAcceso: {usuario.Rol}");
    // Debug.WriteLine($"ID: {usuario.Id}");
    HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);
    HttpContext.Session.SetInt32("NivelAcceso", (int)usuario.Rol);
    HttpContext.Session.SetInt32("ID", usuario.Id);

  }
}
