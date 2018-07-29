$("#ejemplo_m").click(function () {
    init_option();
    $("#referencia_modal").modal();
});

$("#referencia_modal .modal-body").on("click", "#book-button", function () {
    book_option();
});


//Opciones
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
        '<button type="button" class="btn btn-primary">Guardar</button>' +
        '<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>');
}

//HTML
function init_modal() {
    var html = '<div class="container-fluid">'+
        '<div class="row">'+
            '<div class="col-md-12 m-2">'+
                '<button id="book-button" type="button" style="width:100%" class="btn btn-primary">Libro.</button>'+
            '</div>'+
        '</div >'+
        '<div class="row">'+
            '<div class="col-md-12 m-2">'+
                '<button type="button" style="width:100%" class="btn btn-primary">Peipa</button>'+
            '</div>'+
        '</div>'+
        '</div >'+
        '</div >';
    return html;
}

function book_modal() {
    html = '<div class="container-fluid">'+
        '<div class="row">'+
        '<div class="col-md-12 col-sm-12 col-xs-12">' +
            '<div class="form-group">'+
                        '<label class="control-label" for="name"> Autores </label>'+
                        '<input class="form-control" style="width:100%" id="name" name="name" type="text" />'+
            '</div>'+
            '<div class="form-group">' +
                '<label class="control-label" for="name"> Año de publicación </label>' +
                '<input class="form-control" style="width:100%" id="name" name="name" type="text" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label class="control-label " for="name"> Título del libro </label>' +
                '<input class="form-control" style="width:100%" id="name" name="name" type="text" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label class="control-label " for="name"> Lugar de publicación </label>' +
                '<input class="form-control" style="width:100%" id="name" name="name" type="text" />' +
            '</div>' +
            '<div class="form-group">' +
                '<label class="control-label " for="name"> Editorial </label>' +
                '<input class="form-control" style="width:100%" id="name" name="name" type="text" />' +
            '</div>' +
        '</div>'+
        '</div>'+
        '</div >';
    return html;
}

