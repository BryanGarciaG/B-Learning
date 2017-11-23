$(function () {
    $(".respuesta").draggable({
        appendTo: "body",
        revert: "invalid",
        helper: "original",
        start: function (event, ui) {
        }

    });

    $(".soltable").droppable({
        accept: ".respuesta",
        activeClass: "ui-state-hover",
        hoverClass: "ui-state-active",
        drop: function (event, ui) {
            $("<div class='respuesta tile orange ui-state-default'></div>").html(ui.draggable.html()).appendTo(this);
            ui.draggable.remove();
        }
    });
    $(".soltable").sortable();
    $(".soltable").disableSelection();
});

var tipoPregunta = 0;
function back(est) {
    window.location.hash = "no";
    window.location.hash = "Again-No" //chrome
    window.onhashchange = function () { window.location.hash = "no"; }
    $(habilitar(est));
}

function habilitar(est) {
    if (est != "V") {
        $('#siguiente').addClass("disabled");
    }
}


//Alerta de error
function errorAlert() {
    swal({
        title: "Error!",
        text: "Empty field! Fill in all fields to rate the question ",
        type: "error",
        confirmButtonText: "OK"
    });
}

function errorAlertContestada() {
    swal({
        title: "Error!",
        text: "This question has already been resolved. Press continue",
        type: "error",
        confirmButtonText: "OK"
    });
}
//Obtiene las respuestas ingresadas por el usuario
var listaDeDatos = [];
function obtenerDatos(op) {
        
    var datosDeComprobacion = {};
    listaDeDatos = [];
    var check = false;

    var idPregunta = $('#pregunta').attr('data-idpre');
    tipoPregunta = $('#pregunta').attr('data-tipo');
    var verificadorCamposVacios = false;
   
    $('#detPregunta').find('.fg').each(function (i) {
        $(this).find(':input').each(function (j, input) {
                datosDeComprobacion['idPregunta'] = idPregunta;
                datosDeComprobacion['tipoPregunta'] = tipoPregunta;
                if (tipoPregunta != "Seleccion Multiple") {
                    datosDeComprobacion[$(input).attr('name')] = $(input).val();
                    check = true;
                } else {
                    datosDeComprobacion['idOpcionPregunta'] = document.getElementById('idOpcionSeleccion+' + i).value;
                    if ($(".respuesta-" + i).is(':checked')) {
                        datosDeComprobacion['respuestaIngresada'] = 'true';
                        check = true;
                    } else {
                        datosDeComprobacion['respuestaIngresada'] = 'false';
                    }
                }
            });

        if (datosDeComprobacion['idOpcionPregunta'] === "" || datosDeComprobacion['idOpcionPregunta'] === undefined || datosDeComprobacion['respuestaIngresada'] === "" || datosDeComprobacion['respuestaIngresada'] === " " || datosDeComprobacion['respuestaIngresada'] === null) {
            verificadorCamposVacios = true;
        } else {
            listaDeDatos.push(datosDeComprobacion);
        }
        datosDeComprobacion = {};
        });

    if (verificadorCamposVacios || check == false) {
        errorAlert();
    } else {
        $('#calificar').addClass("disabled");
        $('#siguiente').removeClass("disabled");
        if (op== "da") {
            $(ProbarActividad());
        }
        else {
            $(realizarEnvioDatos());
        }
    }
}

//Hace el envio de los datos al sevirdor y trae una lista de las verificacion de las respuestas
function realizarEnvioDatos() {
    var index = $('#pregunta').attr('data-indice');
    var ultimaPre = $('#pregunta').attr('data-ultima');
    $.ajax({
        url: "../Estudiante/ObtieneElementos",
        type: "POST",
        data: JSON.stringify({ ListRespuesta: listaDeDatos, duracion: $('#pregunta').attr('data-duracion'), indexControl: index, idActividad: $('#pregunta').attr('data-actividad'), ultimaPregunta: ultimaPre}),
        contentType: 'application/json',
        processData: false,
        success: function (result) {
            if (result.data.length != 0)
            {
                if (tipoPregunta != "Emparejamiento") {
                    $('#detPregunta').find('.fg').each(function (i) {
                        $(".respuesta-" + i).attr('readonly', true);
                        if (result.data[i] == "C") {
                            $(".respuesta-" + i).css("background", "lightgreen");
                        } else {
                            $(".respuesta-" + i).css("background", "rgba(231, 76, 60, 0.56)");
                        }
                    });
                } else {
                    $('#detPregunta').find('.fg >.respuesta').each(function (i) {
                        if (result.data[i] == "C") {
                            $(this).css("background", "lightgreen");
                        } else {
                            $(this).css("background", "rgba(231, 76, 60, 0.56)");
                        }
                    });
                    $(".soltable").droppable({
                        accept: ".none",
                        hoverClass: "ui-state-disabled",
                    });

                    $('.respuesta').removeClass('ui-state-default');
                    $('.respuesta').addClass('ui-state-disabled');
                }
            }else
            {
                errorAlertContestada();
                if (tipoPregunta != "Emparejamiento") {
                    $('#detPregunta').find('.fg').each(function (i) {
                        $(".respuesta-" + i).attr('readonly', true);
                    });
                } else {
                    $(".soltable").droppable({
                        accept: ".none",
                        hoverClass: "ui-state-disabled",
                    });

                    $('.respuesta').removeClass('ui-state-default');
                    $('.respuesta').addClass('ui-state-disabled');
                }
            }
            
                
        }
    });
}


function ProbarActividad() {
    var index = $('#pregunta').attr('data-indice');
    var ultimaPre = $('#pregunta').attr('data-ultima');
    $.ajax({
        url: "../Actividad/comprobrarQuiz",
        type: "POST",
        data: JSON.stringify({ ListRespuesta: listaDeDatos, indexControl: index, idActividad: $('#pregunta').attr('data-actividad'), ultimaPregunta: ultimaPre }),
        contentType: 'application/json',
        processData: false,
        success: function (result) {
            if (result.data.length != 0) {
                if (tipoPregunta != "Emparejamiento") {
                    $('#detPregunta').find('.fg').each(function (i) {
                        $(".respuesta-" + i).attr('readonly', true);
                        if (result.data[i] == "C") {
                            $(".respuesta-" + i).css("background", "lightgreen");
                        } else {
                            $(".respuesta-" + i).css("background", "rgba(231, 76, 60, 0.56)");
                        }
                    });
                } else {
                    $('#detPregunta').find('.fg >.respuesta').each(function (i) {
                        if (result.data[i] == "C") {
                            $(this).css("background", "lightgreen");
                        } else {
                            $(this).css("background", "rgba(231, 76, 60, 0.56)");
                        }
                    });
                    $(".soltable").droppable({
                        accept: ".none",
                        hoverClass: "ui-state-disabled",
                    });

                    $('.respuesta').removeClass('ui-state-default');
                    $('.respuesta').addClass('ui-state-disabled');
                }
            } else {
                errorAlertContestada();
                if (tipoPregunta != "Emparejamiento") {
                    $('#detPregunta').find('.fg').each(function (i) {
                        $(".respuesta-" + i).attr('readonly', true);
                    });
                } else {
                    $(".soltable").droppable({
                        accept: ".none",
                        hoverClass: "ui-state-disabled",
                    });

                    $('.respuesta').removeClass('ui-state-default');
                    $('.respuesta').addClass('ui-state-disabled');
                }
            }


        }
    });
}




