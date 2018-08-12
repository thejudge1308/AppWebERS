$(document).ready(function () {
    //Limpia cache del navegador

    getReferencias();
    /*var elementos = 0;
    if (elementos == 0) {
        $('#referencia_table').find('tbody:last').append(filaVacia());
    }*/
});

$("#ejemplo_m").click(function () {
    init_option();
    $("#referencia_modal").modal();
    //$('#referencia_table').find('tbody:last').append(agregarFila());
});


$('#addref').on('click', function () {
    init_option();
    $("#referencia_modal").modal();

    //$("#referencia_table").find("tbody:last .not-found").remove();
    //$('#referencia_table').find('tbody:last').append(agregarFila());
});


$("#referencia_modal .modal-body").on("click", "#book-button", function () {
    book_option();

    /*$('#anio-box').keypress(function (e) {
        return false
    });
    $.datepicker.setDefaults($.datepicker.regional["es"]);
    var currentdate = $("#Fecha").val();
    $('#anio-box').datepicker({
        format: "yyyy",
        viewMode: "years",
        minViewMode: "years"
    })*/
    fecha_event();
    //$('#anio-box .ui-datepicker-calendar').attr('display', 'none');
});

$("#referencia_modal .modal-body").on("click", "#paper-button", function () {
    paper_option();
    fecha_event();

});

$("#referencia_modal .modal-body").on("click", "#conferencia-button", function () {
    paper_conf_option();
    fecha_event();

});
//Aqui tienen copiar esto
$("#referencia_modal .modal-body").on("click", "#bookOn-button", function () {
    bookOn_option();
    fecha_event();
});
/*
 * Guardado de datos
 * 
 * */
//Libro
$('#referencia_modal .modal-footer').on('click', '#guardar_libro', function () {

    $("#mensaje").text("");
    if ($.trim($("#autores-box").val()) == "" || $.trim($("#anio-box").val()) == "" || $.trim($("#titulo-box").val()) == "" || $.trim($("#lugar-box").val()) == "" || $.trim($("#editorial-box").val())=="") {
        $("#mensaje").text("Ingrese todos los datos.");
    } else {
        guardarLibro($("#ProyectoActual_IdProyecto").val(), $.trim($("#autores-box").val()), $.trim($("#anio-box").val()), $.trim($("#titulo-box").val()), $.trim($("#lugar-box").val()), $.trim($("#editorial-box").val()));
    }
    
});

//LibroOn
//Aqui hice cambios-------------------------------------X----------------------
$('#referencia_modal .modal-footer').on('click', '#guardar_bookOn', function () {

    $("#mensaje").text("");
    if ($.trim($("#autores-box").val()) == "" || $.trim($("#anio-box").val()) == "" || $.trim($("#titulo-box").val()) == "" || $.trim($("#paginaWeb-box").val()) == "") {
        $("#mensaje").text("Ingrese todos los datos.");
    } else {
        //Aqui van los campos a guardar
        guardarBookOn($("#ProyectoActual_IdProyecto").val(), $.trim($("#autores-box").val()), $.trim($("#anio-box").val()), $.trim($("#titulo-box").val()), $.trim($("#paginaWeb-box").val()));
    }

});
//Paper
$('#referencia_modal .modal-footer').on('click', '#guardar_paper', function () {
    $("#mensaje").text("");
    if ($.trim($("#autores-box").val()) == "" || $.trim($("#anio-box").val()) == "" || $.trim($("#titulo-box").val()) == "" || $.trim($("#nombre-box").val()) == "" || $.trim($("#vol-box").val()) == "" || $.trim($("#pag-box").val()) == "") {
        $("#mensaje").text("Ingrese todos los datos.");
    } else {
        guardarRevista($("#ProyectoActual_IdProyecto").val(), $.trim($("#autores-box").val()), $.trim($("#anio-box").val()), $.trim($("#titulo-box").val()), $.trim($("#nombre-box").val()), $.trim($("#vol-box").val()), $.trim($("#pag-box").val()));
    }
});

