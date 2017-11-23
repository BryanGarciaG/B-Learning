using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class PromediosXModuloEntidad
    {
        public List<ActividadesResueltasEntidad> actResEnt { get; set; }
        public List<LeccionEntidad> leccionEntidad { get; set; }
        public List<ActividadAsignadaEntidad> actiAsigEnti { get; set; }
    }
}