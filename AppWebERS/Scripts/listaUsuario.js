$(document).ready(() => {
    let alerta = $('#alert');
    cerrarAlertaAutomaticamente(); //Si hay una alerta la ocultara 

    /**
     * Evento sobre los botones deshabilitar, muestra la alerta.
     */
    $('.deshabilitar').on('click', () => {
        alerta.toggleClass(function () {
            console.log($(this));
            if ($(this).hasClass('d-none')) {

                $(this).addClass('d-flex').removeClass('d-none');

                //Activa el timer para cerrar automaticamente la alerta al cabo de 2 segundos
                cerrarAlertaAutomaticamente();
            }
            return $(this);
        })
    });

    /**
    * Funcion que cerrara una alerta que este siendo mostrada al cabo de 2 segundos.
    */
    function cerrarAlertaAutomaticamente() {
        //Realiza una tarea asincrona que ocultara la alerta al cabo de 2 segundos
        setTimeout(function () {
            ocultarAlerta();
        }, 2000);
    }

    /**
     *  Funcion que oculta la alerta, usa un efecto de jquery
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
     * Evento sobre el boton de la alerta para ocultarla.
     */
    $('#button-alert').on('click', () => {
        ocultarAlerta();
    })
});