using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ActividadEntidad
    {
        
        public int idActividad { get; set; }
        public int nPreguntas { get; set; }
        public int duracion { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public int idNivel { get; set; }
        public string docenteCrea { get; set; }

        public int idPersona { get; set; }
    }
}