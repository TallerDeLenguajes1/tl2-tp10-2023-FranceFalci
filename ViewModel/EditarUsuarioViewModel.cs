namespace tl2_tp10_2023_FranceFalci;
using System.ComponentModel.DataAnnotations;

public class EditarUsuarioViewModel
{
  public int Id { get; set; }
  [Required(ErrorMessage = "Este campo es requerido")]
  [Display(Name = "Nombre de usuario")]
  public string NombreUsuario { get; set; }

  [Required(ErrorMessage = "Este campo es requerido")]
  public rol Rol { get; set; }

  public EditarUsuarioViewModel(){

  }
  public EditarUsuarioViewModel(Usuario usuario )
  {
    Id = usuario.Id;
    NombreUsuario = usuario.NombreUsuario;
    Rol = usuario.Rol;
  }
}

