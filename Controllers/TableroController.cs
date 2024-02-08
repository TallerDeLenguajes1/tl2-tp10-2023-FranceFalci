using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_FranceFalci.Models;
using System.Text.Json;

namespace tl2_tp10_2023_FranceFalci.Controllers;

public class TableroController : BaseController
{
  private readonly ITableroRepository tableroRepository;

  private readonly ILogger<HomeController> logger;

  public TableroController(ILogger<HomeController> logger, ITableroRepository tableroRepository)
  {
    this.logger = logger;
    this.tableroRepository = tableroRepository;

  }


  [HttpGet]
  [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]

  public IActionResult GetTableros()
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    
    try{

      int idUsuario = HttpContext.Session.GetInt32("ID") ?? -1;
      var tablerosPropios = tableroRepository.GetTableroByIdUsuario(idUsuario);

    if(isAdmin()){

      var tablerosGeneral = tableroRepository.GetTableros();
      var tablerosAjenos = tableroRepository.GetTablerosAjenos(idUsuario);
      return View(new ListarTablerosViewModel(tablerosPropios, tablerosAjenos));

    }

    if(isOperador()){

      var tablerosAjenos = tableroRepository.GetTablerosOperario(idUsuario);
      var viewModel = new ListarTablerosViewModel(tablerosPropios, tablerosAjenos);
      return View("GetTablerosOperario", viewModel);
    }
    }catch(Exception ex){
      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action = "Error" });
    }

    return RedirectToRoute(new { controller = "Login", action = "Index" });
  }


  [HttpGet]
  public IActionResult CrearTablero()
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    return View(new CrearTableroViewModel());
  }

  [HttpPost]
  public IActionResult CrearTablero(CrearTableroViewModel tableroCreadoVM)
  {
    if (!isLogueado()) {
        SweetAlert("Error al crear tablero. Debes estar logueado!", NotificationType.Error, "Oops..");
        return RedirectToRoute(new { controller = "Login", action = "Index" });
    }
    if (!ModelState.IsValid) return RedirectToAction("CrearUsuario");
    
    try{
      var tablero = new Tablero(tableroCreadoVM);
      tablero.IdUsuario = (int)HttpContext.Session.GetInt32("ID");
      tableroRepository.Create(tablero);

      SweetAlert("Tablero creado con exito" , NotificationType.Success, "Genial!");

    }catch (Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al crear tablero", NotificationType.Error, "Oops..");
    }

    return RedirectToAction("GetTableros");
  }

  [HttpGet]
  public IActionResult EditarTablero(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
    try{
    var tableroAEditar = tableroRepository.GetTableroById(idTablero);
    return View(new EditarTableroViewModel(tableroAEditar));
    }
    catch (Exception ex)
    {
      logger.LogError(ex.ToString());
      return RedirectToRoute(new { controller = "Home", action = "Error" });
    }
  }

  [HttpPost]
  public IActionResult EditarTablero(EditarTableroViewModel tableroEditadoVM)
  {
    if (!isLogueado()) {
      SweetAlert("Error al editar tablero. Debes estar logueado!", NotificationType.Error, "Oops..");
      return RedirectToRoute(new { controller = "Login", action = "Index" });
    }
    if (!ModelState.IsValid) return RedirectToAction("EditarTablero", new { idTablero = tableroEditadoVM.IdTablero });

    try{
      var tableroAModificar = new Tablero(tableroEditadoVM);
      tableroRepository.Update(tableroAModificar, tableroAModificar.IdTablero);
      SweetAlert("Tablero editado con éxito.", NotificationType.Success, "Genial!");

    }
    catch (Exception ex)
    {
      logger.LogError(ex.ToString());
      SweetAlert("Error al editar tablero.", NotificationType.Error, "Oops..");

    }

    return RedirectToAction("GetTableros");
  }
  public IActionResult EliminarTablero(int idTablero)
  {
    if (!isLogueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

    try{
      tableroRepository.RemoveTablero(idTablero);
      SweetAlert("Tablero eliminado con éxito.", NotificationType.Success, "Genial!");

    }
    catch (Exception ex){
      logger.LogError(ex.ToString());
      SweetAlert("Error al eliminar tablero.", NotificationType.Error, "Oops..");

    }
    return RedirectToAction("GetTableros");
  }

  private bool isAdmin()
  {
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