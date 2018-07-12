// Ejecuta cuando se haya cargado la pagina completa
$(document).ready(() => {
    let alerta = $('#alertDiv');
    const TipoAlerta = {
        EXITO: 'alert-success',
        ERROR: 'alert-danger'
    };

    cerrarAlertaAutomaticamente(); //Si hay una alerta la ocultara 

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

    /**
     * Autor: Gabriel Sanhueza
     * Modifica la alerta
     * Parametros: 
     * mensaje: Texto que se mostrara
     * tipo: El tipo de alerta (TipoAlerta.EXITO o TipoAlerta.Error)
     */
    function mostrarAlerta(mensaje, tipo = TipoAlerta.EXITO) {
        alerta.toggleClass(function () {
            console.log($(this));
            if ($(this).hasClass('d-none')) {
                $(this).addClass('d-flex').removeClass('d-none');

                //Borra las clases y añade el nuevo tipo
                $('.alert').removeClass(TiposAlerta.EXITO);
                $('.alert').removeClass(TiposAlerta.ERROR);
                $('.alert').addClass(tipo);
                $('#mensajeAlerta').text(mensaje);
                //Activa el timer para cerrar automaticamente la alerta al cabo de 2 segundos
                cerrarAlertaAutomaticamente();
            }
            return $(this);
        })
    }

    /**
     * Autor: Gabriel Sanhueza
     * Agrega un evento cuando se hace click en los botones con clase deshabilitar para que 
     * muestre la alerta
     * Parametros: 
     * event: objeto con informacion del evento
     */
    //$('.deshabilitar').on('click', (event) => {
    //    let mensaje = "El usuario se ha deshabilitado correctamente";
    //    mostrarAlerta(mensaje, TipoAlerta.EXITO);
    //    event.preventDefault(); //Se arreglo por mientras, despues hay que quitarlo y configurar desde el servidor la muestra de alerta
    //});

    /**
     * Autor: Gabriel Sanhueza
     * Evento sobre la alerta para ocultarla
     */
    $('#button-alert').on('click', () => {
        ocultarAlerta();
    })
});