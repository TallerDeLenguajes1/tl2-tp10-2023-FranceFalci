const alertar = () => {
  Swal.fire("SweetAlert2 is working!");
  // Esperas x segundos
  return false;
}


const confirmarEliminacion = (elemento = "", nombre="", urlEliminar) =>{
  
  Swal.fire({
    title: `Seguro que quiere eliminar ${elemento}: "${nombre}"?`,
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Eliminar",
    cancelButtonText:"Cancelar"
  })
  .then((result) => {
    if (result.isConfirmed) {
      window.location.href = urlEliminar;

    }
  });
  return false;
  
}

const confirmarEdicion = (e, elemento, nombre) => {
  e.preventDefault();

  Swal.fire({
    title: `Seguro que quiere editar ${elemento}: "${nombre}"?`,
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Editar",
    cancelButtonText: "Cancelar"
  })
    .then((result) => {
      if (result.isConfirmed) {
        // Envía el formulario si la confirmación es positiva
        e.target.submit();
      }
    });
};
