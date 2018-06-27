// Ejecuta cuando se haya cargado la pagina completa
$(document).ready(() => {
    let alerta = $('#alertDiv');
    const TiposAlerta = {
        EXITO: 'alert-success',
        ERROR: 'alert-danger'
    };
    cerrarAlertaAutomaticamente(); //Si hay una alerta la ocultara 
    /**
     * Autor: Gabriel Sanhueza
     * Agrega un evento cuando se hace click en los botones con clase deshabilitar para que 
     * muestre la alerta
     * Parametros: 
     * event: objeto con informacion del evento
     */
    $('.deshabilitar').on('click', (event) => {
        let mensaje = "El usuario se ha deshabilitado correctamente";
        mostrarAlerta(mensaje, TiposAlerta.EXITO);
        event.preventDefault(); //Se arreglo por mientras, despues hay que quitarlo y configurar desde el servidor la muestra de alerta
    });

    function mostrarAlerta(mensaje, tipo = TiposAlerta.EXITO) {
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
     * Autor Gabriel Sanhueza
     * Evento que cambia el enlace del iframe para que liste el usuario correcto para la modificacion
     */
    $("[data-target='#exampleModal']").on('click', (event) => {
        var iframe = $("#iframe")[0];
        iframe.src = `/Usuario/ModificarCuenta?rut=${event.target.dataset.rut}`;
    })

   /**
    * Autor Gabriel Sanhueza
    * Evento en el boton que enviara el formulario
    */
    $("button[type='submit']").on('click', (event) => {
        let form = $("#iframe").contents().find("#form");
        let prevenido = $("#iframe").contents().find("#form")[0].dispatchEvent(new Event('submit', {bubbles: true, cancelable:true}));
        if (!prevenido) {
            event.preventDefault();
            return;
        }
        form.submit();
        mostrarAlerta("Se ha modificado correctamente el usuario", TiposAlerta.EXITO);
        $("#exampleModal").modal('toggle');
    });
});