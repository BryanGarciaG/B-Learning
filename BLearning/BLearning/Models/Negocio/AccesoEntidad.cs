using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class AccesoEntidad
    {
        public int idAcceso { get; set; }
        public DateTime fechaEntrada { get; set; }
        public int idPersona { get; set; }
        public string tipoAcceso { get; set; }
        public string entrada { get; set; }
        public string nombre { get; set; }
        public string horaEntrada { get; set; }
    }
}