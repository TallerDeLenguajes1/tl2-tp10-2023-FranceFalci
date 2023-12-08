using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_FranceFalci;
public class CrearTareaViewModel
{
  public int IdUsuarioPropietario { get; set; }
  public int IdTablero { get; set; }

  [Required(ErrorMessage = "Este campo es requerido")]
  public string Nombre { get; set; }
  // public string Color { get ; set ; }
  [Required(ErrorMessage = "Este campo es requerido")]
  public string Descripcion { get; set; }
  public EstadoTarea Estado { get; set; }

  public CrearTareaViewModel(int IdTablero){
    this.IdTablero = IdTablero;
  }
  public CrearTareaViewModel(){}
}