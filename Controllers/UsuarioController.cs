using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace tl2_tp10_2023_FranceFalci.Controllers;
using tl2_tp10_2023_FranceFalci;

public class UsuarioController : Controller
{
  private IUsuarioRepository usuarioRepository;

  private readonly ILogger<HomeController> _logger;

  public UsuarioController(ILogger<HomeController> logger)
  {
    _logger = logger;
    usuarioRepository = new UsuarioRepository();

  }


  [HttpGet]
  public IActionResult GetUsuarios()
  {
    // if (String.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) return RedirectToRoute(new { controller = "Logueo", action = "Index" });
    // if (HttpContext.Session.GetString("rol") != Rol.Administrador.ToString()) return RedirectToRoute(new { controller = "Logueo", action = "Index" });
  // var vista = new ListarUsuariosViewModel();
    var usuarios = usuarioRepository.GetAll();
    return View(new ListarUsuariosViewModel().GetIndexUsuariosViewModel(usuarios));
  }
  [HttpGet]
  public IActionResult CrearUsuario()
  {
    return View(new CrearUsuarioViewModel());
  }

  [HttpPost]
  public IActionResult CrearUsuario(CrearUsuarioViewModel usuarioCreadoVM)
  {
    if (!ModelState.IsValid) return RedirectToAction("CrearUsuario");

    var usuario = new Usuario(usuarioCreadoVM);
    usuarioRepository.Create(usuario);
    return RedirectToAction("GetUsuarios");
  }


  [HttpGet]
  public IActionResult EditarUsuario(int idUsuario)
  {
    var usuarioBuscado = usuarioRepository.GetById(idUsuario);
    return View(new EditarUsuarioViewModel(usuarioBuscado));
  }


  [HttpPost]
  public IActionResult EditarUsuario(EditarUsuarioViewModel usuarioEditadoVM)
  {
    if (!ModelState.IsValid) return RedirectToAction("EditarUsuario", new { idUsuario = usuarioEditadoVM.Id });

    var usuario = new Usuario(usuarioEditadoVM);
    usuarioRepository.Update(usuario);

    return RedirectToAction("GetUsuarios");

  }

  // ! [HttpDelete] porq?? 
  public IActionResult EliminarUsuario(int idUsuario)
  {
    usuarioRepository.Remove(idUsuario);
    return RedirectToAction("GetUsuarios");
  }
}