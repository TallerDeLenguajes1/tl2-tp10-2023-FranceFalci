using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class TableroController : Controller
{
  private ITableroRepository tableroRepository;

  private readonly ILogger<HomeController> _logger;

  public TableroController(ILogger<HomeController> logger)
  {
    _logger = logger;
    tableroRepository = new TableroRepository();

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
      int idUsuario = HttpContext.Session.GetInt32("ID") ?? -1;
      var tableros = tableroRepository.GetTableroByIdUsuario(idUsuario);
      return View(tableros);
    }

    return RedirectToRoute(new { controller = "Login", action = "Index" });
  }


  [HttpGet]
  public IActionResult CrearTablero()
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    return View(new Tablero());
  }

  [HttpPost]
  public IActionResult CrearTablero(Tablero tablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    tableroRepository.Create(tablero);
    return RedirectToAction("GetTableros");
  }

  [HttpGet]
  public IActionResult EditarTablero(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tableroBuscado = tableroRepository.GetTableroById(idTablero);
    return View(tableroBuscado);
  }

  [HttpPost]
  public IActionResult EditarTablero(Tablero tablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    var tableroAModificar = tableroRepository.GetTableroById(tablero.IdTablero);
    tableroAModificar.Descripcion = tablero.Descripcion;
    tableroAModificar.Nombre = tablero.Nombre;
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
    if (HttpContext.Session != null && HttpContext.Session.GetInt32("NivelAcceso") == 1)
      return true;

    return false;
  }
  private bool isOperador()
  {
    if (HttpContext.Session != null && HttpContext.Session.GetInt32("NivelAcceso") == 0)
      return true;

    return false;
  }
  private bool isLogueado(){
    if (HttpContext.Session != null && (HttpContext.Session.GetInt32("NivelAcceso") == 1 || HttpContext.Session.GetInt32("NivelAcceso") == 0))
      return true;

    return false;
  }
}