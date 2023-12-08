using System.Data.SQLite;
namespace tl2_tp10_2023_FranceFalci;

public class TableroRepository : ITableroRepository {

  // private string cadenaConexion = "Data Source=BD/kanban.db;Cache=Shared";
  private readonly string cadenaConexion;

  public TableroRepository(string CadenaDeConexion)
  {
    cadenaConexion = CadenaDeConexion;
  }

  public void Create(Tablero tablero){
    var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre,descripcion) VALUES ( @idUsuarioPropietario, @nombre,@descripcion)";
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      connection.Open();
      var command = new SQLiteCommand(query, connection);

      command.Parameters.Add(new SQLiteParameter("@idUsuarioPropietario", tablero.IdUsuario));
      command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
      command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));


      command.ExecuteNonQuery();

      connection.Close();
    }
  }
  public void Update(Tablero tablero, int idTablero){
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      connection.Open();
      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = $"UPDATE tablero SET id_usuario_propietario= @idUsuario, nombre= @nombre,descripcion=@descripcion WHERE id_tablero = @idTablero;";
      command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
      command.Parameters.Add(new SQLiteParameter("@idUsuario",tablero.IdUsuario));
      command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
      command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

      command.ExecuteNonQuery();
      connection.Close();
    }
  }
  public Tablero GetTableroById(int idTablero){
    SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
    var tablero = new Tablero();
    SQLiteCommand command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM tablero WHERE id_tablero= @idTablero";
    command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
    connection.Open();

    using (SQLiteDataReader reader = command.ExecuteReader())
    {
      while (reader.Read())
      {
        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
        tablero.IdUsuario = Convert.ToInt32(reader["id_usuario_propietario"]);
        tablero.Descripcion = reader["descripcion"].ToString();
        tablero.Nombre = reader["nombre"].ToString();
      }
    }
    connection.Close();

    return (tablero);
  }
  public List<Tablero> GetTableros(){
    var queryString = @"SELECT * FROM tablero;";
    List<Tablero> tableros = new List<Tablero>();
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {
      SQLiteCommand command = new SQLiteCommand(queryString, connection);
      connection.Open();

      using (SQLiteDataReader reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var tablero = new Tablero();
          tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
          tablero.IdUsuario = Convert.ToInt32(reader["id_usuario_propietario"]);
          tablero.Descripcion = reader["descripcion"].ToString();
          tablero.Nombre = reader["nombre"].ToString();
          tableros.Add(tablero);

        }
      }
      connection.Close();
    }
    return tableros;

  }
  public List<Tablero> GetTableroByIdUsuario(int idUsuario){
    SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
    SQLiteCommand command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM tablero WHERE id_usuario_propietario= @idUsuario";
    command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
    connection.Open();
  var tableros = new List<Tablero>();
    using (SQLiteDataReader reader = command.ExecuteReader())
    {
      while (reader.Read())
      {
        var tablero = new Tablero();
        tablero.IdTablero = Convert.ToInt32(reader["id_tablero"]);
        tablero.IdUsuario = Convert.ToInt32(reader["id_usuario_propietario"]);
        tablero.Descripcion = reader["descripcion"].ToString();
        tablero.Nombre = reader["nombre"].ToString();
        tableros.Add(tablero);
      }
    }
    connection.Close();

    return (tableros);

  }

  public void RemoveTablero(int idTablero){
    using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
    {

      SQLiteCommand command = connection.CreateCommand();
      command.CommandText = $"DELETE FROM tablero WHERE id_tablero = @idTablero;";
      command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();
    }
  }
}