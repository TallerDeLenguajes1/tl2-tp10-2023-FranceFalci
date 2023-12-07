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
}

public enum EstadoTarea{
  Ideas, 
  ToDo,
 
  Doing, 
  Review, 
  Done
}