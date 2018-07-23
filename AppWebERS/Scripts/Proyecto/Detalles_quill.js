﻿//Autor: Patricio Quezada
//Descripcion: Js del formulario de Proyecto/Detalles/ e implementacion de QuillJs

$(document).ready(function () {
    //Configuracion de la barra de herramientas
    var opcionesTolbar = [
        ['bold', 'italic', 'underline', 'strike'],        // toggled buttons

        [{ 'header': 1 }, { 'header': 2 }],// custom button values
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }], //Header size

        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
        [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent

        [{ 'font': [] }],                                 // Front 
        [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
       

        [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
        
        [{ 'align': [] }],

        ['clean']                                         // remove formatting button
    ];

    //Configuracion de Quilljs
     var config = {
         theme: 'snow',
         placeholder: 'Escribe algo aquí..',
         modules: {
             toolbar: opcionesTolbar
         },
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
           //console.log(data);
            var emptydata = "<p></br></p>";
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

        })
        .fail(function () {
            //alert("error");
        })
        .always(function () {
            //alert("finished");
        });

    $('#nombre-button').on('click', function () {
        console.log(nombreQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "nombre", nombreQ.root.innerHTML);
    });
    $('#proposito-button').on('click', function () {
        console.log(propositoQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "proposito", propositoQ.root.innerHTML);
    });
    $('#alcance-button').on('click', function () {
        console.log(alcanceQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "alcance", alcanceQ.root.innerHTML);
    });
    $('#contexto-button').on('click', function () {
        console.log(contextoQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "contexto", contextoQ.root.innerHTML);
    });
    $('#definicion-button').on('click', function () {
        console.log(definicionQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "definicion", definicionQ.root.innerHTML);
    });
    $('#acronimo-button').on('click', function () {
        console.log(acronimoQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "acronimo", acronimoQ.root.innerHTML);
    });
    $('#abreviatura-button').on('click', function () {
        console.log(abreviaturaQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "abreviatura", abreviaturaQ.root.innerHTML);
    });
    $('#referencia-button').on('click', function () {
        console.log(referenciaQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "referencia", referenciaQ.root.innerHTML);
    });
    $('#ambiente-button').on('click', function () {
        console.log(ambienteQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "ambiente", ambienteQ.root.innerHTML);
    });
    $('#relacion-button').on('click', function () {
        console.log(relacionQ.root.innerHTML);
        info_post($("#ProyectoActual_IdProyecto").val(), "relacion", relacionQ.root.innerHTML);
    });

    function info_post(idProyecto,attr,informacion) {
        var ProyectoJsonRespuesta = {
            id : idProyecto,
            atributo : attr,
            valor: informacion
        };
        console.log(ProyectoJsonRespuesta);
       /* $.post(ProyectoJsonRespuesta, function () {
            // si es necesario, colocar inicio de Wait aqui
             })
            .done(function (i) {
                console.log(i);
            })
            .fail(function () {
                alert("No se ha podido modificar");
            })
            .always(function () {
               
            });*/

        $.ajax({
            type: "POST",
            url: "/Proyecto/Detalles",
            data: JSON.stringify(ProyectoJsonRespuesta),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });  

    }

});
