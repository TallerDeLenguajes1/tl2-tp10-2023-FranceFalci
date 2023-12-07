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
    TempData["IdTablero"] = idTablero;

    var tareas = tareaRepository.GetTareasPorTablero(idTablero);
    return View(tareas);
  }

  [HttpGet]
  public IActionResult CrearTareaForm()
  {
    return View(new Tarea());
  }

  [HttpPost]
  public IActionResult CrearTarea(Tarea tarea)
  {
    var idTablero = TempData["IdTablero"] as int? ?? 0; 

    tareaRepository.Create(tarea, idTablero);
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = idTablero });
  }

  [HttpPost]
  public IActionResult EditarTarea(Tarea tarea)
  {
    var tareaAModificar = tareaRepository.GetTareaById(tarea.Id);
    tareaAModificar.Descripcion = tarea.Descripcion;
    tareaAModificar.Nombre = tarea.Nombre;
    tareaAModificar.Estado = tarea.Estado;
    tareaRepository.Update(tareaAModificar, tareaAModificar.Id);
    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaAModificar.IdTablero });
    // return RedirectToAction("GetTareas");

  }

  [HttpGet]
  public IActionResult EditarTarea(int idTarea)
  {
    var tareaBuscado = tareaRepository.GetTareaById(idTarea);
    return View(tareaBuscado);
  }

  public IActionResult DeleteTarea(int idTarea)
  {
    var tareaBuscado = tareaRepository.GetTareaById(idTarea);
    tareaRepository.RemoveTarea(idTarea);

    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaBuscado.IdTablero });
  }
}