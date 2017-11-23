using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class DetalleCalificacionEntidad
    {
        public int idDetalleCalificacion { get; set; }
        public decimal calificacion { get; set; }
        public string destreza { get; set; }
        public int idCalificacion { get; set; }
    }
}