//Paper de conferencia
$('#referencia_modal .modal-footer').on('click', '#guardar_paper_conf', function () {
    $("#mensaje").text("");
    if ($.trim($("#autores-box").val()) == "" || $.trim($("#anio-box").val()) == "" || $.trim($("#titulo-box").val()) == "" || $.trim($("#place-box").val()) == "" || $.trim($("#nomconf-box").val()) == "") {
        $("#mensaje").text("Ingrese todos los datos.");
    } else {
        guardarReferencia($("#ProyectoActual_IdProyecto").val(), $.trim($("#autores-box").val()), $.trim($("#anio-box").val()), $.trim($("#titulo-box").val()), $.trim($("#place-box").val()), $.trim($("#nomconf-box").val()));
    }
});


//guardarReferencia(idR, autoresR, fechaR, tituloR, lugarR, nombre_conferenciaR)

//Remove Fila actual
$('#referencia_table').on('click', '#boton_fila', function () {

        var td = $(this).parent();
        var tr = td.parent();
        var value = tr.find("span").text();

        var Referencia = {
            id: $("#ProyectoActual_IdProyecto").val(),
            valor: value
        };
        $.ajax({
            type: "POST",
            url: "/Proyecto/RemoverReferenciaPaper",
            data: JSON.stringify(Referencia),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //mostrarAlerta("Modificado con éxito.");
                console.log(response);
                if (response) {
                    mostrarAlerta("Eliminado con éxito");
                    tr.remove();
                    if ($('#referencia_table tr').length == 1) {
                        $('#referencia_table').find('tbody:last').append(filaVacia());
                    }
                }
                else {
                    mostrarAlerta("No se ha podido eliminar la referencia.");
                }
            },
            failure: function (response) {
                mostrarAlerta(response.responseText);
            },
            error: function (response) {
                mostrarAlerta(response.responseText);
            }
        });  

    

    

});

//Opciones del modal
function init_option() {
    $("#referencia_modal .modal-body").empty();
    $("#referencia_modal .modal-body").append(init_modal());
    $("#referencia_modal .modal-footer").empty();
    $("#referencia_modal .modal-footer").append('<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>');
   
}

function book_option() {
    $("#referencia_modal .modal-body").empty();
    $("#tittle_modal").text("Formulario libro");
    $("#referencia_modal .modal-body").append(book_modal());
    $("#referencia_modal .modal-footer").empty();
    $("#referencia_modal .modal-footer").append(
        '<button id="guardar_libro" type="button" class="btn btn-primary">Guardar</button>' +
        '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>');
}
//Aqui hay que copiar y poner el nombre del metodo
function bookOn_option() {
    $("#referencia_modal .modal-body").empty();
    $("#tittle_modal").text("Formulario libro Online");
    $("#referencia_modal .modal-body").append(bookOn_modal());
    $("#referencia_modal .modal-footer").empty();
    $("#referencia_modal .modal-footer").append(
        //Ojo aqui
        '<button id="guardar_bookOn" type="button" class="btn btn-primary">Guardar</button>' +
        '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>');
}
function paper_option() {
    $("#referencia_modal .modal-body").empty();
    $("#tittle_modal").text("Formulario revista.");
    $("#referencia_modal .modal-body").append(paper_modal());
    $("#referencia_modal .modal-footer").empty();
    $("#referencia_modal .modal-footer").append(
        '<button id="guardar_paper" type="button" class="btn btn-primary">Guardar</button>' +
        '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>');
}

function paper_conf_option() {
    $("#referencia_modal .modal-body").empty();
    $("#tittle_modal").text("Formulario revista.");
    $("#referencia_modal .modal-body").append(paper_conf_modal());
    $("#referencia_modal .modal-footer").empty();
    $("#referencia_modal .modal-footer").append(
        '<button id="guardar_paper_conf" type="button" class="btn btn-primary">Guardar</button>' +
        '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>');
}

