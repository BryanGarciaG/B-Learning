using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class EstudiantePromedioEntidad
    {
        public int idEstudiante { get; set; }
        public string nombres { get; set; }
        public decimal promedio { get; set; }
        public decimal? listening { get; set; }
        public decimal? reading { get; set; }
        public decimal? gramar { get; set; }
        public int vecesResuelto { get; set; }
        public int duracion { get; set; }
    }
}