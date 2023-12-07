namespace tl2_tp10_2023_FranceFalci;
public class Usuario {
  int id;
  string nombreUsuario;
  string contrasenia;
  rol rol;


    public int Id { get => id; set => id = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public rol Rol { get => rol; set => rol = value; }
}

public enum rol{
  operador, 
  admin 
}