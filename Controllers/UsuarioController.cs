using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

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
    var usuarios = usuarioRepository.GetAll();
    return View(usuarios);
  }
  [HttpGet]
  public IActionResult CrearUsuario()
  {
    return View(new Usuario());
  }

  [HttpPost]
  public IActionResult CrearUsuario(Usuario usuario)
  {
    usuarioRepository.Create(usuario);
    return RedirectToAction("GetUsuarios");
  }


  [HttpGet]
  public IActionResult EditarUsuario(int idUsuario)
  {
    var usuarioBuscado = usuarioRepository.GetById(idUsuario);
    return View(usuarioBuscado);
  }


  [HttpPost]
  public IActionResult EditarUsuario(Usuario usuario)
  {
    var usuarioAModificar = usuarioRepository.GetById(usuario.Id);
    usuarioAModificar.NombreUsuario = usuario.NombreUsuario;
    usuarioRepository.Update(usuarioAModificar);

    return RedirectToAction("GetUsuarios");
 
  }

  // ! [HttpDelete] porq?? 
  public IActionResult EliminarUsuario(int idUsuario)
  {
    usuarioRepository.Remove(idUsuario);
    return RedirectToAction("GetUsuarios");
  }
}