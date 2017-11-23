using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class RespuestaEntidad
    {
        public int idRespuesta { get; set; }
        public int idOpciones { get; set; }
        public string detalleRespuesta { get; set; }
    }
}