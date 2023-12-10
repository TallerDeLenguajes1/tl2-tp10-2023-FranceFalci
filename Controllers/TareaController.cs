using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class TareaController : Controller
{
  // private ITareaRepository tareaRepository;
  private readonly ITareaRepository tareaRepository;

  private readonly ILogger<HomeController> _logger;

  public TareaController(ILogger<HomeController> logger,ITareaRepository tareaRepository)
  {
    _logger = logger;
    this.tareaRepository = tareaRepository;

  }

  [HttpGet]
  public IActionResult GetTareas()
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tareas = tareaRepository.GetTareasPorTablero(1);
    return View(tareas);
  }

  [HttpGet]
  public IActionResult GetTareasByIdTablero(int idTablero)
  {
    
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tareas = tareaRepository.GetTareasPorTablero(idTablero);
    return View( new ListarTareasViewModel().GetIndexTareaViewModel(tareas));
  }

  [HttpGet]
  public IActionResult CrearTareaForm(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    return View(new CrearTareaViewModel(idTablero));
  }

  [HttpPost]
  public IActionResult CrearTarea(CrearTareaViewModel tareaCreadaVM)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tarea = new Tarea(tareaCreadaVM);
    if (!ModelState.IsValid) return RedirectToAction("CrearTarea");
    tareaRepository.Create(tarea, tarea.IdTablero);
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tarea.IdTablero });
  }



  [HttpGet]
  public IActionResult EditarTarea(int idTarea)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tareaBuscado = tareaRepository.GetTareaById(idTarea);
    return View(new EditarTareaViewModel(tareaBuscado));
  }

  [HttpPost]
  public IActionResult EditarTarea(EditarTareaViewModel tareaEditadaVM)
  {

    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
    var tareaAModificar = new Tarea(tareaEditadaVM);
    Debug.WriteLine("{tareaAModificar.IdTablero}");
    tareaRepository.Update(tareaAModificar, tareaAModificar.Id);
    Console.WriteLine("aqui");
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaAModificar.IdTablero });

  }
  
  [HttpPost]
  public IActionResult EditarTareaFromBody([FromBody] EditarTareaViewModel tareaEditadaVM)
  {

    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
    var tareaAModificar = new Tarea(tareaEditadaVM);
    Debug.WriteLine("{tareaAModificar.IdTablero}");
    tareaRepository.Update(tareaAModificar, tareaAModificar.Id);
    Console.WriteLine("aqui");
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaAModificar.IdTablero });

  }




  public IActionResult DeleteTarea(int idTarea)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    var tareaBuscado = tareaRepository.GetTareaById(idTarea);
    tareaRepository.RemoveTarea(idTarea);

    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaBuscado.IdTablero });
 
 
  }

  private bool isLogueado()
  {
    if (HttpContext.Session != null && (HttpContext.Session.GetInt32("NivelAcceso") == 1 || HttpContext.Session.GetInt32("NivelAcceso") == 2))
      return true;

    return false;
  }
}
