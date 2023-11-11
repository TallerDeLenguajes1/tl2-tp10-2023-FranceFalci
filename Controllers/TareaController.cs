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


  [HttpGet]
  public IActionResult GetTareas()
  {
    var tareas = tareaRepository.GetTareasPorUsuario(1);
    return View(tareas);
  }

}