namespace tl2_tp10_2023_FranceFalci;

  public class ListarUsuariosViewModel
  {
    //public List<ElementoIndexProductoViewModel> ProductoViewModels{get;set;}

    public List<IndexUsuarioViewModel> GetIndexUsuariosViewModel(List<Usuario> usuarios)
    {
      var usuariosViewModel = new List<IndexUsuarioViewModel>();
    foreach (var usuario in usuarios)
    {
      usuariosViewModel.Add(new IndexUsuarioViewModel
      {
        NombreUsuario = usuario.NombreUsuario,
        Id = usuario.Id,
        Rol = usuario.Rol
      });
    }

    return usuariosViewModel;
    }
}


// using PruebaMVC.Models;
// namespace PruebaMVC.ViewModels
// {
//   public class IndexProductoViewModel
//   {
//     //public List<ElementoIndexProductoViewModel> ProductoViewModels{get;set;}

//     public List<ElementoIndexProductoViewModel> GEtIndexProductoViewModel(List<Producto> productos)
//     {
//       var ProductoViewModels = new List<ElementoIndexProductoViewModel>();
//       foreach (var producto in productos)
//       {
//         ProductoViewModels.Add(new ElementoIndexProductoViewModel
//         {
//           Id = producto.Id,
//           Nombre = producto.Nombre,
//           Precio = producto.Precio
//         });
//       }
//       return ProductoViewModels;
//     }
//   }
// }