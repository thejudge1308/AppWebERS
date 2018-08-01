// Creado por patricio Quezada
//Implementación de datepicker y deshabilitar el textbox en escritura. 
$(document).ready(function () {
    $("#Fecha").keypress(function (e) {
        return false
    });
    $.datepicker.setDefaults($.datepicker.regional["es"]);
    var currentdate = $("#Fecha").val();
    $("#Fecha").datepicker({
        dateFormat: 'yy-mm-dd',
        maxDate: currentdate,
        showAnim:'clip',
        changeMonth: true,
        changeYear: true
    })
});