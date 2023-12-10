const draggables = document.querySelectorAll(".tarea");
const droppables = document.querySelectorAll(".contenedor-tareas");
let estadoTemporal;
const cambiarEstadoTarea = async (id,nombre,descripcion,estado) => {
  console.log(id, nombre, descripcion, estado)
  estadoAInt = convertirEstado(estado);
  try {
    const response = await fetch('/Tarea/EditarTareaFromBody', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      // Puedes enviar datos en el cuerpo de la solicitud si es necesario
      body: JSON.stringify({
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


draggables.forEach((tarea) => {
  const { id, nombre, descripcion ,estado} = tarea.dataset;

  tarea.addEventListener("dragstart", () => {
    tarea.classList.add("is-dragging");
  });
  tarea.addEventListener("dragend", () => {
    tarea.classList.remove("is-dragging");
    console.log("ejecutando");
    cambiarEstadoTarea(id,nombre,descripcion,estadoTemporal);
    // location.reload();


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


