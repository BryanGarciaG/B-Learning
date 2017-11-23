using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BLearning.Models.Negocio
{
    public class LeccionEntidad
    {
        public int idLeccion { get; set; }
        public string numLeccion { get; set; }
        public int idNivel { get; set; }
        public bool isActive { get; set; }
        public int cantActiAsig { get; set; }
    }
}