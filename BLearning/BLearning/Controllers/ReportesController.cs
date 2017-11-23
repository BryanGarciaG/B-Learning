using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Negocio;
using System.IO;


namespace BLearning.Controllers
{
    public class ReportesController : Controller
    {
        LogicEncriptacionMetodos objEncrip = new LogicEncriptacionMetodos();

        
        // GET: /Reportes/
        [RequiereNivel(Nivel.Docente)]
        public ActionResult Ver()
        {
            if (Session["personaLogin"] != null)
            {
                LogicCiclo _objLogicCiclo = new LogicCiclo();
                ViewData["CicloActual"] = _objLogicCiclo.consultarCiclos()[0].ciclo;
                return View();
            }
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
        }

        /// <summary>
        /// <para>Consultar el promedio de los estudiantes en un ciclo, ciclo/nivel o ciclo/nivel/paralelo</para>
        /// </summary>
        /// <param name="opcion">1 para consultar ciclo/nivel. 2 para consultar ciclo. 3 para ciclo/nivel/paralelo</param>
        /// <param name="idCiclo">id del ciclo a consultar</param>
        /// <param name="idNivel">id del nivel a consultar. si "_opcion" es 1 o 2, esta variable debe ser 0</param>
        /// <param name="idParalelo">id del paralelo a consultar. si "_opcion" es 1 o 2, esta variable debe ser 0</param>
        /// <param name="idTipoModulo">tipo del modulo que se consulta que consulta</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PromediosCalificaciones(int opcion, int idCiclo, int idNivel, string _numParalelo, int idTipoModulo)
        {
            LogicReporteDocente _objRDE = new LogicReporteDocente();
            List<EstudiantePromedioEntidad> listaPosiciones = _objRDE.ConsultarPosicionesPastel(opcion, idCiclo, idNivel, _numParalelo, idTipoModulo);
            return Json(listaPosiciones);
        }

