using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class TareaController : Controller
{
  private ITareaRepository tareaRepository;

  private readonly ILogger<HomeController> _logger;

  public TareaController(ILogger<HomeController> logger)
  {
    _logger = logger;
    tareaRepository = new TareaRepository();

  }
  // GetTareasByIdTablero

  [HttpGet]
  public IActionResult GetTareas()
  {
    var tareas = tareaRepository.GetTareasPorTablero(1);
    return View(tareas);
  }

  [HttpGet]
  public IActionResult GetTareasByIdTablero(int idTablero)
  {
    // TempData["IdTablero"] = idTablero;

    var tareas = tareaRepository.GetTareasPorTablero(idTablero);
    return View( new ListarTareasViewModel().GetIndexTareaViewModel(tareas));
  }

  [HttpGet]
  public IActionResult CrearTareaForm(int idTablero)
  {
    return View(new CrearTareaViewModel(idTablero));
  }

  [HttpPost]
  public IActionResult CrearTarea(CrearTareaViewModel tareaCreadaVM)
  {
    // var idTablero = TempData["IdTablero"] as int? ?? 0; 
    var tarea = new Tarea(tareaCreadaVM);
    if (!ModelState.IsValid) return RedirectToAction("CrearTarea");
    tareaRepository.Create(tarea, tarea.IdTablero);
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tarea.IdTablero });
  }



  [HttpGet]
  public IActionResult EditarTarea(int idTarea)
  {

    var tareaBuscado = tareaRepository.GetTareaById(idTarea);
    return View(new EditarTareaViewModel(tareaBuscado));
  }

  [HttpPost]
  public IActionResult EditarTarea(EditarTareaViewModel tareaEditadaVM)
  {
    if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
    var tareaAModificar = new Tarea(tareaEditadaVM);
    Debug.WriteLine("{tareaAModificar.IdTablero}");
    tareaRepository.Update(tareaAModificar, tareaAModificar.Id);
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaAModificar.IdTablero });

  }


  public IActionResult DeleteTarea(int idTarea)
  {
    var tareaBuscado = tareaRepository.GetTareaById(idTarea);
    tareaRepository.RemoveTarea(idTarea);

    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaBuscado.IdTablero });
  }
}