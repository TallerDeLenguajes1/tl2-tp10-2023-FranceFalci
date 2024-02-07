using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_FranceFalci
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Este campo es requerido.")]
    // [Display(Name = "Nombre de Usuario")]
    public string NombreUsuario { get; set; }

    [Required(ErrorMessage = "Este campo es requerido.")]
    [PasswordPropertyText]
    // [Display(Name = "Contraseña")]
    public string Contrasenia { get; set; }
  }
}