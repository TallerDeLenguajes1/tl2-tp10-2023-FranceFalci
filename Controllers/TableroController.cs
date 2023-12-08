using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class TableroController : Controller
{
  private readonly ITableroRepository tableroRepository;

  private readonly ILogger<HomeController> _logger;

  public TableroController(ILogger<HomeController> logger, ITableroRepository tableroRepository)
  {
    _logger = logger;
    this.tableroRepository = tableroRepository;

  }


  [HttpGet]
  public IActionResult GetTableros()
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    
    if(isAdmin()){
      var tablerosGeneral = tableroRepository.GetTableros();
      return View(tablerosGeneral);
    }

    if(isOperador()){
      Console.WriteLine("Rol: Operador");

      int idUsuario = HttpContext.Session.GetInt32("ID") ?? -1;
      var tableros = tableroRepository.GetTableroByIdUsuario(idUsuario);
      return View(tableros);
    }

    return RedirectToRoute(new { controller = "Login", action = "Index" });
  }


  [HttpGet]
  public IActionResult CrearTablero()
  {
    // Debug.WriteLine("id Usuario : {idUsuario}");
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    return View(new CrearTableroViewModel());
  }

  [HttpPost]
  public IActionResult CrearTablero(CrearTableroViewModel tableroCreadoVM)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    if (!ModelState.IsValid) return RedirectToAction("CrearUsuario");
    var tablero = new Tablero(tableroCreadoVM);
    tablero.IdUsuario = (int)HttpContext.Session.GetInt32("ID");
    tableroRepository.Create(tablero);
    return RedirectToAction("GetTableros");
  }

  [HttpGet]
  public IActionResult EditarTablero(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tableroAEditar = tableroRepository.GetTableroById(idTablero);
    return View(new EditarTableroViewModel(tableroAEditar));
  }

  [HttpPost]
  public IActionResult EditarTablero(EditarTableroViewModel tableroEditadoVM)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    if (!ModelState.IsValid) return RedirectToAction("EditarTablero", new { idTablero = tableroEditadoVM.IdTablero });

    var tableroAModificar = new Tablero(tableroEditadoVM);
    tableroRepository.Update(tableroAModificar, tableroAModificar.IdTablero);
    return RedirectToAction("GetTableros");
  }
  public IActionResult EliminarTablero(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    tableroRepository.RemoveTablero(idTablero);
    return RedirectToAction("GetTableros");
  }

  private bool isAdmin()
  {
    Console.WriteLine(HttpContext.Session.GetInt32("NivelAcceso"));
    if (HttpContext.Session != null && HttpContext.Session.GetInt32("NivelAcceso") == 2)
      return true;

    return false;
  }
  private bool isOperador()
  {
    if (HttpContext.Session != null && HttpContext.Session.GetInt32("NivelAcceso") == 1)
      return true;

    return false;
  }
  private bool isLogueado(){
    if (HttpContext.Session != null && (HttpContext.Session.GetInt32("NivelAcceso") == 1 || HttpContext.Session.GetInt32("NivelAcceso") == 2))
      return true;

    return false;
  }
}