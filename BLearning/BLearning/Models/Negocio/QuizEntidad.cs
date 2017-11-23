using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class QuizEntidad
    {
        public int idQuiz { get; set; }
        public int idActividad { get; set; }
        public DateTime fecha { get; set; }
        public int idEstudiante { get; set; }
        public bool completado { get; set; }
        public int indicePregunta { get; set; }
        public int duracion { get; set; }
        public int nVecesCompletado { get; set; }
    }
}