//HTML del modal
function init_modal() {
    var html = '<div class="container-fluid">'+
        '<div class="row">'+
            '<div class="col-md-12 m-2">'+
                '<button id="book-button" type="button" style="width:100%" class="btn btn-primary">Libro.</button>'+
            '</div>'+
        '</div >'+
        '<div class="row">'+
            '<div class="col-md-12 m-2">'+
                '<button id="paper-button" type="button" style="width:100%" class="btn btn-primary">Paper</button>'+
            '</div>'+
        '</div>' +
        '<div class="row">' +
            '<div class="col-md-12 m-2">' +
                '<button id="conferencia-button" type="button" style="width:100%" class="btn btn-primary">Paper de conferencia.</button>' +
            '</div>' +
        '</div>' +
        //Copiar y agregar nuevo boton
        '<div class="row">' +
            '<div class="col-md-12 m-2">' +
                '<button id="bookOn-button" type="button" style="width:100%" class="btn btn-primary">Libro Online.</button>' +
            '</div>' +
        '</div>' +
        '</div >';
    return html;
}

function book_modal() {
    html = '<div class="container-fluid">'+
        '<div class="row">'+
        '<div class="col-md-12 col-sm-12 col-xs-12">' +
            '<div class="form-group">'+
                        '<label class="control-label"> Autores. </label>'+
                        '<input class="form-control" style="max-width: 100%;" id="autores-box" type="text" />'+
            '</div>'+
            '<div class="form-group">' +
                '<label class="control-label"> Año de publicación. </label>' +
                '<input class="form-control" style="max-width: 100%;" id="anio-box" type="number" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label class="control-label"> Título del libro. </label>' +
                '<input class="form-control" style="max-width: 100%;" id="titulo-box" type="text" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label class="control-label " for="name"> Páginas. </label>' +
                '<input class="form-control" style="max-width: 100%;" id="lugar-box" type="text" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label class="control-label " for="name"> Editorial. </label>' +
                '<input class="form-control" style="max-width: 100%;" id="editorial-box" type="text" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label id="mensaje" class="control-label text-danger"></label>' +
            '</div>' +
        '</div>'+
        '</div>'+
        '</div >';
    return html;
}


//Esto hay que copiar
function bookOn_modal() {
    html = '<div class="container-fluid">' +
        '<div class="row">' +
        '<div class="col-md-12 col-sm-12 col-xs-12">' +
        '<div class="form-group">' +
        '<label class="control-label"> Autores. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="autores-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label"> Año de publicación. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="anio-box" type="number" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label"> Título del libro. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="titulo-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Pagina Web. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="paginaWeb-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label id="mensaje" class="control-label text-danger"></label>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div >';
    return html;
}

function paper_modal() {
    html ='<div class="container-fluid">' +
        '<div class="row">' +
        '<div class="col-md-12 col-sm-12 col-xs-12">' +
        '<div class="form-group">' +
        '<label class="control-label" for="name"> Autores. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="autores-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label" for="name"> Año de publicación. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="anio-box" type="number" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Título del libro. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="titulo-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Nombre de la revista. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="nombre-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Volumen. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="vol-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Páginas. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="pag-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label id="mensaje" class="control-label text-danger"></label>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div >';
    return html;
}

function paper_conf_modal() {
    html = '<div class="container-fluid">' +
        '<div class="row">' +
        '<div class="col-md-12 col-sm-12 col-xs-12">' +
        '<div class="form-group">' +
        '<label class="control-label" for="name"> Autores. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="autores-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label" for="name"> Año de publicación. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="anio-box" type="number" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Título del paper. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="titulo-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Lugar de presentación. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="place-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="control-label " for="name"> Nombre de la conferencia. </label>' +
        '<input class="form-control" style="max-width: 100%;" id="nomconf-box" type="text" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label id="mensaje" class="control-label text-danger"></label>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div >';
    return html;
}

