using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace tl2_tp10_2023_FranceFalci.Controllers;
using tl2_tp10_2023_FranceFalci;

public class UsuarioController : Controller
{
  private readonly IUsuarioRepository  usuarioRepository;

  private readonly ILogger<HomeController> _logger;

  public UsuarioController(ILogger<HomeController> logger,IUsuarioRepository usuarioRepository)
  {
    _logger = logger;
    this.usuarioRepository = usuarioRepository;

  }


  [HttpGet]
  public IActionResult GetUsuarios()
  {
    if (isAdmin()){
      var usuarios = usuarioRepository.GetAll();
    return View(new ListarUsuariosViewModel().GetIndexUsuariosViewModel(usuarios));
    }
  return RedirectToRoute(new { controller = "Home", action = "Error" });

  }
  [HttpGet]
  public IActionResult CrearUsuario()
  {
    if (isAdmin()) return View(new CrearUsuarioViewModel());
    return RedirectToRoute(new { controller = "Home", action = "Error" });

  }

  [HttpPost]
  public IActionResult CrearUsuario(CrearUsuarioViewModel usuarioCreadoVM)
  {
    if (isAdmin()){
    if (!ModelState.IsValid) return RedirectToAction("CrearUsuario");
    var usuario = new Usuario(usuarioCreadoVM);
    usuarioRepository.Create(usuario);
    return RedirectToAction("GetUsuarios");
    }
    return RedirectToRoute(new { controller = "Home", action = "Error" });

  }


  [HttpGet]
  public IActionResult EditarUsuario(int idUsuario)
  {
    if (isAdmin()){
      var usuarioBuscado = usuarioRepository.GetById(idUsuario);
      return View(new EditarUsuarioViewModel(usuarioBuscado));
      }
    return RedirectToRoute(new { controller = "Home", action = "Error" });


  }


  [HttpPost]
  public IActionResult EditarUsuario(EditarUsuarioViewModel usuarioEditadoVM)
  {
    if (isAdmin())
    {
      if (!ModelState.IsValid) return RedirectToAction("EditarUsuario", new { idUsuario = usuarioEditadoVM.Id });

    var usuario = new Usuario(usuarioEditadoVM);
    usuarioRepository.Update(usuario);
    return RedirectToAction("GetUsuarios");
    }
    return RedirectToRoute(new { controller = "Home", action = "Error" });

  }

  // ! [HttpDelete] porq?? 
  public IActionResult EliminarUsuario(int idUsuario)
  {
    if (isAdmin()){
      usuarioRepository.Remove(idUsuario);
    return RedirectToAction("GetUsuarios");
    }
    return RedirectToRoute(new { controller = "Home", action = "Error" });

  }
  private bool isLogueado()
{
  if (HttpContext.Session != null && (HttpContext.Session.GetInt32("NivelAcceso") == 1 || HttpContext.Session.GetInt32("NivelAcceso") == 2))
    return true;

  return false;
}

  private bool isAdmin()
  {
    Console.WriteLine(HttpContext.Session.GetInt32("NivelAcceso"));
    if (HttpContext.Session != null && HttpContext.Session.GetInt32("NivelAcceso") == 2)
      return true;

    return false;
  }
}

