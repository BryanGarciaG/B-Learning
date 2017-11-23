using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ActividadesXDocenteEntidad
    {
        public int idActividad { get; set; }
        public int nPreguntas { get; set; }
        public int duracion { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string creada { get; set; }
        public string Destrezas { get; set; }
        public int idNivel { get; set; }
        public string docente { get; set; }
        public string docenteReporta { get; set; }
        public int idActiRepor { get; set; }
        public string reportadaEl { get; set; }
        public int idDocente { get; set; }
        public int isReportad { get; set; }
    }
}