using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class OpcionesEntidad
    {
        public int idOpciones { get; set; }
        public int idCabeceraPregunta { get; set; }
        public string detallePregunta { get; set; }
    }
}