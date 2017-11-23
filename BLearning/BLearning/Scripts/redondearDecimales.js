function redondeaDecimales(numero, decimales)
{
    numeroRegexp = new RegExp('\\d\\.(\\d){' + decimales + ',}');
    if (numeroRegexp.test(numero)) {
        return Number(numero.toFixed(decimales)); 
    } else {
        return Number(numero.toFixed(decimales)) === 0 ? 0 : numero;
    }
}