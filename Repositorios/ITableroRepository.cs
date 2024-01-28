namespace tl2_tp10_2023_FranceFalci;
public interface ITableroRepository
{
  public void Create(Tablero tablero);
  public void Update(Tablero tablero, int idTablero);
  public Tablero GetTableroById(int idTablero);
  public List<Tablero> GetTableros();
  public List<Tablero> GetTableroByIdUsuario(int idUsuario);
  public List<Tablero> GetTablerosOperario(int idUsuario);
  public List<Tablero> GetTablerosAjenos(int idUsuario);
  


  public void RemoveTablero(int idTablero);




}


// Crear un repositorio llamado TableroRepository para gestionar todas las operaciones
// relacionadas con tableros.Este repositorio debe incluir métodos para:
// ● Crear un nuevo tablero(devuelve un objeto Tablero)
// ● Modificar un tablero existente(recibe un id y un objeto Tablero)
// ● Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero)
// ● Listar todos los tableros existentes(devuelve un list de tableros)
// ● Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un
// list de tableros)
// ● Eliminar un tablero por ID

// public void Create(Tarea tarea, int idTablero);
// public void Update(Tarea tarea, int idTarea);
// public Tarea GetTareaById(int id);
// public List<Tarea> GetTareasPorUsuario(int idUsuario);
// public List<Tarea> GetTareasPorTablero(int idTablero);
// public void RemoveTarea(int idTarea);
// public void AsignarTareaUsuario(int idUsuario, int idTarea);