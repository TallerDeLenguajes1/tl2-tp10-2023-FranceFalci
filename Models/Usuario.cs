namespace tl2_tp10_2023_FranceFalci;

public enum rol
{

  operador = 1,
  admin= 2
}
public class Usuario {
  int id;
  string nombreUsuario;
  string contrasenia;
  rol rol;


    public int Id { get => id; set => id = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public rol Rol { get => rol; set => rol = value; }

public Usuario(){}
  public Usuario(EditarUsuarioViewModel usuarioEditadoVM)
  {
    Id = usuarioEditadoVM.Id;
    NombreUsuario = usuarioEditadoVM.NombreUsuario;
    Rol = usuarioEditadoVM.Rol;
  }

  public Usuario(CrearUsuarioViewModel usuarioCreadoVM){
    Id = usuarioCreadoVM.Id;
    NombreUsuario = usuarioCreadoVM.NombreUsuario;
    Rol = usuarioCreadoVM.Rol;
    Contrasenia = usuarioCreadoVM.Contrasenia;
  }
}
