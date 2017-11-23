using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class VariosModel
    {
        public PreguntaEntidad modelPregunta { get; set; }
        public ApoyoEntidad modelApoyo { get; set; }
        public List<string> modelListaOpciones { get; set; }
        public List<string> modelListaRespuesta { get; set; }
        public List<HttpPostedFileBase> files { get; set; }
        public List<int> idOpcionImg { get; set; }
        public List<ActividadesXDocenteEntidad> listaActXDoc { get; set; }
        public List<LeccionEntidad> listaLeccion { get; set; }
        public ActividadModuloEntidad modelActiModulo { get; set; }
        public List<int> modelListaModu { get; set; }
        public List<int> modelListaLecc { get; set; }
    }
}