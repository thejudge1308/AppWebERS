// Ejecuta cuando se haya cargado la pagina completa
$(document).ready(() => {
    let alerta = $('#alert');
    cerrarAlertaAutomaticamente(); //Si hay una alerta la ocultara 

    /**
     * Autor: Gabriel Sanhueza
     * Agrega un evento cuando se hace click en los botones con clase deshabilitar para que 
     * muestre la alerta
     * Parametros: 
     * event: objeto con informacion del evento
     */
    $('.deshabilitar').on('click', (event) => {
        alerta.toggleClass(function () {
            console.log($(this));
            if ($(this).hasClass('d-none')) {

                $(this).addClass('d-flex').removeClass('d-none');

                //Activa el timer para cerrar automaticamente la alerta al cabo de 2 segundos
                cerrarAlertaAutomaticamente();
            }
            return $(this);
        })
        event.preventDefault(); //Se arreglo por mientras, despues hay que quitarlo y configurar desde el servidor la muestra de alerta
    });

    /**
     * Autor: Gabriel Sanhueza
     * Llama a una funcion asincrona para que cierre la alerta a los 2 segundos
     * 
     */
    function cerrarAlertaAutomaticamente() {
        //Realiza una tarea asincrona que ocultara la alerta al cabo de 2 segundos
        setTimeout(function () {
            ocultarAlerta();
        }, 2000);
    }


     /**
     * Autor: Gabriel Sanhueza
     * Oculta la alerta y usa un efecto de jquery mientras se oculta
     */
    function ocultarAlerta() {
        if (!$(this).hasClass('d-none')) {
            alerta.slideUp("slow", function () {
                alerta.toggleClass(function () {
                        //Oculta la alert agregando la clase d-none 
                        $(this).addClass('d-none').removeClass('d-flex');
                    return $(this);
                });
                //Remueve el estilo dado por el efecto jquery
                alerta.removeAttr('style');
            });
        }
    }

    /**
     * Autor: Gabriel Sanhueza
     * Evento sobre la alerta para ocultarla
     */
    $('#button-alert').on('click', () => {
        ocultarAlerta();
    })

    /**
     * Autor: Gabriel Sanhueza
     * Agrega eventos sobre los botones modificar que abren la ventana modal y rellena esta ventana con los datos
     * Parametros:
     * event: objeto con informacion del evento
     */    $("[data-target='#exampleModal']").on('click', (event) => {
        $("#nombreModal").text(event.target.dataset.nombre);
    })
});