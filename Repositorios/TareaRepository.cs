using System.Data.SQLite;
namespace tl2_tp10_2023_FranceFalci;

public class TareaRepository : ITareaRepository{
  private string cadenaConexion = "Data Source=BD/kanban.db;Cache=Shared";

  public void Create(Tarea tarea, int idTablero){
    var query = $"INSERT INTO Tarea (id_tablero, nombre, estado,descripcion,color) VALUES (@idTablero, @nombre, @estado,@descripcion,@color)";
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      connection.Open();
      var command = new SQLiteCommand(query, connection);

      command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
      command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
      command.Parameters.Add(new SQLiteParameter("@estado", (int)tarea.Estado));
      command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
      command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));


      command.ExecuteNonQuery();

      connection.Close();
    }
  }
  public void Update(Tarea tarea, int idTarea){
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      connection.Open();
      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = $"UPDATE tarea SET nombre=@nombre, estado = @estado, descripcion =@descripcion  WHERE id_tarea= @idTarea;";

      command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
      command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
      command.Parameters.Add(new SQLiteParameter("@estado", (int)tarea.Estado));
      command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
      command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));


      command.ExecuteNonQuery();
      connection.Close();
    }
  }
  public Tarea GetTareaById(int id){

    SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
    var tarea = new Tarea();
    SQLiteCommand command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM tarea WHERE id_tarea= @idTarea";
    command.Parameters.Add(new SQLiteParameter("@idTarea", id));
    connection.Open();

    using (SQLiteDataReader reader = command.ExecuteReader())
    {
      while (reader.Read())
      {
        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
        tarea.IdUsuarioPropietario = reader["id_usuario_asignado"] != DBNull.Value
            ? Convert.ToInt32(reader["id_usuario_asignado"])
            : 0; tarea.Nombre = reader["nombre"].ToString();
        tarea.Descripcion = reader["descripcion"].ToString();
        tarea.Color = reader["color"].ToString();
        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
      }
    }
    connection.Close();

    return (tarea);
  }
  public List<Tarea> GetTareasPorUsuario(int idUsuario){
    var queryString = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuarioAsig;";
    List<Tarea> tareas = new List<Tarea>();
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {
      SQLiteCommand command = new SQLiteCommand(queryString, connection);
      command.Parameters.Add(new SQLiteParameter("@idUsuarioAsig", idUsuario));

      connection.Open();

      using (SQLiteDataReader reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var tarea = new Tarea();
          tarea.Id = Convert.ToInt32(reader["id_tarea"]);
          tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
          // tarea.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_asignado"]);
          tarea.Nombre = reader["nombre"].ToString();
          tarea.Descripcion = reader["descripcion"].ToString();
          tarea.Color = reader["color"].ToString();
          tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
          tareas.Add(tarea);
        }
      }
      connection.Close();
    }
    return tareas;
  }
  public List<Tarea> GetTareasPorTablero(int idTablero){
    var queryString = @"SELECT * FROM tarea WHERE id_tablero = @idTab;";
    List<Tarea> tareas = new List<Tarea>();
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {
      SQLiteCommand command = new SQLiteCommand(queryString, connection);
      command.Parameters.Add(new SQLiteParameter("@idTab", idTablero));

      connection.Open();

      using (SQLiteDataReader reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var tarea = new Tarea();
          tarea.Id = Convert.ToInt32(reader["id_tarea"]);
          tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
          if(reader["id_usuario_asignado"] != DBNull.Value) tarea.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_asignado"]);
          tarea.Nombre = reader["nombre"].ToString();
          tarea.Descripcion = reader["descripcion"].ToString();
          tarea.Color = reader["color"].ToString();
          tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
          tareas.Add(tarea);
        }
      }
      connection.Close();
    }
    return tareas;
  }
  public void RemoveTarea(int idTarea){
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = $"DELETE FROM Tarea WHERE id_tarea = @idTarea;";
      command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();
    }
  }
  public void AsignarTareaUsuario(int idUsuario, int idTarea){
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      connection.Open();
      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = $"UPDATE tarea SET id_usuario_asignado=@idUsuarioAsig WHERE id_tarea= @idTarea;";

      command.Parameters.Add(new SQLiteParameter("@idUsuarioAsig", idUsuario));
      command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));


      command.ExecuteNonQuery();
      connection.Close();
    }
  }

}