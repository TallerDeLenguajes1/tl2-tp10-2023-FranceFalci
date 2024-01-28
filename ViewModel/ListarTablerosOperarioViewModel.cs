namespace tl2_tp10_2023_FranceFalci
{
  public class ListarTablerosViewModel
  {
    public List<IndexTableroViewModel> TablerosPropiosViewModel { get; set; }
    public List<IndexTableroViewModel> TablerosAjenosViewModel { get; set; }

    public ListarTablerosViewModel(List<Tablero> tablerosPropios, List<Tablero> tablerosAjenos)
    {
      TablerosPropiosViewModel = tablerosPropios.Select(tablero => new IndexTableroViewModel
      {
        IdTablero = tablero.IdTablero,
        Nombre = tablero.Nombre,
        Descripcion = tablero.Descripcion,
        IdUsuario = tablero.IdUsuario,
      }).ToList();

      TablerosAjenosViewModel = tablerosAjenos.Select(tablero => new IndexTableroViewModel
      {
        IdTablero = tablero.IdTablero,
        Nombre = tablero.Nombre,
        Descripcion = tablero.Descripcion,
        IdUsuario = tablero.IdUsuario,
      }).ToList();
    }
  }
}


// namespace tl2_tp10_2023_FranceFalci;

// public class ListarTablerosViewModel
// {

//   public List<IndexTableroViewModel> GetIndexTableroViewModel(List<Tablero> tablerosPropios , List<Tablero> tablerosAsignados )
//   {
//     var tablerosPropiodViewModel = new List<IndexTableroViewModel>();
//     foreach (var tablero in tablerosPropios)
//     {
//       tablerosPropiodViewModel.Add(new IndexTableroViewModel
//       {
//         IdTablero = tablero.IdTablero,
//         Nombre = tablero.Nombre,
//         Descripcion = tablero.Descripcion,
//         IdUsuario = tablero.IdUsuario,
//       });
//     }

//     var tablerosAsignadosViewModel = new List<IndexTableroViewModel>();
//     foreach (var tablero in tablerosAsignados)
//     {
//       tablerosAsignadosViewModel.Add(new IndexTableroViewModel
//       {
//         IdTablero = tablero.IdTablero,
//         Nombre = tablero.Nombre,
//         Descripcion = tablero.Descripcion,
//         IdUsuario = tablero.IdUsuario,
//       });
//     }

//     return tablerosPropiodViewModel;
//   }
// }