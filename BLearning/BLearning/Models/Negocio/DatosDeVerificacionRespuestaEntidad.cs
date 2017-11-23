using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class DatosDeVerificacionRespuestaEntidad
    {
        public int idPregunta { get; set; }
        public int idOpcionPregunta { get; set; }
        public string respuestaIngresada { get; set; }
        public string tipoPregunta { get; set; }
    }
}