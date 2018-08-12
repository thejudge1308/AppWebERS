//Creado por Patricio Quezada.
//Descripcion, script que configura la vista del SysAdmin
$(document).ready(function () {
    //Configuracion de Quilljs
   /* var config = {
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
    var relacionQ = new Quill('#relacion-edit', config);*/
    //obtiene la informacion
    var urlget = "/Proyecto/infoProyecto/" + $("#ProyectoActual_IdProyecto").val();
    $.get(urlget, function () {

    })
        .done(function (data) {
            var emptydata = "<p></br></p>";
            //obtener valores
            var nombre = data.Nombre === "" ? emptydata : data.Nombre;
            var version = data.Version === "" ? emptydata : data.Version;
            var descripcion = data.Descripcion === "" ? emptydata : data.Descripcion;
            var proposito = data.Proposito === "" ? emptydata : data.Proposito;
            var alcance = data.Alcance === "" ? emptydata : data.Alcance;
            var contexto = data.Contexto === "" ? emptydata : data.Contexto;
            var definicion = data.Definiciones === "" ? emptydata : data.Definiciones;
            var acronimo = data.Acronimos === "" ? emptydata : data.Acronimos;
            var abreviatura = data.Abreviaturas === "" ? emptydata : data.Abreviaturas;
            var referencia = data.Referencia === "" ? emptydata : data.Referencia;
            var suposicion = data.Suposicion === "" ? emptydata : data.Suposicion;
            var restriccion = data.Restriccion === "" ? emptydata : data.Restriccion;
            var ambiente = data.AmbienteOperacional === "" ? emptydata : data.AmbienteOperacional;
            var relacion = data.RelacionProyectos === "" ? emptydata : data.RelacionProyectos;

            //Set space
            $('#nombre-edit').addClass('border border-dark p-3');
            $('#version-edit').addClass('border border-dark p-3');
            $('#descripcion-edit').addClass('border border-dark p-3');
            $('#proposito-edit').addClass('border border-dark p-3');
            $('#alcance-edit').addClass('border border-dark p-3');
            $('#contexto-edit').addClass('border border-dark p-3');
            $('#definicion-edit').addClass('border border-dark p-3');
            $('#acronimo-edit').addClass('border border-dark p-3');
            $('#abreviatura-edit').addClass('border border-dark p-3');
            $('#referencia-edit').addClass('border border-dark p-3');
            $('#suposicion-edit').addClass('border border-dark p-3');
            $('#restriccion-edit').addClass('border border-dark p-3');
            $('#ambiente-edit').addClass('border border-dark p-3');
            $('#relacion-edit').addClass('border border-dark p-3');

            //cargar la informacion 
            $('#nombre-edit').append(nombre);
            $('#version-edit').append(version);
            $('#descripcion-edit').append(descripcion);
            $('#proposito-edit').append(proposito);
            $('#alcance-edit').append(alcance);
            $('#contexto-edit').append(contexto);
            $('#definicion-edit').append(definicion);
            $('#acronimo-edit').append(acronimo);
            $('#abreviatura-edit').append(abreviatura);
            $('#referencia-edit').append(referencia);
            $('#suposicion-edit').append(suposicion);
            $('#restriccion-edit').append(restriccion);
            $('#ambiente-edit').append(ambiente);
            $('#relacion-edit').append(relacion);

           // $('.ql-editor').addClass('border border-dark');
        })
        .fail(function () {
            //alert("error");
        })
        .always(function () {
            //alert("finished");
        });
});