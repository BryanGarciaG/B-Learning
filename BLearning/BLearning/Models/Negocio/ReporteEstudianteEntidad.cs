using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ReporteEstudianteEntidad
    {
        public List<RendimientoLeccionEntidad> calificacionesEstudiante { get; set; }
        public List<EfectividadEntidad> efectividadDeRespuestas { get; set; }
        public List<RendimientoLeccionEntidad> leccionesPracticasCompletadas { get; set; }
        public List<RendimientoLeccionEntidad> leccionesEvaluativasCompletadas { get; set; }
    }
}