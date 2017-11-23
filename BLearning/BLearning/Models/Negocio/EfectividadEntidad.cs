using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class EfectividadEntidad
    {
        public string tipoEfectividad { get; set; }
        public decimal porcentaje { get; set; }
        public string modulo { get; set; }
        public decimal Listening { get; set; }
        public decimal Reading { get; set; }
        public decimal GrammarVocabulary { get; set; }

    }
}