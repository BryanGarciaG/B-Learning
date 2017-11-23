using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Negocio;

namespace BLearning.Controllers
{
    [RequiereNivel(Nivel.Administradores)]

    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/


        public ActionResult Inicio()
        {
            LogicPersona objPersonaEntidad = new LogicPersona();
            if (Session["personaLogin"] == null)
            {
                LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos(); var op = _objSeguridad.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });

            }
            PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
            LogicAcceso _objLogicAcceso = new LogicAcceso();
            _objLogicAcceso.ingresarAcceso(0, DateTime.Now, "Ingreso", objPersona.idPersona, 3);
            return View();
        }

        public ActionResult Configuraciones()
        {
            if (Session["personaLogin"] == null)
            {
                LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos(); var op = _objSeguridad.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });

            }
            LogicLeccion _objLE = new LogicLeccion();
            List<LeccionesXNivelEntidad> listLeccionXNivel = _objLE.consultarLeccionesAllNivel();
            return View(listLeccionXNivel);
        }

        public JsonResult actualizarLeccionesActivas(int idNivel, int cantLeccion)
        {
            if (Session["personaLogin"] == null) { }
            LogicLeccion _objLE = new LogicLeccion();
            _objLE.actualizarEstadoLeccion(idNivel, cantLeccion);
            return Json('a');
        }

        public ActionResult Estadistica()
        {
            if (Session["personaLogin"] == null)
            {
                LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos(); var op = _objSeguridad.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });


            }
            LogicCiclo _objLogicCiclo = new LogicCiclo();
            ViewData["Ciclos"] = _objLogicCiclo.consultarCiclos();
            LogicCarrera _objLogicCarrera = new LogicCarrera();
            ViewData["Carreras"] = _objLogicCarrera.consultarCarreras();
            return View();
        }

        [HttpPost]
        public ActionResult CargarTipoModulo(int ciclo)
        {
            if (Session["personaLogin"] == null) { }

            LogicTipoModulo _objLogicTiposModulo = new LogicTipoModulo();
            List<TipoModuloEntidad> _objTipoModuloEntidad = _objLogicTiposModulo.consultarTiposModulo(ciclo);
            return Json(_objTipoModuloEntidad);
        }

        [HttpPost]
        public ActionResult CargarNivel(int ciclo, int tipoModulo)
        {
            if (Session["personaLogin"] == null) { }
            LogicNivel _objLogicNivel = new LogicNivel();
            List<NivelEntidad> _objListaNivelEntidad = _objLogicNivel.consultarNivelesXtipoModulo(ciclo, tipoModulo);
            return Json(_objListaNivelEntidad);
        }

        [HttpPost]
        public ActionResult cargarParalelos(int ciclo, int idTipoModulo, string codigo, string nivel)
        {
            if (Session["personaLogin"] == null) { }
            LogicParalelo _objLogicoParalelo = new LogicParalelo();
            List<string> listaParalelo = _objLogicoParalelo.consultarParalelos(codigo, ciclo, idTipoModulo, nivel);
            return Json(listaParalelo);
        }

        [HttpPost]
        public ActionResult consultarXnivel(int nC, string eN, string cM, string eT)
        {
            if (Session["personaLogin"] == null) { }
            LogicEfectividad _objLogicEfectividad = new LogicEfectividad();
            List<EfectividadEntidad> _objListaEfectividadEntidad = _objLogicEfectividad.EfectividadXcicloYnivel(nC, eN, cM, eT);
            List<EfectividadEntidad> _objListaEfectividadEntidad1 = _objLogicEfectividad.EfectividadComparativaXparalelos(nC, eN, cM, eT);
            LogicTiempoTrabajo _objLogicTiempoTrabajo = new LogicTiempoTrabajo();
            List<TiempoTrabajoEntidad> _objListaTiempoTrabajoEntidad = _objLogicTiempoTrabajo.TiempoTrabajoXcicloYnivel(nC, eN, cM, eT);
            decimal tiempoTrabajado = 0;
            tiempoTrabajado = _objLogicTiempoTrabajo.calcularTiempoPromedio(_objListaTiempoTrabajoEntidad, tiempoTrabajado);
            var result = new { dataEfectividad = _objListaEfectividadEntidad, tiempoParalelo = _objListaTiempoTrabajoEntidad, tiempoTotal = _objLogicTiempoTrabajo.convertirMinutosAhorasMinutos(tiempoTrabajado), efecTividadComparativa = _objListaEfectividadEntidad1 };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult consultarXcicloNivelYparalelo(int idCiclo, int idNivel, string cM, string eT, string p)
        {
            if (Session["personaLogin"] == null) { }
            LogicEfectividad _objLogicEfectividad = new LogicEfectividad();
            if (Convert.ToInt32(p) < 10)
                p = "0" + p;
            List<EfectividadEntidad> _objListaEfectividadEntidad = _objLogicEfectividad.EfectividadXcicloNivelYparalelo(null, idCiclo, idNivel, cM, eT, p);
            LogicParalelo _objLogicoParalelo = new LogicParalelo();
            string docente = _objLogicoParalelo.ConsultaDocenteXmodulo(idCiclo, idNivel, cM, eT, p);
            LogicTiempoTrabajo _objLogicTiempoTrabajo = new LogicTiempoTrabajo();
            List<TiempoTrabajoEntidad> _objTiempoTrabajoEntidad = _objLogicTiempoTrabajo.TiempoTrabajoXparalelo(null, idCiclo, idNivel, cM, eT, p);
            decimal tiempoTrabajado = 0;
            tiempoTrabajado = _objLogicTiempoTrabajo.calcularTiempoPromedio(_objTiempoTrabajoEntidad, tiempoTrabajado);
            var result = new { dataEfectividad = _objListaEfectividadEntidad, docenteM = docente, tiempoParalelo = _objTiempoTrabajoEntidad, tt = tiempoTrabajado, tts = _objLogicTiempoTrabajo.convertirMinutosAhorasMinutos(tiempoTrabajado) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult consultarXcarrera(int nC, string eN, string cM, string eT, int ca)
        {
            if (Session["personaLogin"] == null) { }
            LogicEfectividad _objLogicEfectividad = new LogicEfectividad();
            List<EfectividadEntidad> _objListaEfectividadEntidad = _objLogicEfectividad.EfectividadXcicloNivelYcarrera(nC, eN, cM, eT, ca);
            LogicTiempoTrabajo _objLogicTiempoTrabajo = new LogicTiempoTrabajo();
            List<TiempoTrabajoEntidad> _objTiempoTrabajoEntidad = _objLogicTiempoTrabajo.TiempoTrabajoXcarrera(nC, eN, cM, eT);
            var result = new { dataEfectividad = _objListaEfectividadEntidad, tiempoParalelo = _objTiempoTrabajoEntidad };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Metodo de DocenteController
        /// <para>Ver las actividades (evaluativas/practicas) que ha creado un determinado docente</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult verActividadesAdmin()
        {
            if (Session["personaLogin"] != null)
            {

                PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
                LogicActividad objActividadEntidad = new LogicActividad();
                List<ActividadesXDocenteEntidad> listAD = objActividadEntidad.actiXDocente(objPersona.idPersona);
                List<ActividadesXDocenteEntidad> listAXR = objActividadEntidad.actividadesXRevisar();
                ViewBag.ActiXRev = listAXR;
                VariosModel objVM = new VariosModel();
                objVM.listaActXDoc = listAD;
                return View(objVM);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("d&a");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        /// <summary>Metodo de DocenteController
        /// <para>Ver la vista donde se crea la actividady todas sus preguntas</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult crearpreguntasAdmin()
        {
            if (Session["personaLogin"] != null)
            {
                LogicTipoPregunta _objTipoPre = new LogicTipoPregunta();
                List<TipoPreguntaEntidad> listTP = _objTipoPre.tiposPreguntaConsultar();
                return View(listTP);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos(); var op = _objSeguridad.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        public ActionResult guardarActividad(ActividadEntidad _actividad)
        {
            if (Session["personaLogin"] == null) { }


            LogicActividad objActividadEntidad = new LogicActividad();
            int idActividad = objActividadEntidad.actividadCud(int.Parse("3"), int.Parse("0"), int.Parse("0"), int.Parse("0"), _actividad.descripcion, _actividad.idNivel);
            objActividadEntidad.actvDocenteCud(int.Parse("3"), int.Parse("0"), _actividad.idPersona, idActividad, DateTime.Now);
            return Json(idActividad);
        }

        /// <summary>Metodo de DocenteController
        /// <para>Guardar cada pregunta creada y su apoyo, si es que existe</para>
        /// </summary>
        /// <param name="_pregunta"></param>
        /// <returns></returns>
        public JsonResult guardarPregunta(VariosModel _pregunta)
        {
            if (Session["personaLogin"] == null) { }
            int idApoyo = 0;
            LogicApoyo objApoyoEntidad = new LogicApoyo();
            LogicActividad objActividadEntidad = new LogicActividad();
            LogicPregunta objPreguntaEntidad = new LogicPregunta();
            if (_pregunta.modelApoyo != null)
            {
                idApoyo = objApoyoEntidad.gestorApoyo(_pregunta.modelApoyo);
            }
            _pregunta.modelPregunta.idApoyo = idApoyo;
            int[,] arrayPO = objPreguntaEntidad.preguntaCud(_pregunta);
            objActividadEntidad.actividadActualizar(int.Parse("1"), _pregunta.modelPregunta.idActividad, _pregunta.modelPregunta.duracion);
            List<int> _listOPs = new List<int>();
            int idP = arrayPO[0, 0];
            if (_pregunta.modelPregunta.idTipo == 4)
            {
                for (int i = 0; i < arrayPO.Length; i++)
                {
                    if (i == arrayPO.Length / 2)
                    {
                        break;
                    }
                    _listOPs.Add(arrayPO[1, i]);
                }
            }
            var result = new { ipPre = idP, idApo = idApoyo, arrayOpc = _listOPs };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Metodo de DocenteController
        /// <para>que el docente pueda revisar/editar las actividades que ha creado</para>
        /// </summary>
        /// <param name="idActiv"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult revisarActividadAdmin(string idActivEn, string tema, string nuNivelEn)
        {
            if (Session["personaLogin"] != null)
            {
                LogicEncriptacionMetodos _objDecryp = new LogicEncriptacionMetodos();
                int idActiv = int.Parse(_objDecryp.Decrypt(idActivEn));
                int nuNivel = int.Parse(_objDecryp.Decrypt(nuNivelEn));
                tema = _objDecryp.Decrypt(tema);
                LogicPregunta objPregEnti = new LogicPregunta();
                LogicActividad objActE = new LogicActividad();
                LogicTipoPregunta _objTipoPre = new LogicTipoPregunta();
                List<TipoPreguntaEntidad> listTP = _objTipoPre.tiposPreguntaConsultar();
                int isEditable = objActE.actividadResuelta(idActiv);
                ViewBag.TiposPregunta = listTP;
                ViewBag.Tema = tema;
                ViewBag.IsEditable = isEditable;
                ViewBag.nuNivel = nuNivel;
                List<PreguntaEntidad> listPreguntas = objPregEnti.consultarPreguntasXAct(idActiv);
                return View(listPreguntas);
            }
            LogicEncriptacionMetodos _objSeguridad = new LogicEncriptacionMetodos();
            var op = _objSeguridad.Encrypt("d&a");
            return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }

        /// <summary>
        /// <para>Editar los campos cabecera y tipo de una actividad</para>
        /// </summary>
        /// <param name="cabeActiv">Variable actividad a modificar</param>
        /// <returns></returns>
        public JsonResult editarActividad(ActividadEntidad cabeActiv)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividad objActividadEntidad = new LogicActividad();
            int idActividad = objActividadEntidad.actividadCud(int.Parse("2"), cabeActiv.idActividad, int.Parse("0"), int.Parse("0"), cabeActiv.descripcion, cabeActiv.idNivel);
            return Json("ok");
        }

        /// <summary>
        /// <para>Edita una pregunta segun su ID</para>
        /// </summary>
        /// <param name="_pregunta">Actividad a editar</param>
        /// <returns></returns>
        public JsonResult editarPregunta(VariosModel _pregunta)
        {
            if (Session["personaLogin"] == null) { }
            int idApoyo = 0;
            LogicApoyo objApoyoEntidad = new LogicApoyo();
            LogicActividad objActividadEntidad = new LogicActividad();
            LogicPregunta objPreguntaEn = new LogicPregunta();
            if (_pregunta.modelApoyo != null)
            {
                idApoyo = objApoyoEntidad.gestorApoyo(_pregunta.modelApoyo);
            }
            _pregunta.modelPregunta.idApoyo = idApoyo;
            int[,] arrayPO = objPreguntaEn.preguntaCud(_pregunta);
            LogicActividad _objActividadEntidad = new LogicActividad();
            ActividadEntidad _objActividad = _objActividadEntidad.consultarActividad(_pregunta.modelPregunta.idActividad);
            int duracionActual = _objActividad.duracion; //duracion actual en la tabla actividad... campo que se actualiza
            PreguntaEntidad _objPE = objPreguntaEn.consultarPreguntaIdPregunta(_pregunta.modelPregunta.idPregunta);
            int duracionOldP = _objPE.duracion;
            int duracionNewP = 0;
            int duracionGuardar = 0;
            if (duracionOldP >= _pregunta.modelPregunta.duracion)
            {
                duracionNewP = duracionOldP - _pregunta.modelPregunta.duracion;
                duracionGuardar = duracionActual - duracionNewP;
                objActividadEntidad.actividadActualizar(int.Parse("3"), _pregunta.modelPregunta.idActividad, duracionGuardar);
            }
            else
            {
                duracionNewP = _pregunta.modelPregunta.duracion - duracionOldP;
                duracionGuardar = duracionActual + duracionNewP;
                objActividadEntidad.actividadActualizar(int.Parse("3"), _pregunta.modelPregunta.idActividad, duracionGuardar);
            }
            objPreguntaEn.preguntaEliminar(_pregunta.modelPregunta.idPregunta);
            List<int> _listOPs = new List<int>();
            int idP = arrayPO[0, 0];
            if (_pregunta.modelPregunta.idTipo == 4)
            {
                for (int i = 0; i < arrayPO.Length; i++)
                {
                    _listOPs.Add(arrayPO[1, i]);
                }
            }
            var result = new { ipPre = idP, idApo = idApoyo, arrayOpc = _listOPs };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Metodo de AdministradorControler
        /// <para>Editarlas imagenes de una pregunta de tipo imgen</para>
        /// </summary>
        /// <param name="_pregunta"></param>
        /// <returns></returns>
        public JsonResult editarPreguntaImg(VariosModel _pregunta)
        {
            if (Session["personaLogin"] == null) { }
            int idApoyo = 0;
            LogicApoyo objApoyoEntidad = new LogicApoyo();
            LogicActividad objActividadEntidad = new LogicActividad();
            LogicPregunta objPreguntaEn = new LogicPregunta();
            if (_pregunta.modelApoyo != null)
            {
                idApoyo = objApoyoEntidad.gestorApoyo(_pregunta.modelApoyo);
            }
            _pregunta.modelPregunta.idApoyo = idApoyo;
            List<int> _listOP = objPreguntaEn.editarPreguntaCudIMG(_pregunta);
            int idPr = _pregunta.modelPregunta.idPregunta;
            LogicActividad _objActividadEntidad = new LogicActividad();
            ActividadEntidad _objActividad = _objActividadEntidad.consultarActividad(_pregunta.modelPregunta.idActividad);
            int duracionActual = _objActividad.duracion; //duracion actual en la tabla actividad... campo que se actualiza
            PreguntaEntidad _objPE = objPreguntaEn.consultarPreguntaIdPregunta(_pregunta.modelPregunta.idPregunta);
            int duracionOldP = _objPE.duracion;
            int duracionNewP = 0;
            int duracionGuardar = 0;
            if (duracionOldP >= _pregunta.modelPregunta.duracion)
            {
                duracionNewP = duracionOldP - _pregunta.modelPregunta.duracion;
                duracionGuardar = duracionActual - duracionNewP;
                objActividadEntidad.actividadActualizar(int.Parse("3"), _pregunta.modelPregunta.idActividad, duracionGuardar);
            }
            else
            {
                duracionNewP = _pregunta.modelPregunta.duracion - duracionOldP;
                duracionGuardar = duracionActual + duracionNewP;
                objActividadEntidad.actividadActualizar(int.Parse("3"), _pregunta.modelPregunta.idActividad, duracionGuardar);
            }

            var result = new { ipPre = idPr, idApo = idApoyo, arrayOpc = _listOP };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Metodo de AdministradorControler
        /// <para>eliminar la opcion de una pregunta de tipo imagen</para>
        /// </summary>
        /// <param name="idOpcion">id de la opcion</param>
        /// <returns></returns>
        public JsonResult eliminarLiteralImg(int idOpcion)
        {
            if (Session["personaLogin"] == null) { }
            LogicPregunta _objLP = new LogicPregunta();
            _objLP.eliminarOpcionImg(idOpcion);
            return Json(idOpcion);
        }

        public JsonResult eliminarApoyo(int _idApoyo, int _idActividad, int _idPregunta)
        {
            if (Session["personaLogin"] == null) { }
            LogicApoyo objApoyoE = new LogicApoyo();
            objApoyoE.eliminarApoyoPregunta(_idPregunta);
            if (objApoyoE.apoyoContar(_idActividad, _idApoyo) != 0)
            {
                objApoyoE.apoyoEliminar(_idApoyo);
                return Json("Deleted");
            }

            return Json("No deleted");
        }

        /// <summary>
        /// <para>Eliminar una pregunta</para>
        /// </summary>
        /// <param name="_pregunta">Pregunta a eliminar</param>
        /// <returns></returns>
        public JsonResult eliminarPregunta(VariosModel _pregunta)
        {
            if (Session["personaLogin"] == null) { }
            LogicPregunta objPreguntaEn = new LogicPregunta();
            LogicActividad objActividadEntidad = new LogicActividad();
            LogicApoyo objApoyoE = new LogicApoyo();
            objPreguntaEn.preguntaEliminar(_pregunta.modelPregunta.idPregunta);
            objActividadEntidad.actividadActualizar(int.Parse("2"), _pregunta.modelPregunta.idActividad, _pregunta.modelPregunta.duracion);
            if (_pregunta.modelApoyo != null)
            {
                if (_pregunta.modelApoyo.idApoyo != 0)
                {
                    if (objApoyoE.apoyoContar(_pregunta.modelPregunta.idActividad, _pregunta.modelApoyo.idApoyo) != 0)
                    {
                        objApoyoE.apoyoEliminar(_pregunta.modelApoyo.idApoyo);
                    }
                }
            }

            return Json("a");
        }

        /// <summary>
        /// <para>eliminar una actividad</para>
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <returns></returns>
        public JsonResult eliminarActividad(int _idActividad)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividad objActi = new LogicActividad();
            int esBorrable = objActi.actividadResueltaYAsignada(_idActividad);
            if (esBorrable == 0)
            {
                objActi.eliminarActivdad(_idActividad);
            }
            return Json(esBorrable);
        }

        [HttpPost]
        public ActionResult promediosXModulo(int _idTipoModulo, int _ciclo, int _nivel, string _numPara)
        {
            if (Session["personaLogin"] == null) { }
            LogicReporteDocente _objRDE = new LogicReporteDocente();
            LogicLeccion _objLeccionEntidad = new LogicLeccion();
            LogicDocenteCursos _objDCE = new LogicDocenteCursos();
            int idModulo = _objDCE.consultarIdModulo(_idTipoModulo, _ciclo, _nivel, _numPara);
            List<LeccionEntidad> _objListaLeccion = _objLeccionEntidad.consultarLeccionesConActividades(idModulo);
            List<ActividadesResueltasEntidad> listARE = _objRDE.consultarActiResueltasXModulo(idModulo);
            var result = new { listARE = listARE, lecciones = _objListaLeccion, idModu = idModulo };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult actividadRevisada(int opcion, int idActividad)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividad _objAE = new LogicActividad();
            _objAE.actividadRevisada(opcion, idActividad);
            if (opcion == 1)
            {
                return Json(1);
            }
            return Json(2);
        }

        public ActionResult actividadesPerdidas()
        {
            List<ActividadEntidad> _listaActi = new List<ActividadEntidad>();
            LogicActividad _objLA = new LogicActividad();
            _listaActi = _objLA.actividadesPerdidasLista();
            return View(_listaActi);
        }

    }
}