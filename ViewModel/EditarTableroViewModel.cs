using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_FranceFalci;

public class EditarTableroViewModel
{
  // public int Idusuario { get; set; }
  public int IdTablero { get ; set; }

  [Required(ErrorMessage = "Este campo es requerido")]
  public string Nombre { get; set; }
  [Required(ErrorMessage = "Este campo es requerido")]
  public string Descripcion { get; set; }

  public EditarTableroViewModel(){}

  public EditarTableroViewModel(Tablero tablero) { 
    Nombre = tablero.Nombre;
    Descripcion = tablero.Descripcion;
    IdTablero = tablero.IdTablero;
  }

}