
namespace tl2_tp10_2023_FranceFalci;
public interface IUsuarioRepository
{
  public List<Usuario> GetAll();
  public Usuario GetById(int id);
  public void Create(Usuario usuario);
  public void Remove(int id);
  public void Update(Usuario usuario);
}