function agregarFila(mensaje) {
    html =
        '<tr>' +
        '<td class="mesaje"><span>' + mensaje + '</span></td>' +
        '<td class="text-right"><a class="btn btn-danger text-white" id="boton_fila" >Eliminar</a></td>' +
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

/*
 * GET AND POST
 */
//Get
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

//Post
function guardarLibro(idR, autoresR, anioR, tituloR, lugarR, editorialR) {
    var JsonReferenciaLibro = {
        id: idR,
        autores : autoresR,
        anio: anioR,
        titulo: tituloR,
        lugar: lugarR,
        editorial: editorialR
    };
    console.log(JsonReferenciaLibro);
    $.ajax({
        type: "POST",
        url: "/Proyecto/AgregarReferenciaLibro",
        data: JSON.stringify(JsonReferenciaLibro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //mostrarAlerta("Modificado con éxito.");
            console.log(response);
            if (response) {
                mostrarAlerta("Agregado con éxito");
                $('#referencia_modal').modal('toggle');
                $('#referencia_table tbody').empty();
                getReferencias();   
            }
            else {
                mostrarAlerta("No se ha podido guardar la referencia.");
            }
            
        },
        failure: function (response) {
            mostrarAlerta(response.responseText);
        },
        error: function (response) {
            mostrarAlerta(response.responseText);
        }
    });  
}

function guardarBookOn(idR, autoresR, anioR, tituloR, paginawebR) {
    var JsonReferenciaLibro = {
        id: idR,
        autores: autoresR,
        anio: anioR,
        titulo: tituloR,
        paginaWeb: paginawebR
    };
    console.log(JsonReferenciaLibro);
    $.ajax({
        type: "POST",
        url: "/Proyecto/AgregarReferenciaLibroOnline",
        data: JSON.stringify(JsonReferenciaLibro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //mostrarAlerta("Modificado con éxito.");
            console.log(response);
            if (response) {
                mostrarAlerta("Agregado con éxito");
                $('#referencia_modal').modal('toggle');
                $('#referencia_table tbody').empty();
                getReferencias();
            }
            else {
                mostrarAlerta("No se ha podido guardar la referencia.");
            }

        },
        failure: function (response) {
            mostrarAlerta(response.responseText);
        },
        error: function (response) {
            mostrarAlerta(response.responseText);
        }
    });
}

function guardarRevista(idR, autoresR, fechaR, tituloR, revistaR, volumenR, pagR) {
    var JsonReferenciaPaper = {
        id: idR,
        autores: autoresR,
        fecha: fechaR,    
        titulo: tituloR,
        revista: revistaR,
        volumen: volumenR,
        pag: pagR
    };

    $.ajax({
        type: "POST",
        url: "/Proyecto/AgregarReferenciaPaper",
        data: JSON.stringify(JsonReferenciaPaper),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //mostrarAlerta("Modificado con éxito.");
            console.log(response);
            if (response) {
                mostrarAlerta("Agregado con éxito");
                $('#referencia_modal').modal('toggle');
                $('#referencia_table tbody').empty();
                getReferencias();   
            }
            else {
                mostrarAlerta("No se ha podido guardar la referencia.");
            }
        },
        failure: function (response) {
            mostrarAlerta(response.responseText);
        },
        error: function (response) {
            mostrarAlerta(response.responseText);
        }
    });  
}

function guardarReferencia(idR, autoresR, fechaR, tituloR, lugarR, nombre_conferenciaR) {
    var JsonReferenciaConferencia = {
        id: idR,
        autores: autoresR,
        fecha: fechaR,
        titulo: tituloR,
        lugar: lugarR,
        nombre_conferencia: nombre_conferenciaR
    }

    $.ajax({
        type: "POST",
        url: "/Proyecto/AgregarReferenciaConferencia",
        data: JSON.stringify(JsonReferenciaConferencia),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //mostrarAlerta("Modificado con éxito.");
            console.log(response);
            if (response) {
                mostrarAlerta("Agregado con éxito");
                $('#referencia_modal').modal('toggle');
                $('#referencia_table tbody').empty();
                getReferencias();
            }
            else {
                mostrarAlerta("No se ha podido guardar la referencia.");
            }
        },
        failure: function (response) {
            mostrarAlerta(response.responseText);
        },
        error: function (response) {
            mostrarAlerta(response.responseText);
        }
    });  

}

/**
 * Eventos
 */
function fecha_event() {
    $("#anio-box").change(function () {
        if ($(this).val() < 1) {
            $(this).val(1);
        }
        else if ($(this).val() > 3000) {
            $(this).val(3000);
        }
    }); 
}