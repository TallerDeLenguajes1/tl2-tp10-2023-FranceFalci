using Microsoft.AspNetCore.Mvc;
namespace tl2_tp10_2023_FranceFalci;




  public enum NotificationType
  {
    Success,
    Error,
    Info
  }

  
  public class BaseController : Controller
  {

    public void SweetAlert(string msj, NotificationType type, string title = "")
    {
      TempData["notification"] = $"Swal.fire('{title}','{msj}', '{type.ToString().ToLower()}')";
    }
   
  }