        [HttpPost]
        public ActionResult consultarEfectividaXmodulo(int idModulo)
        {
            LogicEfectividad _objLogicEfectividad = new LogicEfectividad();
            List<EfectividadEntidad> _objListaEfectividadEntidad = _objLogicEfectividad.EfectividadXcicloNivelYparalelo(idModulo, null, null, null, null, null);
            var result = new { data = _objListaEfectividadEntidad };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult consultarTiempoXmodulo(int idModulo)
        {
            LogicTiempoTrabajo _objLogicTiempoTrabajo = new LogicTiempoTrabajo();
            List<TiempoTrabajoEntidad> _objTiempoTrabajoEntidad = _objLogicTiempoTrabajo.TiempoTrabajoXparalelo(idModulo, null, null, null, null, null);
            decimal tiempoTrabajado = 0;
            tiempoTrabajado = _objLogicTiempoTrabajo.calcularTiempoPromedio(_objTiempoTrabajoEntidad, tiempoTrabajado);
            var result = new { tiempoParalelo = _objTiempoTrabajoEntidad, tt = tiempoTrabajado, tts = _objLogicTiempoTrabajo.convertirMinutosAhorasMinutos(tiempoTrabajado)};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult promediosXModulo(int _idModulo)
        {
            LogicReporteDocente _objRDE = new LogicReporteDocente();
            LogicLeccion _objLeccionEntidad = new LogicLeccion();
            List<LeccionEntidad> _objListaLeccion = _objLeccionEntidad.consultarLeccionesConActividades(_idModulo);
            List<ActividadesResueltasEntidad> listARE = _objRDE.consultarActiResueltasXModulo(_idModulo);
            var result = new { listARE = listARE, lecciones = _objListaLeccion };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult reporteActividadLeccion(int idMod)
        {
            LogicActividadAsignada _objLogicActividadAsignada = new LogicActividadAsignada();
            List<ActividadAsignadaEntidad> _objListaActividadAsignadaEntidad = _objLogicActividadAsignada.ConsultarReporteActividadesAsignadasXtipoActividad(idMod);
            return Json(_objListaActividadAsignadaEntidad);
        }

        [HttpPost]
        public ActionResult posicionesXmodulo(int _idModulo)
        {
            LogicReporteDocente _objRDE = new LogicReporteDocente();
            List<EstudiantePromedioEntidad> posiciones = _objRDE.ConsultarPosicionesXModulo(_idModulo);
            return Json(posiciones);
        }

        [RequiereNivel(Nivel.Docente)]
        public ActionResult promediosXModuloActividad(string _idActividadDe, string _idModuloDe, string _titulo)
        {
            if (Session["personaLogin"] == null)
            {
                var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
            }
            LogicReporteDocente _objRDE = new LogicReporteDocente();
            LogicEncriptacionMetodos _objDecrypt = new LogicEncriptacionMetodos();
            int _idActividad = int.Parse(_objDecrypt.Decrypt(_idActividadDe));
            int _idModulo = int.Parse(_objDecrypt.Decrypt(_idModuloDe));
            List<EstudiantePromedioEntidad> listEstudianProme = _objRDE.consultarPromediosXModuloActividad(_idActividad, _idModulo);
            List<EstudiantePromedioEntidad> listEstudianEfec = _objRDE.consultarEfectividadXModuloActividad(_idActividad, _idModulo);

            ViewBag.TituloAct = _titulo;
            ViewBag.PromediosActividad = listEstudianProme;
            ViewBag.EfectividadActividad = listEstudianEfec;
            return View();
        }


        [HttpPost]
        public ActionResult promediosXModuloActividadPractica(int _idActividad, int _idModulo)
        {
            LogicReporteDocente _objRDE = new LogicReporteDocente();
            List<EstudiantePromedioEntidad> listEstudianDone = _objRDE.consultarPromediosXModuloActividadPractica(_idActividad, _idModulo);
            return Json(listEstudianDone);
        }

        [HttpPost]
        public ActionResult efectividad(int idEstudiante, int idModulo)
        {
            LogicReporteEstudiante objReporteEntidad = new LogicReporteEstudiante();
            List<EfectividadEntidad> data = new List<EfectividadEntidad>();
            var result = new { data = objReporteEntidad.efectividadDeRespuestasXtipo(idModulo, idEstudiante) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [RequiereNivel(Nivel.Administradores)]
        public ActionResult HistorialActividades()
        {
            if (Session["personaLogin"] == null)
            {
                var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
            }
            LogicDocenteActividades _objLogicDocenteActividades = new LogicDocenteActividades();
            List<DocenteActividadesEntidad> _objListaDocentesConActividades = new List<DocenteActividadesEntidad>();
            List<ActividadesXDocenteEntidad> _objListaActividadesXdocente = new List<ActividadesXDocenteEntidad>();
            _objListaDocentesConActividades = _objLogicDocenteActividades.Consultar_NumDeActividadesCreadasXciclo();
            foreach (var item in _objListaDocentesConActividades)
            {
                _objListaActividadesXdocente.AddRange(_objLogicDocenteActividades.Consultar_ReporteDeActividadesXDocente(item.idPersona));
                
            }
            ViewData["CollectionActivities"]= _objListaActividadesXdocente;
            LogicCiclo _objLogicCiclo = new LogicCiclo();
            ViewData["CicloActual"] = _objLogicCiclo.consultarCiclos()[0].ciclo;
            return View(_objListaDocentesConActividades);
        }

         public ActionResult consultarTablaDocente(int idPersona)
        {
            LogicDocenteActividades _objLogicDocenteActividades = new LogicDocenteActividades();
            List<ActividadesXDocenteEntidad> _objListaActividadesXdocente = new List<ActividadesXDocenteEntidad>();
            _objListaActividadesXdocente = _objLogicDocenteActividades.Consultar_ReporteDeActividadesXDocente(idPersona);
            return Json(_objListaActividadesXdocente);
        }

        [HttpGet]
         public ActionResult verActividad(int idA)
         {
             if (Session["personaLogin"] == null)
             {
                 var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
             }
             LogicActividad _objActividadEntidad = new LogicActividad();
             ActividadEntidad _objActividad = _objActividadEntidad.consultarActividad(idA);
             ViewData["CabeceraActividad"] = _objActividad;
             LogicPregunta _objPreeguntaLogic = new LogicPregunta();
             List<PreguntaEntidad> _objListaPregunta = _objPreeguntaLogic.consultarPreguntasXAct(idA);
             return View(_objListaPregunta);
         }


        [RequiereNivel(Nivel.Administradores)]
        public ActionResult Historial()
        {
            if (Session["personaLogin"] == null)
            {
                var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
            }
            return View();
        }

        [HttpPost]
        public ActionResult consultarHistorial(string nombres)
    {
        LogicAcceso _objLogicAcceso = new LogicAcceso();
        List<AccesoEntidad> _objListaAcceso = _objLogicAcceso.consultarHistorialDeAcceso(nombres);
        return Json(_objListaAcceso);
    }
        
        [HttpGet]
        public ActionResult creditos(string currentContro)
        {
            if (Session["personaLogin"] == null)
            {
                var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
            }
            ViewBag.Contro = currentContro;
            return View();
        }

        public ActionResult Manual(string currentContro)
        {
            if (Session["personaLogin"] == null)
            {
                var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
            }
            return View();
        }

        [HttpPost]
        public ActionResult predecirNombre(string nombre)
       {
            LogicAcceso _objLogicAcceso = new LogicAcceso();
            List<string> _objListaPersona = _objLogicAcceso.predecirNombre(nombre);
            return Json(_objListaPersona);
        }

        public ActionResult error()
        {
            return View();
        }

        public ActionResult cannotAcces()
        {
            return View();
        }
	}
}