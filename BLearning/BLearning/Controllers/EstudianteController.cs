using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Negocio;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script;


namespace BLearning.Controllers
{
        [RequiereNivel(Nivel.Alumno)]

    public class EstudianteController : Controller
    {
        //
        // GET: /Estudiante/

        public ActionResult Inicio()
        {
            LogicPersona objPersonaEntidad= new LogicPersona();

            if (Session["personaLogin"] != null)
            {
                PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
                Session["Modulo"] = objPersonaEntidad.consultarModuloDeAlumno(objPersona.usuario);
                LogicAcceso _objLogicAcceso = new LogicAcceso();
                _objLogicAcceso.ingresarAcceso(0,DateTime.Now, "Ingreso", objPersona.idPersona,3);
                return View();
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();

            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });
        }

        public ActionResult Actividades(string idLeccionEn, string n)
        { 
            if (Session["personaLogin"] != null)
            {
                LogicEncriptacionMetodos _objDec = new LogicEncriptacionMetodos();
                int idLeccion = int.Parse(_objDec.Decrypt(idLeccionEn));
                n = _objDec.Decrypt(n);
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                ModuloAlumnoEntidad objModuloAlumno = (ModuloAlumnoEntidad)Session["Modulo"];
                LogicActividadLeccion objActividadLeccionEnt = new LogicActividadLeccion();
                List<ActividadLeccionEntidad> listaActividades = new List<ActividadLeccionEntidad>();
                LogicReporteEstudiante _objReporteEtudianteEntidad = new LogicReporteEstudiante();
                Session["idLeccion"] = idLeccion;
                Session["numeroLeccion"] = n;
                ViewBag.numeroLeccion = Session["numeroLeccion"];
                listaActividades = objActividadLeccionEnt.consultarListaActividades(objModuloAlumno.idModulo, idLeccion, objAlumno.idAlumno);
                ViewBag.Calificaciones = _objReporteEtudianteEntidad.CalificacionesDeActividadesXleccion(objAlumno.idAlumno, idLeccion, objModuloAlumno.idModulo);
                
                return View(listaActividades);
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();

            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        public ActionResult Actividad(string idAEn, string t, string est, string indPreEn)
        {
            if (Session["personaLogin"] != null)
            {
                LogicActividad objActividad = new LogicActividad();
                LogicPregunta objPreguntaEnt = new LogicPregunta();
                LogicQuiz objQuizEntidad = new LogicQuiz();
                LogicEncriptacionMetodos _objDe = new LogicEncriptacionMetodos();
                int idA = int.Parse(_objDe.Decrypt(idAEn));
                int indPre = int.Parse(_objDe.Decrypt(indPreEn));
                if (indPre == 0)
                {
                    Session["efectividad"] = null;
                    Session["efectividadObtenida"] = null;
                    Session["calif"] = null;
                }
                t = _objDe.Decrypt(t);
                est = _objDe.Decrypt(est);
                Session["tipoActividad"] = t;
                int numPreguntas = objPreguntaEnt.consultarNumeroPreguntasActividad(idA);
                int indexControl = indPre + 1;
                PreguntaEntidad objPregunta = objPreguntaEnt.consultarPregunta(idA, indexControl);
                if (indexControl <= numPreguntas)
                {
                    if (indexControl == numPreguntas)
                        ViewBag.UltP = true;
                    else
                        ViewBag.UltP = false;    
                    
                    ViewBag.tA = t;
                    ViewBag.LeccionId = Session["idLeccion"];
                    ViewBag.LeccionNo = Session["numeroLeccion"];
                    ViewBag.EstadoActividad = est;
                    ViewBag.idAct = idA;
                    ViewBag.indexControl = indexControl;
                    ViewBag.numUltimaPre = numPreguntas;
                    if (t == "P")
                    { ViewBag.TipoAct = "Practical"; }
                    else
                    { ViewBag.TipoAct = "Evaluative"; }
                }
                return View(objPregunta);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();

            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        public ActionResult Lecciones()
        {
            if (Session["personaLogin"] != null)
            {
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                LogicLeccion objLeccionEnt = new LogicLeccion();
                ModuloAlumnoEntidad objModuloAlumno = (ModuloAlumnoEntidad)Session["Modulo"];
                List<LeccionEntidad> listaLecciones = new List<LeccionEntidad>();
                if (objModuloAlumno.estado != 3)
                {
                    objModuloAlumno.idModulo = 0;
                }
                else
                { listaLecciones = objLeccionEnt.consultarLecciones(objModuloAlumno.nuemeroNivel); }
                LogicReporteDocente _objReporteDocenteEntidad = new LogicReporteDocente();
                decimal? calificaacionTotal = _objReporteDocenteEntidad.consultarCalificacion(objAlumno.idAlumno, objModuloAlumno.idModulo);
                ViewData["puntos"] = calificaacionTotal;
                ViewBag.Nivel = objModuloAlumno.nuemeroNivel;
                LogicReporteEstudiante objReporteEntidad = new LogicReporteEstudiante();
                ViewData["numLecciones"] = objReporteEntidad.CantidadDeLeccioneXcompletar(objModuloAlumno.idModulo, objAlumno.idAlumno);
                return View(listaLecciones);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        [HttpPost]
        public ActionResult ObtieneElementos(List<DatosDeVerificacionRespuestaEntidad> ListRespuesta, int duracion, int indexControl, int idActividad, bool ultimaPregunta)
        {
            if (Session["personaLogin"] != null)
            {
                if (Session["numeroLeccion"]!=null && Session["numeroLeccion"]!=null)
                {
                    //Mantiene actiiva la sesion;
                }
                List<string> listVerificacion = new List<string>();
                LogicQuiz objQuizEntidad = new LogicQuiz();
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                if (ultimaPregunta)
                {
                    listVerificacion = objQuizEntidad.IngresarQuiz(ListRespuesta, true, objAlumno.idAlumno, idActividad, 0, 1, duracion);
                    if ((string)Session["tipoActividad"] == "P")
                        califActividadesPracticas(ListRespuesta, listVerificacion, ultimaPregunta);
                }
                else
                {
                    if ((string)Session["tipoActividad"] == "P")
                    {
                        listVerificacion = objQuizEntidad.IngresarQuiz(ListRespuesta, false, objAlumno.idAlumno, idActividad, 0, 0, duracion);
                        califActividadesPracticas(ListRespuesta, listVerificacion, ultimaPregunta);
                    }else
                        listVerificacion = objQuizEntidad.IngresarQuiz(ListRespuesta, false, objAlumno.idAlumno, idActividad, indexControl, 0, duracion);
                }
                
                var result = new {data= listVerificacion};
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });
        }

        private void califActividadesPracticas(List<DatosDeVerificacionRespuestaEntidad> ListRespuesta, List<string> listVerificacion, bool ultimaPregunta)
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

            List<EfectividadEntidad> _listaEfectividadEntidad = new List<EfectividadEntidad>();
            List<EfectividadEntidad> _listaEfectividadObtenidaEntidad = new List<EfectividadEntidad>();
            int indice = 0;
            if (Session["efectividad"] != null)
            {
                _listaEfectividadEntidad = (List<EfectividadEntidad>)Session["efectividad"];
                _listaEfectividadObtenidaEntidad = (List<EfectividadEntidad>)Session["efectividadObtenida"];
            }
            if (_listaEfectividadEntidad.Exists(C => C.tipoEfectividad == objPregunta.destreza))
            {
                indice = _listaEfectividadEntidad.FindIndex(C => C.tipoEfectividad == objPregunta.destreza);
                _listaEfectividadEntidad[indice].porcentaje = _listaEfectividadEntidad[indice].porcentaje + objPregunta.ponderacion;
                _listaEfectividadObtenidaEntidad[indice].porcentaje = _listaEfectividadObtenidaEntidad[indice].porcentaje + calificacionObtenida;
            }
            else
            {
                _listaEfectividadEntidad.Add(new EfectividadEntidad { porcentaje = objPregunta.ponderacion, tipoEfectividad = objPregunta.destreza });
                _listaEfectividadObtenidaEntidad.Add(new EfectividadEntidad { porcentaje = calificacionObtenida, tipoEfectividad = objPregunta.destreza });
            }
            if (ultimaPregunta)
            {
                int contador = 0;
                foreach (var item in _listaEfectividadObtenidaEntidad)
                {
                    item.porcentaje = Math.Round((item.porcentaje * 100) / _listaEfectividadEntidad[contador].porcentaje, 2);
                    contador++;
                }
            }
            Session["efectividad"] = _listaEfectividadEntidad;
            Session["efectividadObtenida"] = _listaEfectividadObtenidaEntidad;
            if (Session["calif"] != null)
                Session["calif"] = Math.Round((decimal)Session["calif"] + calificacionObtenida, 2);
            else
                Session["calif"] = calificacionObtenida;
        }

        /// <summary>
        /// <para>Muestra una vista con la calificación y efectividad por habilidades obtenida en una actividad</para>
        /// </summary>
        /// <param name="idActividadDe">Identificador de la actividad resuelta</param>
        /// <returns></returns>
        public ActionResult ProgresoHabilidades(string idActividadDe)
        {
            if (Session["personaLogin"] != null)
            {
                LogicEncriptacionMetodos _objDe = new LogicEncriptacionMetodos();
                int idActividad = int.Parse(_objDe.Decrypt(idActividadDe));
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                LogicCalificacion objCalifEntidad = new LogicCalificacion();
                List<EfectividadEntidad> lista = new List<EfectividadEntidad>();
                if ((string)Session["tipoActividad"] == "P")
                {
                    ViewBag.calificacion = Session["calif"];
                    lista.AddRange((List<EfectividadEntidad>)Session["efectividadObtenida"]);
                }
                else
                {
                    LogicEfectividad _objLogicEfectividad = new LogicEfectividad();
                    lista = _objLogicEfectividad.EfectividadXactividad(objAlumno.idAlumno, idActividad);
                    ViewBag.calificacion = objCalifEntidad.consultarCalificacionObtenida(objAlumno.idAlumno, idActividad);
                }
                ViewBag.idLeccion = Session["idLeccion"];
                ViewBag.numeroLeccion = Session["numeroLeccion"];
                Session["tipoActividad"] = "";
                Session["efectividad"] = null;
                Session["efectividadObtenida"] = null;
                Session["calif"] = 0;
                return View(lista);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });
        }

        public ActionResult Estadistica()
        {
            if (Session["personaLogin"] != null)
            {
                List<RendimientoLeccionEntidad> _objRendimientoLeccionCalificaciones = new List<RendimientoLeccionEntidad>();
                List<EfectividadEntidad> efectividad = new List<EfectividadEntidad>();
                List<RendimientoLeccionEntidad> _objRendimientoLeccionPracticasCompletadas = new List<RendimientoLeccionEntidad>();
                List<RendimientoLeccionEntidad> _objRendimientoLeccionEvaluativaCompletadas = new List<RendimientoLeccionEntidad>();
                PersonaEntidad objAlumno = (PersonaEntidad)Session["personaLogin"];
                ModuloAlumnoEntidad objModuloAlumno = (ModuloAlumnoEntidad)Session["Modulo"];
                LogicReporteEstudiante objReporteEntidad = new LogicReporteEstudiante();
                _objRendimientoLeccionCalificaciones = objReporteEntidad.CalificacionesXleccion(objModuloAlumno.idModulo, objAlumno.idAlumno);
                efectividad = objReporteEntidad.efectividadDeRespuestasXtipo(objModuloAlumno.idModulo, objAlumno.idAlumno);
                _objRendimientoLeccionPracticasCompletadas = objReporteEntidad.LeccionesCompletadas(objModuloAlumno.idModulo, objAlumno.idAlumno, "P");
                _objRendimientoLeccionEvaluativaCompletadas = objReporteEntidad.LeccionesCompletadas(objModuloAlumno.idModulo, objAlumno.idAlumno, "E");
                LogicReporteDocente _objReporteDocenteEntidad = new LogicReporteDocente();
                decimal? calificaacionTotal = _objReporteDocenteEntidad.consultarCalificacion(objAlumno.idAlumno, objModuloAlumno.idModulo);
                ViewData["puntos"] = calificaacionTotal;
                var result = new ReporteEstudianteEntidad { calificacionesEstudiante = _objRendimientoLeccionCalificaciones, efectividadDeRespuestas = efectividad, leccionesPracticasCompletadas = _objRendimientoLeccionPracticasCompletadas, leccionesEvaluativasCompletadas=_objRendimientoLeccionEvaluativaCompletadas};
                return View(result);
            }

            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("c");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

     
	}
}