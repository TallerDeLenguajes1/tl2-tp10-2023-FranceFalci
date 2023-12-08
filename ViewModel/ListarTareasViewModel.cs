namespace tl2_tp10_2023_FranceFalci;

public class ListarTareasViewModel
{
  //public List<ElementoIndexProductoViewModel> ProductoViewModels{get;set;}

  public List<IndexTareaViewModel> GetIndexTareaViewModel(List<Tarea> tareas)
  {
    var tareasViewModel = new List<IndexTareaViewModel>();
    foreach (var tarea in tareas)
    {
      tareasViewModel.Add(new IndexTareaViewModel
      {
        Id = tarea.Id,
        Nombre = tarea.Nombre,
        Descripcion = tarea.Descripcion,
        Estado = tarea.Estado,
        IdUsuarioPropietario  = tarea.IdUsuarioPropietario,
        IdTablero = tarea.IdTablero,
      });
    }

    return tareasViewModel;
  }
}