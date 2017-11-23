using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BLearning.Models.Negocio
{
    public class DocenteCursosEntidad
    {
        public int idModulo { get; set; }
        public int idNivel { get; set; }
        public string numNivel { get; set; }
        public int idParalelo { get; set; }
        public string numParalelo { get; set; }
    }
}