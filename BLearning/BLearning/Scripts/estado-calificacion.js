function agregarMensaje()
{
    var calf = $("#calif").attr("data-valor");
    var calificacion = calf.replace(",", ".");
    console.log(calificacion);
    if (calificacion >= 95)
    {
        imagen = document.getElementById("img-cali-star");
        imagen.setAttribute('src', '../Img/medallas/excelente.png');
        $("#mens1").append("¡Perfect!");
        $("#mens2").append("You're awesome");
    }
    else if (calificacion >= 85 && calificacion < 95)
    {
        imagen = document.getElementById("img-cali-star");
        imagen.setAttribute('src', '../Img/medallas/muy-bueno.png');
        $("#mens1").append("¡Well done!");
        $("#mens2").append("Keep the good way");
    }
    else if (calificacion >= 70 && calificacion < 85)
    {
        imagen = document.getElementById("img-cali-star");
        imagen.setAttribute('src', '../Img/medallas/bueno.png');
        $("#mens1").append("¡Good!");
        $("#mens2").append("You can do it better");
    }
    else if (calificacion >= 40 && calificacion < 70)
    {
        imagen = document.getElementById("img-cali-star");
        imagen.setAttribute('src', '../Img/medallas/regular.png');
        $("#mens1").append("¡Keep on practicing!");
        $("#mens2").append("Don't give up");
    }
    else
    {
        imagen = document.getElementById("img-cali-star");
        imagen.setAttribute('src', '../Img/medallas/deficiente.png');
        $("#mens1").append("¡I'm very worried!");
        $("#mens2").append("You really need practice");
    }
}

function consultarEstadoCalificacion(calificacion) {
    if (calificacion >= 95) {
        return "Excellent";
    }
    else if (calificacion >= 85 && calificacion < 95) {
        return "Very Good";
    }
    else if (calificacion >= 70 && calificacion < 85) {
        return "Good";
    }
    else if (calificacion >= 40 && calificacion < 70) {
        return "Regular";
    }
    else {
        return "Deficient";
    }
}
