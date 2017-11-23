using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ActividadAsignadaEntidad
    {
        public string tipoActividad { get; set; }
        public List<RendimientoLeccionEntidad> _objListaLeccionesAsignadas { get; set; }
    }
}