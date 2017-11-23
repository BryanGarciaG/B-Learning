using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ActividadLeccionEntidad
    {
        public int idActModulo { get; set; }
        public int idModulo { get; set; }
        public ActividadEntidad idActividad { get; set; }
        public DateTime fechaInicio {get; set;}
        public DateTime fechAFin { get; set; }
        public int IdLeccion { get; set; }
        public int nVecesResuelta { get; set; }
        public int indicePregunta { get; set;}
        public string tipo { get; set; }
    }
}