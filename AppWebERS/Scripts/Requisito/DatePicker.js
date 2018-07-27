// Creado por patricio Quezada
//Implementación de datepicker y deshabilitar el textbox en escritura. 
$(document).ready(function () {
    $("#Fecha").keypress(function (e) {
        return false
    });
    $.datepicker.setDefaults($.datepicker.regional["es"]);
    $("#Fecha").datepicker({
        dateFormat: 'yy-mm-dd',
        showAnim:'clip',
        changeMonth: true,
        changeYear: true
    })
});