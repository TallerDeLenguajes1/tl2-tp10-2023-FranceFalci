namespace tl2_tp10_2023_FranceFalci;
public interface ITareaRepository
{
  public void Create(Tarea tarea, int idTablero);
  public void Update(Tarea tarea, int idTarea);
  public Tarea GetTareaById(int id);
  public List<Tarea> GetTareasPorUsuario(int idUsuario);
  public List<Tarea> GetTareasPorTablero(int idTablero);
  public void RemoveTarea(int idTarea);
  public void AsignarTareaUsuario(int idUsuario, int idTarea);





}

// ● Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
// ● Modificar una tarea existente. (recibe un id y un objeto Tarea)
// ● Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
// ● Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario,
// devuelve un list de tareas)
// ● Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list
// de tareas)
// ● Eliminar una tarea(recibe un IdTarea)
// ● Asignar Usuario a Tarea(recibe idUsuario y un idTarea)

// public List<Usuario> GetAll();
// public Usuario GetById(int id);
// public void Create(Usuario usuario);
// public void Remove(int id);
// public void Update(Usuario usuario);
