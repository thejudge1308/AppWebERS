//Creado por Patricio Quezada.
//Descripcion, script que configura la vista del SysAdmin
$(document).ready(function () {
    //Configuracion de Quilljs
    var config = {
        theme: 'bubble', 
        readOnly: true,
    };
    //Inilizacion de los AreaText
    var nombreQ = new Quill('#nombre-edit', config);
    var propositoQ = new Quill('#proposito-edit', config);
    var alcanceQ = new Quill('#alcance-edit', config);
    var contextoQ = new Quill('#contexto-edit', config);
    var definicionQ = new Quill('#definicion-edit', config);
    var acronimoQ = new Quill('#acronimo-edit', config);
    var abreviaturaQ = new Quill('#abreviatura-edit', config);
    var referenciaQ = new Quill('#referencia-edit', config);
    var ambienteQ = new Quill('#ambiente-edit', config);
    var relacionQ = new Quill('#relacion-edit', config);
    //obtiene la informacion
    var urlget = "/Proyecto/infoProyecto/" + $("#ProyectoActual_IdProyecto").val();
    $.get(urlget, function () {

    })
        .done(function (data) {
            console.log(data);
            var emptydata = "<p></p>";
            //obtener valores
            var nombre = data.Nombre === "" ? emptydata : data.Nombre;
            var proposito = data.Proposito === "" ? emptydata : data.Proposito;
            var alcance = data.Alcance === "" ? emptydata : data.Alcance;
            var contexto = data.Contexto === "" ? emptydata : data.Contexto;
            var definicion = data.Definiciones === "" ? emptydata : data.Definiciones;
            var acronimo = data.Acronimos === "" ? emptydata : data.Acronimos;
            var abreviatura = data.Abreviaturas === "" ? emptydata : data.Abreviaturas;
            var referencia = data.referencia === "" ? emptydata : data.referencia;
            var ambiente = data.AmbienteOperacional === "" ? emptydata : data.AmbienteOperacional;
            var relacion = data.RelacionProyectos === "" ? emptydata : data.RelacionProyectos;

            //Set space
            $('#nombre-edit .ql-editor').empty();
            $('#proposito-edit .ql-editor').empty();
            $('#alcance-edit .ql-editor').empty();
            $('#contexto-edit .ql-editor').empty();
            $('#definicion-edit .ql-editor').empty();
            $('#acronimo-edit .ql-editor').empty();
            $('#abreviatura-edit .ql-editor').empty();
            $('#referencia-edit .ql-editor').empty();
            $('#ambiente-edit .ql-editor').empty();
            $('#relacion-edit .ql-editor').empty();

            //cargar la informacion 
            $('#nombre-edit .ql-editor').append(nombre);
            $('#proposito-edit .ql-editor').append(proposito);
            $('#alcance-edit .ql-editor').append(alcance);
            $('#contexto-edit .ql-editor').append(contexto);
            $('#definicion-edit .ql-editor').append(definicion);
            $('#acronimo-edit .ql-editor').append(acronimo);
            $('#abreviatura-edit .ql-editor').append(abreviatura);
            $('#referencia-edit .ql-editor').append(referencia);
            $('#ambiente-edit .ql-editor').append(ambiente);
            $('#relacion-edit .ql-editor').append(relacion);

            $('.ql-editor').addClass('border border-dark');
        })
        .fail(function () {
            //alert("error");
        })
        .always(function () {
            //alert("finished");
        });
});