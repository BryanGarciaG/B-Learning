using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ActividadModuloEntidad
    {
        public int idActModulo { get; set; }
        public int idModulo { get; set; }
        public int idActividad { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public int idLeccion { get; set; }
        public string tipo { get; set; }

    }
}