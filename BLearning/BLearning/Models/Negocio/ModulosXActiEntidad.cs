using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class ModulosXActiEntidad
    {
        public int idActModulo { get; set; }
        public string numNivel { get; set; }
        public string numPara { get; set; }
        public string numLeccion { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string tipo { get; set; }
    }
}