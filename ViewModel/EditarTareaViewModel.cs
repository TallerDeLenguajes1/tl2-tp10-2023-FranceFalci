using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_FranceFalci;
public class EditarTareaViewModel
{
  public List<Usuario> usuarios;
  public int Id { get; set; }
  public int IdUsuarioAsignado { get; set; }
  public int IdTablero { get; set; }

  [Required(ErrorMessage = "Este campo es requerido")]
  public string Nombre { get; set; }
  // public string Color { get ; set ; }
  [Required(ErrorMessage = "Este campo es requerido")]
  public string Descripcion { get; set; }
  public EstadoTarea Estado { get; set; }

  public EditarTareaViewModel(Tarea tarea , List<Usuario> usuarios)
  {
    this.usuarios = usuarios;
    IdTablero =tarea.IdTablero;
    IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    Id = tarea.Id;
    Nombre = tarea.Nombre;
    Estado = tarea.Estado;
    Descripcion = tarea.Descripcion;
  }
  public EditarTareaViewModel() { }
}