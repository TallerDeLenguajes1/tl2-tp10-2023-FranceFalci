using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace tl2_tp10_2023_FranceFalci.Controllers;
using tl2_tp10_2023_FranceFalci;

public class UsuarioController : BaseController
{
  private readonly IUsuarioRepository  usuarioRepository;

  private readonly ILogger<HomeController> logger;

  public UsuarioController(ILogger<HomeController> logger,IUsuarioRepository usuarioRepository)
  {
    this.logger = logger;
    this.usuarioRepository = usuarioRepository;

  }


  [HttpGet]
  [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

  public IActionResult GetUsuarios()
  {
    if (!isLogueado())
    {
      SweetAlert("Error al ver usuarios. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    }

    if (!isAdmin()) return RedirectToRoute(new { controller = "Home", action = "Error" });

    var usuarios = usuarioRepository.GetAll();
    return View(new ListarUsuariosViewModel().GetIndexUsuariosViewModel(usuarios));
    

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
    if (!isLogueado())
    {
      SweetAlert("Error al crear usuario. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    }
    if (!isAdmin()) return RedirectToRoute(new { controller = "Home", action = "Error" });

    if (!ModelState.IsValid) return RedirectToAction("CrearUsuario");

    try{
      var usuario = new Usuario(usuarioCreadoVM);
      usuarioRepository.Create(usuario);
      SweetAlert("Usuario creado con exito", NotificationType.Success, "Genial!");

    }
    catch (Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al editar usuario.", NotificationType.Error, "Oops..");

    }
    return RedirectToAction("GetUsuarios");
    

  }


  [HttpGet]
  public IActionResult EditarUsuario(int idUsuario)
  {
    if (!isAdmin()) return RedirectToRoute(new { controller = "Home", action = "Error" });

    try{

      var usuarioBuscado = usuarioRepository.GetById(idUsuario);
      return View(new EditarUsuarioViewModel(usuarioBuscado));

    }catch (Exception ex){
      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action  = "Error" });

    }



  }


  [HttpPost]
  public IActionResult EditarUsuario(EditarUsuarioViewModel usuarioEditadoVM)
  {
    if (!isLogueado())
    {
      SweetAlert("Error al editar usuario. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    }

    if (!isAdmin()) return RedirectToRoute(new { controller = "Home", action = "Error" });
    
    if (!ModelState.IsValid) return RedirectToAction("EditarUsuario", new { idUsuario = usuarioEditadoVM.Id });

    try{

      var usuario = new Usuario(usuarioEditadoVM);
      usuarioRepository.Update(usuario);
      SweetAlert("Usuario editado con exito", NotificationType.Success, "Genial!");
    
    }catch(Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al eliminar usuario.", NotificationType.Error, "Oops..");
    }
    
    return RedirectToAction("GetUsuarios");

  }

  // ! [HttpDelete] porq?? 
  public IActionResult EliminarUsuario(int idUsuario)
  {
    if (!isAdmin()) return RedirectToRoute(new { controller = "Home", action = "Error" });

    try{

      usuarioRepository.Remove(idUsuario);
      SweetAlert("Usuario eliminado con exito", NotificationType.Success, "Genial!");
    
    }catch(Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al eliminar usuario.", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Home", action = "Error" });
    }
      return RedirectToAction("GetUsuarios");
    

  }
  private bool isLogueado()
{
  if (HttpContext.Session != null && (HttpContext.Session.GetInt32("NivelAcceso") == 1 || HttpContext.Session.GetInt32("NivelAcceso") == 2))
    return true;

  return false;
}

  private bool isAdmin()
  {
    if (HttpContext.Session != null && HttpContext.Session.GetInt32("NivelAcceso") == 2)
      return true;

    return false;
  }
}

