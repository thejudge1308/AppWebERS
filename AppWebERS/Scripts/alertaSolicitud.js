/*
 * NOTA: He utilizado el script realizado por Gabriel Sanhueza y le he hecho adaptaciones para las alertas
 * de las solicitudes
 * ATTE: Jose Nunnez
 */

// Ejecuta cuando se haya cargado la pagina completa
$(document).ready(() => {
    let alerta = $('#alert');
    let alerta2 = $('#alerta2');
    cerrarAlertaAutomaticamente(); //Si hay una alerta la ocultara 
    cerrarAlertaAutomaticamente2();
    /**
     * Autor: Gabriel Sanhueza
     * Agrega un evento cuando se hace click en los botones con clase boton para que 
     * muestre la alerta de rechazo
     * Parametros: 
     * event: objeto con informacion del evento
     */
    $('.boton').on('click', (event) => {
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
     * Agrega un evento cuando hace click en los botones con clase boton2, para abrir posteriormente 
     * una alerta exitosa
     */
    $('.boton2').on('click', (event) => {
        alerta2.toggleClass(function () {
            console.log($(this));
            if ($(this).hasClass('d-none')) {

                $(this).addClass('d-flex').removeClass('d-none');

                //Activa el timer para cerrar automaticamente la alerta al cabo de 2 segundos
                cerrarAlertaAutomaticamente2();
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

    function cerrarAlertaAutomaticamente2() {
        //Realiza una tarea asincrona que ocultara la alerta al cabo de 2 segundos
        setTimeout(function () {
            ocultarAlerta2();
        }, 2000);
    }
     /**
     * Autor: Gabriel Sanhueza
     * Oculta la alerta de rechazo y usa un efecto de jquery mientras se oculta
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

    //oculta la alerta de aceptación
    function ocultarAlerta2() {
        if (!$(this).hasClass('d-none')) {
            alerta2.slideUp("slow", function () {
                alerta2.toggleClass(function () {
                    //Oculta la alert agregando la clase d-none 
                    $(this).addClass('d-none').removeClass('d-flex');
                    return $(this);
                });
                //Remueve el estilo dado por el efecto jquery
                alerta2.removeAttr('style');
            });
        }
    }
    /**
     * Autor: Gabriel Sanhueza
     * Evento sobre la alerta de rechazo para ocultarla
     */
    $('#button-alert').on('click', () => {
        ocultarAlerta();
    })

    /**
     * Evento sobre la alerta de aceptacion
     */
    $('#button-alert2').on('click', () => {
        ocultarAlerta2();
    })

});