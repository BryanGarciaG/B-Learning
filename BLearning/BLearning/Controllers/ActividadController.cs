using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Negocio;

namespace BLearning.Controllers
{
    public class ActividadController : Controller
    {
        //
        // GET: /Actividad/
        public static class globalVar
        {
            //public static string numeroLeccion { get; set; }
            //public static int idLeccion { get; set; }
            //public static string tipoActi { get; set; }
            public static decimal calificacion { get; set; }
            public static bool ultP { get; set; }
            public static decimal efectL { get; set; }
            public static decimal efectR { get; set; }
            public static decimal efectG { get; set; }
            public static decimal califTG { get; set; }
            public static decimal califTR { get; set; }
            public static decimal califTL { get; set; }

        }

        public ActionResult Probar(string idAEn, string indPreEn)
        {
            if (Session["personaLogin"] != null)
            {
                LogicActividad objActividad = new LogicActividad();
                LogicPregunta objPreguntaEnt = new LogicPregunta();
                LogicQuiz objQuizEntidad = new LogicQuiz();
                LogicEncriptacionMetodos _objDe = new LogicEncriptacionMetodos();
                int idA = int.Parse(_objDe.Decrypt(idAEn));
                int indPre = int.Parse(_objDe.Decrypt(indPreEn));
                int numPreguntas = objPreguntaEnt.consultarNumeroPreguntas(idA);
                int indexControl = indPre + 1;
                PreguntaEntidad objPregunta = objPreguntaEnt.consultarPreguntaActividad(idA, indexControl);
                if (indexControl <= numPreguntas)
                {
                    if (indexControl == numPreguntas)
                        globalVar.ultP = true;
                   
                    ViewBag.idAct = idA;
                    ViewBag.indexControl = indexControl;
                    ViewBag.numUltimaPre = numPreguntas;
                    ViewBag.UltP = globalVar.ultP;
                }
                return View(objPregunta);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();

            var op = _objSeguridad.Encrypt("d&a");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        [HttpPost]
        public ActionResult comprobrarQuiz(List<DatosDeVerificacionRespuestaEntidad> ListRespuesta)
        {
            if (Session["personaLogin"] != null)
            {
                List<string> listVerificacion = new List<string>();
                LogicQuiz objQuizEntidad = new LogicQuiz();
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                if (globalVar.ultP)
                {
                    listVerificacion = objQuizEntidad.comprobarQuiz(ListRespuesta);
                    califActividadesPracticas(ListRespuesta, listVerificacion);
                }
                else
                {
                    listVerificacion = objQuizEntidad.comprobarQuiz(ListRespuesta);
                    califActividadesPracticas(ListRespuesta, listVerificacion);
                }

                var result = new { data = listVerificacion };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("d&a");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }


        private static void califActividadesPracticas(List<DatosDeVerificacionRespuestaEntidad> ListRespuesta, List<string> listVerificacion)
        {
            LogicCalificacion objCalificacionEntidad = new LogicCalificacion();
            LogicPregunta objPreguntaEnt = new LogicPregunta();
            int totalCorrectas = 0;
            foreach (var item in listVerificacion)
            {
                if (item.Contains("C"))
                    totalCorrectas++;
            }
            PreguntaEntidad objPregunta = objPreguntaEnt.consultarPreguntaIdPregunta(ListRespuesta[0].idPregunta);
            decimal calificacionObtenida = objCalificacionEntidad.calculoCalificacion(ListRespuesta.Count(), totalCorrectas, objPregunta.ponderacion);

            if (objPregunta.destreza == "R")
            {
                globalVar.califTR = globalVar.califTR + objPregunta.ponderacion;
                globalVar.efectR = globalVar.efectR + calificacionObtenida;
            }
            if (objPregunta.destreza == "L")
            {
                globalVar.califTL = globalVar.califTL + objPregunta.ponderacion;
                globalVar.efectL = globalVar.efectL + calificacionObtenida;
            }
            if (objPregunta.destreza == "G")
            {
                globalVar.califTG = globalVar.califTG + objPregunta.ponderacion;
                globalVar.efectG = globalVar.efectG + calificacionObtenida;
            }
            if (globalVar.ultP)
            {
                if (globalVar.efectR != 0)
                    globalVar.efectR = (globalVar.efectR * 100) / globalVar.califTR;
                if (globalVar.efectL != 0)
                    globalVar.efectL = (globalVar.efectL * 100) / globalVar.califTL;
                if (globalVar.efectG != 0)
                    globalVar.efectG = (globalVar.efectG * 100) / globalVar.califTG;
            }
            globalVar.calificacion = globalVar.calificacion + calificacionObtenida;
        }

        public ActionResult Calificacion(string idActividadDe)
        {
            if (Session["personaLogin"] != null)
            {
                LogicEncriptacionMetodos _objDe = new LogicEncriptacionMetodos();
                int idActividad = int.Parse(_objDe.Decrypt(idActividadDe));
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                LogicCalificacion objCalifEntidad = new LogicCalificacion();
                List<EfectividadEntidad> lista = new List<EfectividadEntidad>();
                ViewBag.calificacion = globalVar.calificacion;
                if (globalVar.califTR >= 1)
                {
                    lista.Add(new EfectividadEntidad { tipoEfectividad = "R", porcentaje = globalVar.efectR });
                }
                if (globalVar.califTL >= 1)
                {
                    lista.Add(new EfectividadEntidad { tipoEfectividad = "L", porcentaje = globalVar.efectL });
                }
                if (globalVar.califTG >= 1)
                {
                    lista.Add(new EfectividadEntidad { tipoEfectividad = "L", porcentaje = globalVar.efectG });
                }
                globalVar.calificacion = 0;
                globalVar.califTR = 0;
                globalVar.califTL = 0;
                globalVar.califTG = 0;
                globalVar.efectL = 0;
                globalVar.efectR = 0;
                globalVar.efectG = 0;
                return View(lista);
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("d&a");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }
	}
}