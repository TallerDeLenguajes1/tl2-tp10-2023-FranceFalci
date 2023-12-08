namespace tl2_tp10_2023_FranceFalci;

public class Tablero {
  int idTablero;
  int idUsuario;
  string nombre ;
  string descripcion;

    public int IdTablero { get => idTablero; set => idTablero = value; }
    public int IdUsuario { get => idUsuario; set => idUsuario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

public Tablero(){}
    public Tablero (CrearTableroViewModel tableroCreadoVM) {
      Nombre = tableroCreadoVM.Nombre;
      Descripcion = tableroCreadoVM.Descripcion;
    }
  public Tablero(EditarTableroViewModel tableroEditadoVM)
  {
    Nombre = tableroEditadoVM.Nombre;
    Descripcion = tableroEditadoVM.Descripcion;
    IdTablero = tableroEditadoVM.IdTablero;
  }
}