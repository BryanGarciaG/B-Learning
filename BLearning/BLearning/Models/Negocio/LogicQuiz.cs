using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BLearning.Models.Negocio
{
    public class LogicQuiz
    {
        MetodosConsultar objConsultasDB = new MetodosConsultar();
        MetodosIngreso objIngresosDB = new MetodosIngreso();

        public QuizEntidad consultarQuizPorActividadYestudiante(int idAct, int idEst)
        {
            QuizEntidad objQuiz = new QuizEntidad();
            DataTable quiz = objConsultasDB.consultarQuizPorActividad(idAct, idEst);
            if (quiz.Rows.Count != 0)
            {
                foreach (DataRow item in quiz.Rows)
                {
                    objQuiz.idQuiz = Convert.ToInt32(item.ItemArray[0]);
                    objQuiz.idActividad = Convert.ToInt32(item.ItemArray[1]);
                    objQuiz.fecha = Convert.ToDateTime(item.ItemArray[2]);
                    objQuiz.idEstudiante = Convert.ToInt32(item.ItemArray[3]);
                    objQuiz.completado = Convert.ToBoolean(item.ItemArray[4]);
                    objQuiz.indicePregunta = Convert.ToInt32(item.ItemArray[5]);
                    objQuiz.duracion = Convert.ToInt32(item.ItemArray[6]);
                    objQuiz.nVecesCompletado = Convert.ToInt32(item.ItemArray[7].ToString());
                }
            }

            return objQuiz;
        }
        public DetalleQuizEntidad consultarDetalleQuizPorOpcionYquiz(int idOpt, int idQuiz)
        {
            DetalleQuizEntidad objDetalleQ = new DetalleQuizEntidad();
            DataTable detalleQuiz = objConsultasDB.consultarDetalleQuizPorQuizYopcion(idQuiz, idOpt);
            if (detalleQuiz.Rows.Count != 0)
            {
                foreach (DataRow item in detalleQuiz.Rows)
                {
                    objDetalleQ.idDetalleQuiz = int.Parse(item.ItemArray[0].ToString());
                    objDetalleQ.idOpciones = int.Parse(item.ItemArray[1].ToString());
                    objDetalleQ.detalleRespuesta = item.ItemArray[2].ToString();
                }
            }

            return objDetalleQ;
        }

        public List<string> IngresarQuiz(List<DatosDeVerificacionRespuestaEntidad> ListaRespuesta, bool completado, int idEstudiante, int idActModulo, int indicePregunta, int? nVecesResuelta, int duracion)
        {
            LogicActividad objActEntidad = new LogicActividad();
            LogicRespuesta objRespuesta = new LogicRespuesta();
            ActividadModuloEntidad objActi = objActEntidad.consultarActividadModuloXidActMod(idActModulo);
            LogicPregunta objPreguntaEnt = new LogicPregunta();
            List<string> listaVerificacion = new List<string>();
            var tipoPregunta = "";
            int idPregunta = 0;
            int idQ = 0;
            int contador = 0;
            string respuesta = "";
            string temporal = "";
            QuizEntidad objQuiz = consultarQuizPorActividadYestudiante(idActModulo, idEstudiante);
            DetalleQuizEntidad objDetalleQuiz = new DetalleQuizEntidad();
            if (objQuiz.idQuiz != 0)
            {
                if (consultarDetalleQuizPorOpcionYquiz(ListaRespuesta[0].idOpcionPregunta, objQuiz.idQuiz).idOpciones != 0 && objActi.tipo == "E")
                    return listaVerificacion;
                else
                {
                    //Actualiza la cabecera del quiz 
                    objIngresosDB.ingresarQuiz(objQuiz.idQuiz, idActModulo, idEstudiante, DateTime.Now, completado, indicePregunta, duracion, nVecesResuelta, 2);
                    idQ = objQuiz.idQuiz;
                }
                
            }
            else
            {
                //Ingresa la cabecera del quiz
                DataTable identificadorQuiz = objIngresosDB.ingresarQuiz(0, idActModulo, idEstudiante, DateTime.Now, completado, indicePregunta, duracion, nVecesResuelta,3);
                idQ = Convert.ToInt32(identificadorQuiz.Rows[0].ItemArray[0]);
            }
            //Ingresa el detalle del quiz
            foreach (var item in ListaRespuesta)
            {
                tipoPregunta = item.tipoPregunta;
                idPregunta = item.idPregunta;
                if (item.tipoPregunta == "Emparejamiento" && contador == 0)
                {
                    List<RespuestaEntidad> objResplista = objPreguntaEnt.consultarRespuestas(item.idPregunta);
                    foreach (var item1 in objResplista)
                    {
                        ListaRespuesta[contador].respuestaIngresada = item1.detalleRespuesta;
                        contador++;
                    }
                    contador = 0;
                }
                else
                {
                    //Elimina los espacios en blanco de la respuesta
                    temporal = item.respuestaIngresada.Trim();
                    respuesta = Regex.Replace(temporal, @"\s+", " ");
                    ListaRespuesta[contador].respuestaIngresada = respuesta;
                }
                objDetalleQuiz = consultarDetalleQuizPorOpcionYquiz(item.idOpcionPregunta, idQ);
                if (objActi.tipo == "P")
                {
                    //Actualiza el detalle del quiz
                    if (objDetalleQuiz.idDetalleQuiz != 0 && tipoPregunta != "Pertenencia")
                        objIngresosDB.ingresarDetalleQuiz(objDetalleQuiz.idDetalleQuiz, item.idOpcionPregunta, item.respuestaIngresada, idQ, 2);
                    //Ingresa el detalle del quiz
                    else if (objDetalleQuiz.idDetalleQuiz != 0 && tipoPregunta == "Pertenencia" && contador == 0)
                    {
                        //Elimina todos los detalles que esten guardados con el idOpcionPregunta de la pregunta
                        objIngresosDB.ingresarDetalleQuiz(0, item.idOpcionPregunta, " ", 0, 1);
                        //Ingresa la nueva respuesta
                        objIngresosDB.ingresarDetalleQuiz(0, item.idOpcionPregunta, item.respuestaIngresada, idQ, 3);
                    }
                    else
                        objIngresosDB.ingresarDetalleQuiz(0, item.idOpcionPregunta, item.respuestaIngresada, idQ, 3);
                }
                else
                {
                    objIngresosDB.ingresarDetalleQuiz(0, item.idOpcionPregunta, item.respuestaIngresada, idQ, 3);
                }
                contador++;
            }

            listaVerificacion = objRespuesta.verificarRespuesta(ListaRespuesta, tipoPregunta);

            if (objActi.tipo == "E")
            {
                LogicCalificacion objCalificacionEntidad = new LogicCalificacion();
                int totalCorrectas = 0;
                foreach (var item in listaVerificacion)
                {
                    if (item.Contains("C"))
                        totalCorrectas++;
                }
                PreguntaEntidad objPregunta = objPreguntaEnt.consultarPreguntaIdPregunta(idPregunta);
                decimal calificacionPregunta = objCalificacionEntidad.calculoCalificacion(ListaRespuesta.Count(), totalCorrectas, objPregunta.ponderacion);
                objCalificacionEntidad.ingresoEfectividad(idQ, calificacionPregunta, objPregunta.destreza);
            }
            return listaVerificacion;
        }

        public List<string> comprobarQuiz(List<DatosDeVerificacionRespuestaEntidad> ListaRespuesta)
        {
            LogicActividad objActEntidad = new LogicActividad();
            LogicRespuesta objRespuesta = new LogicRespuesta();
            LogicPregunta objPreguntaEnt = new LogicPregunta();
            List<string> listaVerificacion = new List<string>();
            var tipoPregunta = "";
            int contador = 0;
            string respuesta = "";
            string temporal = "";
            foreach (var item in ListaRespuesta)
            {
                tipoPregunta = item.tipoPregunta;
                if (item.tipoPregunta == "Emparejamiento" && contador == 0)
                {
                    List<RespuestaEntidad> objResplista = objPreguntaEnt.consultarRespuestas(item.idPregunta);
                    foreach (var item1 in objResplista)
                    {
                        ListaRespuesta[contador].respuestaIngresada = item1.detalleRespuesta;
                        contador++;
                    }
                    contador = 0;
                }
                else
                {
                    //Elimina los espacios en blanco de la respuesta
                    temporal = item.respuestaIngresada.Trim();
                    respuesta = Regex.Replace(temporal, @"\s+", " ");
                    ListaRespuesta[contador].respuestaIngresada = respuesta;
                }
                contador++;
            }

            listaVerificacion = objRespuesta.verificarRespuesta(ListaRespuesta, tipoPregunta);
            return listaVerificacion;
        }
   
    
    }
   
}