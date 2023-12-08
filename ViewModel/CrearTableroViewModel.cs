using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_FranceFalci;

public class CrearTableroViewModel
{
  // public int Idusuario { get; set; }
  // public int IdUsuario { get ; set; }
  
  [Required(ErrorMessage = "Este campo es requerido")]
  public string Nombre { get ; set ; }
  [Required(ErrorMessage = "Este campo es requerido")]
  public string Descripcion { get ; set ;} 
}