using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Negocio;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Controllers
{
    [RequiereNivel(Nivel.Docente)]

    public class DocenteController : Controller
    {
        //
        // GET: /Docente/


        LogicEncriptacionMetodos objEncrip = new LogicEncriptacionMetodos();

        public ActionResult Inicio()
        {
            LogicActividad objActividadEntidad = new LogicActividad();
            if (Session["personaLogin"] != null)
            {
                LogicDocenteCursos objDocenteCursosE = new LogicDocenteCursos();
                PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
                List<DocenteCursosEntidad> cursos = objDocenteCursosE.consultarCursosDocente(objPersona.idPersona);
                LogicAcceso _objLogicAcceso = new LogicAcceso();
                _objLogicAcceso.ingresarAcceso(0, DateTime.Now, "Ingreso", objPersona.idPersona, 3);
                Session["cursos"] = cursos;

                return View();
            }
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });

        }

        public ActionResult Manual()
        {
            if (Session["personaLogin"] == null)
            {
                var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });
            }
            return View();
        }

        /// <summary>Metodo de DocenteController
        /// <para>Ver las lecciones que estan asignadas a un modulo/para>
        /// </summary>
        /// <param name="idNivel"></param>
        /// <param name="modulo"></param>
        /// <param name="idModulo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult buscarLecciones(string idNivelEn, string modulo, string idModuloEn)
        {
            if (Session["personaLogin"] != null)
            {
                LogicLeccion objLeccionE = new LogicLeccion();
                LogicReporteDocente _objRDE = new LogicReporteDocente();
                LogicEncriptacionMetodos _objDecrypt = new LogicEncriptacionMetodos();
                int idNivel = int.Parse(_objDecrypt.Decrypt(idNivelEn));
                int idModulo = int.Parse(_objDecrypt.Decrypt(idModuloEn));
                modulo = _objDecrypt.Decrypt(modulo);
                List<EstudiantePromedioEntidad> listPosiciones = _objRDE.ConsultarPosicionesXModulo(idModulo);

                List<EstudiantePromedioEntidad> listPosicionesOrdenada = listPosiciones;
                ViewBag.posiciones = listPosiciones;
                ViewBag.idModulo = idModulo;
                ViewBag.idNivel = idNivel;
                ViewBag.nunModulo = modulo;
                List<LeccionEntidad> listaleccion = objLeccionE.consultarLeccionesYNumActi(idNivel, idModulo);
                return View(listaleccion);
            }
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }

        /// <summary>Metodo de DocenteController
        /// <para>Buscar las actividades asignadas a una leccion</para>
        /// </summary>
        /// <param name="idLeccion"></param>
        /// <returns></returns>
        public ActionResult buscarActivi(string idLeccionEn, string numLeccion, string numModu, string idModuloEn)
        {
            LogicActividad objActividadEntidad = new LogicActividad();
            if (Session["personaLogin"] != null)
            {
                LogicEncriptacionMetodos _objDecrypt = new LogicEncriptacionMetodos();
                int idLeccion = int.Parse(_objDecrypt.Decrypt(idLeccionEn));
                int idModulo = int.Parse(_objDecrypt.Decrypt(idModuloEn));
                numLeccion = _objDecrypt.Decrypt(numLeccion);
                numModu = _objDecrypt.Decrypt(numModu);
                ViewBag.idModulo = idModulo;
                ViewBag.NumLecc = numLeccion;
                ViewBag.CodigoModu = numModu;
                List<ActividadEntidad> listaActividad = objActividadEntidad.actividadesModuloLeccion(idLeccion, idModulo);
                return View(listaActividad);
            }
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }


        /// <summary>Metodo de DocenteController
        /// <para>asignar una actividad a determinado modulo/leccion</para>
        /// </summary>
        /// <param name="actModu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult asignarActividades(VariosModel actModu)
        {
            LogicActividadModulo objActMod = new LogicActividadModulo();
            List<int> idActMod = new List<int>();
            for (int i = 0; i < actModu.modelListaLecc.Count; i++)
            {
                idActMod.Add(objActMod.activModuloCud(int.Parse("3"), int.Parse("0"), actModu.modelListaModu[i], actModu.modelActiModulo.idActividad, actModu.modelActiModulo.fechaInicio, actModu.modelActiModulo.fechaFin, actModu.modelListaLecc[i], actModu.modelActiModulo.tipo));
            }

            return Json(idActMod);
        }

        /// <summary>
        /// <para>Editar los valores de una actividad ya asignada. solo se edita la fecha fin</para>
        /// </summary>
        /// <param name="_fechaFin"></param>
        /// <returns></returns>
        public JsonResult editarAsignacion(VariosModel newAsig)
        {
            LogicActividadModulo objActMod = new LogicActividadModulo();
            objActMod.activModuloCud(int.Parse("2"), newAsig.modelActiModulo.idActModulo, int.Parse("0"), int.Parse("0"), DateTime.Today, newAsig.modelActiModulo.fechaFin, int.Parse("0"), "");
            return Json("");

        }

        /// <summary>Metodo de DocenteController
        /// <para>que el docente pueda revisar/editar las actividades que ha creado</para>
        /// </summary>
        /// <param name="idActiv"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult revisarActividad(int idActiv, string tema, int nuNivel)
        {
            if (Session["personaLogin"] != null)
            {

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
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }

        /// <summary>Metodo de DocenteController
        /// <para>Ver las actividades (evaluativas/practicas) que ha creado un determinado docente</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult verActividadesDocente()
        {
            if (Session["personaLogin"] != null)
            {

                LogicLeccion objLeccEn = new LogicLeccion();
                LogicActividad objActividadEntidad = new LogicActividad();
                List<LeccionEntidad> listLecionesXModulo = new List<LeccionEntidad>();

                List<int> idsNivel = new List<int>();
                foreach (var item in Session["cursos"] as List<DocenteCursosEntidad>)
                {
                    idsNivel.Add(item.idNivel);
                }
                SortedSet<int> listIdNi = new SortedSet<int>(idsNivel);
                foreach (var item in listIdNi)
                {
                    List<LeccionEntidad> tempList = objLeccEn.consultarLeccion(item);
                    foreach (var item2 in tempList)
                    {
                        listLecionesXModulo.Add(new LeccionEntidad
                        {
                            idLeccion = item2.idLeccion,
                            numLeccion = item2.numLeccion,
                            idNivel = item2.idNivel
                        });
                    }
                }
                PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
                List<ActividadesXDocenteEntidad> listAD = objActividadEntidad.actiXDocente(objPersona.idPersona);
                VariosModel objVM = new VariosModel();
                objVM.listaActXDoc = listAD;
                objVM.listaLeccion = listLecionesXModulo;
                return View(objVM);
            }
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }

        /// <summary>Metodo de DocenteController
        /// <para>Consultar las actividades creadas para un nivel</para>
        /// </summary>
        /// <param name="_idNivel">Nivel a consultar</param>
        /// <returns></returns>
        public JsonResult buscarActiXNivel(int _idNivel)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividad objActividadEntidad = new LogicActividad();
            List<ActividadesXDocenteEntidad> listActiXNivel = objActividadEntidad.actividadesXNivel(_idNivel);
            return Json(listActiXNivel);
        }

        /// <summary>Metodo de DocenteController
        /// <para>para consultar los mudulos y lecciones a los que esta asignada una actividad</para>
        /// </summary>
        /// <param name="_idActidad"></param>
        /// <returns></returns>
        public JsonResult buscarModuXActividad(int _idActidad)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividad objActividadEntidad = new LogicActividad();
            List<ModulosXActiEntidad> lisMoXAc = objActividadEntidad.modulXActiv(_idActidad);
            if (lisMoXAc.Count == 0)
            {
                Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(lisMoXAc, JsonRequestBehavior.AllowGet);
        }

        /// <summary>Metodo de DocenteController
        /// <para>Ver la vista donde se crea la actividady todas sus preguntas</para>
        /// </summary>
        /// <returns></returns>
        public ActionResult crearpreguntas()
        {
            if (Session["personaLogin"] != null)
            {
                LogicTipoPregunta _objTipoPre = new LogicTipoPregunta();
                List<TipoPreguntaEntidad> listTP = _objTipoPre.tiposPreguntaConsultar();
                return View(listTP);
            }
            var op = objEncrip.Encrypt("d&a"); return RedirectToAction("Oops", "Login", new { @area = "", N = op });


        }

        /// <summary>Metodo de DocenteController
        /// <para>Guardar los campos tema y tipo de actividad, ademas para regiistrar quien creo la actividad</para>
        /// </summary>
        /// <param name="_actividad"></param>
        /// <returns></returns>
        public ActionResult guardarActividad(ActividadEntidad _actividad)
        {
            if (Session["personaLogin"] == null) { }
            PersonaEntidad objPersona = (PersonaEntidad)Session["personaLogin"];
                
            
            LogicActividad objActividadEntidad = new LogicActividad();
            int idActividad = objActividadEntidad.actividadCud(int.Parse("3"), int.Parse("0"), int.Parse("0"), int.Parse("0"), _actividad.descripcion, _actividad.idNivel);
            objActividadEntidad.actvDocenteCud(int.Parse("3"), int.Parse("0"), _actividad.idPersona, idActividad, DateTime.Now);
            return Json(idActividad);
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
                    if (i==arrayPO.Length/2)
                    {
                        break;
                    }
                    _listOPs.Add(arrayPO[1, i]);
                }
            }
            var result = new { ipPre = idP, idApo = idApoyo, arrayOpc = _listOPs };
            return Json(result, JsonRequestBehavior.AllowGet);
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

        /// <summary>Metodo de DocenteControler
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

        /// <summary>Metodo de DocenteControler
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
        /// <summary>
        /// <para>Para saber si la actividad asignada ya ha sido resuelta y poderla eliminar</para>
        /// </summary>
        /// <param name="_idActiModu">ID de la asignacion a eliminar</param>
        /// <returns></returns>
        public JsonResult eliminarAsignacion(int _idActiModu)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividadModulo objAM = new LogicActividadModulo();
            return Json(objAM.asignacionResuelta(_idActiModu));
        }

        public JsonResult actividadPonderacion(int _idActividad)
        {
            if (Session["personaLogin"] == null) { }
            LogicActividad objAE = new LogicActividad();
            decimal ponde = objAE.actividadPonderacion(_idActividad);
            return Json(ponde);
        }

        public JsonResult eliminarApoyo(int _idApoyo, int _idActividad, int _idPregunta)
        {
            if (Session["personaLogin"] == null) { }
            LogicApoyo objApoyoE = new LogicApoyo();
            objApoyoE.eliminarApoyoPregunta(_idPregunta);
            if (objApoyoE.apoyoContar(_idActividad, _idApoyo) == 0)
            {
                objApoyoE.apoyoEliminar(_idApoyo);
                return Json("Deleted");
            }

            return Json("No deleted");
        }

        public JsonResult reportarActividad(int _idActividad,int _idPersLog)
        {
            if (Session["personaLogin"] == null) { }
            
            LogicActividad _objActiEnti = new LogicActividad();
            _objActiEnti.reportarActividad(1, 0, _idActividad, _idPersLog, DateTime.Today, false);
            return Json("");
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
    }
}