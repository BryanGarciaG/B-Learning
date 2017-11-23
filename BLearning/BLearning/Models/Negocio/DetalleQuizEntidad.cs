using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class DetalleQuizEntidad
    {
        public int idDetalleQuiz { get; set; }
        public int idOpciones { get; set; }
        public string detalleRespuesta { get; set; }
        public int idQuiz { get; set; }

        public DetalleQuizEntidad()
        {

        }
    }
}