using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class PreguntaEntidad
    {
        public int idPregunta { get; set; }
        public string indicaciones { get; set; }
        public decimal ponderacion { get; set; }
        public int idActividad { get; set; }
        public string destreza { get; set; }
        public int idTipo { get; set; }
        public TipoPreguntaEntidad objTipo { get; set; }
        public ApoyoEntidad objApoyo { get; set; }
        public int? idApoyo { get; set; }
        public int duracion { get; set; }

        public bool mostarOpciones { get; set; }
        public List<OpcionesEntidad> listaOpciones { get; set; }
        public List<RespuestaEntidad> listaRespuesta { get; set; }
        public List<RespuestaEntidad> listaRespuestaDesordenada { get; set; }
    }
}