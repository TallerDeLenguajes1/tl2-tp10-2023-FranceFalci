using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
namespace tl2_tp10_2023_FranceFalci.Controllers;

public class TareaController : BaseController
{
  // private ITareaRepository tareaRepository;
  private readonly ITareaRepository tareaRepository;
  private readonly IUsuarioRepository usuarioRepository;
  private readonly ITableroRepository tableroRepository;



  private readonly ILogger<HomeController> logger;

  public TareaController(ILogger<HomeController> logger,ITareaRepository tareaRepository,IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository)
  {
    this.logger = logger;
    this.tareaRepository = tareaRepository;
    this.usuarioRepository = usuarioRepository;
    this.tableroRepository = tableroRepository;

  }


  [HttpGet]
  [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
  public IActionResult GetTareasByIdTablero(int idTablero)
  {
    
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    var usuarioPropietario = tableroRepository.GetPropietarioByIdTablero(idTablero);
    if(!esPropietario(usuarioPropietario) &&  !isAdmin()) {
      try{

      var tareas = tareaRepository.GetTareasPorTablero(idTablero);
      var usuarios = usuarioRepository.GetAll();
      var viewModel = new ListarTareasViewModel().GetIndexTareaViewModel(tareas,usuarios);
      return View("GetTareasOperario", viewModel);

      }catch(Exception ex){

        logger.LogError(ex.ToString());
        return RedirectToRoute(new { controller = "Home", action = "Error" });

      }
    }

    try{
      var tareas = tareaRepository.GetTareasPorTablero(idTablero);
      var usuarios = usuarioRepository.GetAll();
      return View( new ListarTareasViewModel().GetIndexTareaViewModel(tareas,usuarios));

    }catch(Exception ex){
      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action = "Error" });
    }
  }

  [HttpGet]

  public IActionResult CrearTarea(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    try{

      var usuarios = usuarioRepository.GetAll();
      return View(new CrearTareaViewModel(idTablero,usuarios));

    }catch (Exception ex){

      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action = "Error" });
    }
  }

  [HttpPost]
  public IActionResult CrearTarea(CrearTareaViewModel tareaCreadaVM)
  {
    if (!isLogueado()){
      SweetAlert("Error al crear tarea. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    } 
    if (!ModelState.IsValid) return RedirectToAction("CrearTarea");
    
    
    var tarea = new Tarea(tareaCreadaVM);
    try{
      tareaRepository.Create(tarea, tarea.IdTablero);
      SweetAlert("Tarea creada con exito", NotificationType.Success, "Genial!");

    }
    catch (Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al crear tablero", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Home", action = "Error" });

    }

    return RedirectToAction("GetTareasByIdTablero", new { idTablero = tarea.IdTablero });
  }



  [HttpGet]
  public IActionResult EditarTarea(int idTarea)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    try{

      var tareaBuscada = tareaRepository.GetTareaById(idTarea);
      var usuarios =  usuarioRepository.GetAll();
      var vm = new EditarTareaViewModel(tareaBuscada, usuarios);
      return View(vm);

    }catch(Exception ex){

      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action = "Error" });

    }
  }

  [HttpPost]
  public IActionResult EditarTarea(EditarTareaViewModel tareaEditadaVM)
  {
    if (!isLogueado()) {
      SweetAlert("Error al editar tarea. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    }

    if (!ModelState.IsValid) return RedirectToAction("EditarTarea");

    try{

      var tareaAModificar = new Tarea(tareaEditadaVM);
      tareaRepository.Update(tareaAModificar, tareaAModificar.Id);
      SweetAlert("Tarea editada con exito", NotificationType.Success, "Genial!");
      return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaAModificar.IdTablero });

    }catch (Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al editar tarea", NotificationType.Error, "Oops..");
      return RedirectToAction("EditarTarea");
    }

  }

  [HttpGet]
  public IActionResult EditarTareaAsignada(int idTarea, int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    if (isAdmin()) return RedirectToRoute(new { controller = "Home", action = "Error" });
    try
    {

      var tareaBuscada = tareaRepository.GetTareaById(idTarea);
      if (!isTareaAsignada(tareaBuscada.IdUsuarioAsignado)){
        SweetAlert("No tienes permiso para editar esta tarea", NotificationType.Error, "Oops..");
        return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaBuscada.IdTablero });
      }
      var usuarios = usuarioRepository.GetAll(); 
      
      var vm = new EditarTareaViewModel(tareaBuscada, usuarios);
      return View("EditarTareaAsignada", vm);
    }
    catch (Exception ex)
    {

      logger.LogError(ex.ToString());

    }
      return RedirectToRoute(new { controller = "Home", action = "Error" });
  }

  [HttpPost]
  public IActionResult EditarTareaFromBody([FromBody] EditarTareaViewModel tareaEditadaVM)
  {

    if (!isLogueado()){
      SweetAlert("Error al mover tarea. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    }


    if (!ModelState.IsValid) return RedirectToAction("EditarTarea");
    var usuarioPropietario = tableroRepository.GetPropietarioByIdTablero(tareaEditadaVM.IdTablero);

    if (!isAdmin() && !isTareaAsignada(tareaEditadaVM.IdUsuarioAsignado) && !esPropietario(usuarioPropietario))
    {
      SweetAlert("No tienes permiso para editar esta tarea", NotificationType.Error, "Oops..");
      return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaEditadaVM.IdTablero });
    }

    try{
      
      var tareaAModificar = new Tarea(tareaEditadaVM);

      tareaRepository.Update(tareaAModificar, tareaAModificar.Id);

      return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaAModificar.IdTablero });

    }catch (Exception ex){
      
      SweetAlert("Error al mover tarea", NotificationType.Error, "Oops..");
      logger.LogError(ex.ToString());
      return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaEditadaVM.IdTablero });

    }

  }




  public IActionResult EliminarTarea(int idTarea)
  {
    if (!isLogueado()){
      SweetAlert("Error al eliminar tarea. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    } 
    try{

      var tareaBuscado = tareaRepository.GetTareaById(idTarea);
      tareaRepository.RemoveTarea(idTarea);
      SweetAlert("Tarea eliminada con exito", NotificationType.Success, "Genial!");

      return RedirectToAction("GetTareasByIdTablero", new { idTablero = tareaBuscado.IdTablero });

    }catch(Exception ex){
      SweetAlert("Error al eliminar tarea", NotificationType.Error, "Oops..");
      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action = "Error" });

    }




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

  private bool esPropietario( int idUsuarioPropietario)
  {
    if (HttpContext.Session != null && idUsuarioPropietario == HttpContext.Session.GetInt32("ID"))
      return true;

    return false;
  }

  private bool isTareaAsignada(int idUsuarioAsignado){
    // var tarea
    if (HttpContext.Session != null && idUsuarioAsignado == HttpContext.Session.GetInt32("ID"))
      return true;

    return false;
  }
}
