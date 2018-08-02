$(document).ready(function () {
    getReferencias();
    /*var elementos = 0;
    if (elementos == 0) {
        $('#referencia_table').find('tbody:last').append(filaVacia());
    }*/
});

function agregarFila(mensaje) {
    html =
        '<tr>' +
        '<td class="mesaje"><span>' + mensaje + '</span></td>' +
        '<td class="text-right"></td>' +
        '</tr>';
    return html;
}

function filaVacia() {
    html = '<tr class="table-info not-found">' +
        '<td ><br>No hay referencias.<br></td>' +
        '<td></td>' +
        '</tr>';
    return html;
}

function getReferencias() {
    var urlget = "/Proyecto/MostrarReferencia/" + $("#ProyectoActual_IdProyecto").val();
    $.get(urlget, function () {
    })
        .done(function (data) {
            if (data == "null") {
                $('#referencia_table').find('tbody:last').append(filaVacia());
            } else {
                console.log(data);
                $.each(data, function (i, item) {
                    console.log(item.valor);
                    $("#referencia_table").find("tbody:last .not-found").remove();
                    $('#referencia_table').find('tbody:last').append(agregarFila(item.valor));

                });


            }

        })
        .fail(function () {
            //alert("error");
        })
        .always(function () {
            //alert("finished");
        });
}