using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ActividadesResueltasEntidad
    {
        public int idAlumno { get; set; }
        public string nombres { get; set; }
        public int cantidadActiResu { get; set; }
        public List<RendimientoLeccionEntidad> objProLecc { get; set; }
    }
}