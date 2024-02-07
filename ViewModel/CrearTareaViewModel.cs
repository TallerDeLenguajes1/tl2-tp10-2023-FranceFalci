using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_FranceFalci;
public class CrearTareaViewModel
{
  public List<Usuario> usuarios;
  public int IdUsuarioAsignado { get; set; }
  public int IdTablero { get; set; }

  [Required(ErrorMessage = "Este campo es requerido")]
  public string Nombre { get; set; }
  // public string Color { get ; set ; }
  [Required(ErrorMessage = "Este campo es requerido")]
  public string Descripcion { get; set; }
  public EstadoTarea Estado { get; set; }

  public CrearTareaViewModel(int IdTablero , List<Usuario> usuarios)
  {
    this.IdTablero = IdTablero;
    this.usuarios = usuarios;

  }
  public CrearTareaViewModel(){}
}