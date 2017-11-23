using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class TiempoTrabajoEntidad
    {
        public string label { get; set; }
        public decimal tiempo { get; set; }
        public string docente { get; set; }
        public string tiempoString { get; set; }
    }
}