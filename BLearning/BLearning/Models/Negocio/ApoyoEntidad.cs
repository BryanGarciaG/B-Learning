using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ApoyoEntidad
    {
        public int idApoyo { get; set; }
        public string titulo { get; set; }
        public string enunciado { get; set; }
        public string link { get; set; }
        public string imagen { get; set; }
        public HttpPostedFileBase imagenImagen { get; set; }
        public string audio { get; set; }
        public HttpPostedFileBase audioAudio { get; set; }


    }
}