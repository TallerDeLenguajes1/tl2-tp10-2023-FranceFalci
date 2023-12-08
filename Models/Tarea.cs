namespace tl2_tp10_2023_FranceFalci;

public class Tarea{
  int id;
  int idUsuarioPropietario;
  int idTablero;

  string nombre;
  string color;
  string descripcion;
  EstadoTarea estado;

  public int Id { get => id; set => id = value; }
  public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
  public int IdTablero { get => idTablero; set => idTablero = value; }
  public string Nombre { get => nombre; set => nombre = value; }
  public string Color { get => color; set => color = value; }
  public string Descripcion { get => descripcion; set => descripcion = value; }
  public EstadoTarea Estado { get => estado; set => estado = value; }

  public Tarea(){}
  public Tarea(CrearTareaViewModel tareaCreadaVM) {
    IdTablero = tareaCreadaVM.IdTablero;
    Nombre = tareaCreadaVM.Nombre;
    Descripcion = tareaCreadaVM.Descripcion;
    Estado = tareaCreadaVM.Estado;
    IdUsuarioPropietario = tareaCreadaVM.IdUsuarioPropietario;
   }
  public Tarea(EditarTareaViewModel tareaEditadaVM)
  {
    IdTablero = tareaEditadaVM.IdTablero;
    Nombre = tareaEditadaVM.Nombre;
    Descripcion = tareaEditadaVM.Descripcion;
    Estado = tareaEditadaVM.Estado;
    IdUsuarioPropietario = tareaEditadaVM.IdUsuarioPropietario;
    Id = tareaEditadaVM.Id;
  }

}

public enum EstadoTarea{
  Ideas, 
  ToDo,
 
  Doing, 
  Review, 
  Done
}