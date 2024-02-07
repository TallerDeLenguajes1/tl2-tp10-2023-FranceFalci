const draggables = document.querySelectorAll(".tarea");
const droppables = document.querySelectorAll(".contenedor-tareas");
let estadoTemporal;
const cambiarEstadoTarea = async (id,nombre,descripcion,estado, idTablero , asignado) => {
  console.log(id, nombre, descripcion, estado,  idTablero)
  estadoAInt = convertirEstado(estado);
  try {
    const response = await fetch('/Tarea/EditarTareaFromBody', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      // Puedes enviar datos en el cuerpo de la solicitud si es necesario
      body: JSON.stringify({
        IdUsuarioAsignado: asignado,
        IdTablero: idTablero,
        Id: id,
        Nombre: nombre,
        Descripcion: descripcion,
        Estado: estadoAInt
        }
      ),
    });
    if (!response.ok) {
      const errorMessage = await response.text();
      console.error('Error en la respuesta del servidor:', errorMessage);
    }

  } catch (error) {
    console.error('Error al ejecutar el método:', error);
  }
}; 

  const convertirEstado = (estado) => {
    switch (estado) {
      case "Ideas":
        return 0;
      case "ToDo":
        return 1;
      case "Doing":
        return 2;
      case "Review":
        return 3;
      case "Done":
        return 4;
      default:
        return "Estado desconocido";
    }
  };

// draggables.forEach((tarea) => {
//   const { id, nombre, descripcion, estado, idTablero } = tarea.dataset;

//   tarea.addEventListener("dragstart", (e) => {
//     // Guarda la información en el objeto de datos de la transferencia para que esté disponible en dragend
//     e.dataTransfer.setData("text/plain", JSON.stringify({ id, nombre, descripcion, estado, idTablero }));

//     tarea.classList.add("is-dragging");
//   });

//   tarea.addEventListener("dragend", () => {
//     tarea.classList.remove("is-dragging");
//     // Recupera la información desde la transferencia de datos
//     const data = JSON.parse(e.dataTransfer.getData("text/plain"));
//     cambiarEstadoTarea(data.id, data.nombre, data.descripcion, estadoTemporal, data.idTablero);
//     // location.reload();
//   });
// });


draggables.forEach((tarea) => {
  const { id, nombre, descripcion ,estado  , tablero, asignado} = tarea.dataset;

  tarea.addEventListener("dragstart", () => {
    // console.log( tablero , asignado)

    tarea.classList.add("is-dragging");
  });
  tarea.addEventListener("dragend", () => {
    tarea.classList.remove("is-dragging");
    cambiarEstadoTarea(id, nombre, descripcion, estadoTemporal, tablero , asignado);
  

  });
});

droppables.forEach((zona) => {
  zona.addEventListener("dragover", (e) => {
    e.preventDefault();

    // const bottomTask = insertAboveTask(zona, e.clientY);
    const tareaActual = document.querySelector(".is-dragging");

    const estadoColumna = zona.dataset.estado;
    tareaActual.dataset.estado = estadoColumna;
    // console.log("estado columna : " , estadoColumna)
    estadoTemporal = estadoColumna;

    zona.appendChild(tareaActual);
  
  });
});


