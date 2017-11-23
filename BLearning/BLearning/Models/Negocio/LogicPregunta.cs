using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicPregunta
    {
        MetodosConsultar objConsultasDB = new MetodosConsultar();
        public PreguntaEntidad consultarPreguntaIdPregunta(int idPregunta)
        {
            PreguntaEntidad objPregunta = new PreguntaEntidad();
            LogicTipoPregunta objTipP = new LogicTipoPregunta();
            LogicApoyo objApoyoEnt = new LogicApoyo();

            foreach (DataRow item in objConsultasDB.consultarPreguntaIdPregunta(idPregunta).Rows)
            {
                objPregunta.idPregunta = Convert.ToInt32(item.ItemArray[0]);
                objPregunta.indicaciones = item.ItemArray[1].ToString();
                objPregunta.ponderacion = Convert.ToDecimal(item.ItemArray[2]);
                objPregunta.idActividad = Convert.ToInt32(item.ItemArray[3]);
                objPregunta.destreza = item.ItemArray[4].ToString();
                objPregunta.objTipo = objTipP.consultarTipoPreguntaId(Convert.ToInt32(item.ItemArray[5]));
                objPregunta.duracion = Convert.ToInt32(item.ItemArray[7]);
                objPregunta.mostarOpciones = bool.Parse(item.ItemArray[8].ToString());
            }
            return objPregunta;
        }

        public List<RespuestaEntidad> consultarRespuestas(int _idPregunta)
        {
            List<RespuestaEntidad> listaRespuesta = new List<RespuestaEntidad>();
            foreach (DataRow item in objConsultasDB.consultarRespuestaId(_idPregunta).Rows)
            {
                listaRespuesta.Add(new RespuestaEntidad
                {
                    idRespuesta = Convert.ToInt32(item.ItemArray[0]),
                    idOpciones = Convert.ToInt32(item.ItemArray[1]),
                    detalleRespuesta = item.ItemArray[2].ToString(),
                });

            }
            return listaRespuesta;
        }
        public List<RespuestaEntidad> DesordenarListaRespuesta(List<RespuestaEntidad> listAdesordenar)
        {
            List<RespuestaEntidad> listaEntrada = listAdesordenar;
            List<RespuestaEntidad> listaDesordenada = new List<RespuestaEntidad>();

            Random randNum = new Random();
            while (listaEntrada.Count > 0)
            {
                int val = randNum.Next(0, listaEntrada.Count - 1);
                listaDesordenada.Add(listaEntrada[val]);
                listaEntrada.RemoveAt(val);
            }

            return listaDesordenada;
        }

        public PreguntaEntidad consultarPreguntaActividad(int _idActividad, int _numPregunta)
        {
            PreguntaEntidad objPregunta = new PreguntaEntidad();
            LogicTipoPregunta objTipoPreg = new LogicTipoPregunta();
            LogicApoyo objApoyo = new LogicApoyo();
            TipoPreguntaEntidad objTip = new TipoPreguntaEntidad();
            foreach (DataRow item in objConsultasDB.consultarPreguntaActividad(_idActividad, _numPregunta).Rows)
            {
                objTip = objTipoPreg.consultarTipoPreguntaId(Convert.ToInt32(item.ItemArray[6]));
                objPregunta.idPregunta = Convert.ToInt32(item.ItemArray[1]);
                objPregunta.indicaciones = item.ItemArray[2].ToString();
                objPregunta.ponderacion = Convert.ToDecimal(item.ItemArray[3]);
                objPregunta.idActividad = Convert.ToInt32(item.ItemArray[4]);
                objPregunta.destreza = item.ItemArray[5].ToString();
                objPregunta.objTipo = objTip;
                objPregunta.objApoyo = objApoyo.consultarApoyoId(Convert.ToInt32(item.ItemArray[7].ToString()));
                objPregunta.listaOpciones = consultarOpciones(Convert.ToInt32(item.ItemArray[1]), objTip.tipo);
                objPregunta.listaRespuesta = consultarRespuestas(Convert.ToInt32(item.ItemArray[1]));
                objPregunta.listaRespuestaDesordenada = DesordenarListaRespuesta(consultarRespuestas(Convert.ToInt32(item.ItemArray[1])));
                objPregunta.duracion = Convert.ToInt32(item.ItemArray[8].ToString());
                objPregunta.mostarOpciones = bool.Parse(item.ItemArray[9].ToString());
            }
            return objPregunta;
        }

        public PreguntaEntidad consultarPregunta(int _idActividad, int _numPregunta)
        {
            PreguntaEntidad objPregunta = new PreguntaEntidad();
            LogicTipoPregunta objTipoPreg = new LogicTipoPregunta();
            LogicApoyo objApoyo = new LogicApoyo();
            TipoPreguntaEntidad objTip = new TipoPreguntaEntidad();
            foreach (DataRow item in objConsultasDB.consultarPregunta(_idActividad, _numPregunta).Rows)
            {
                objTip = objTipoPreg.consultarTipoPreguntaId(Convert.ToInt32(item.ItemArray[6]));
                objPregunta.idPregunta = Convert.ToInt32(item.ItemArray[1]);
                objPregunta.indicaciones = item.ItemArray[2].ToString();
                objPregunta.ponderacion = Convert.ToDecimal(item.ItemArray[3]);
                objPregunta.idActividad = Convert.ToInt32(item.ItemArray[4]);
                objPregunta.destreza = item.ItemArray[5].ToString();
                objPregunta.objTipo = objTip;
                objPregunta.objApoyo = objApoyo.consultarApoyoId(Convert.ToInt32(item.ItemArray[7].ToString()));
                objPregunta.listaOpciones = consultarOpciones(Convert.ToInt32(item.ItemArray[1]), objTip.tipo);
                objPregunta.listaRespuesta = consultarRespuestas(Convert.ToInt32(item.ItemArray[1]));
                objPregunta.listaRespuestaDesordenada = DesordenarListaRespuesta(consultarRespuestas(Convert.ToInt32(item.ItemArray[1])));
                objPregunta.duracion = Convert.ToInt32(item.ItemArray[8].ToString());
                objPregunta.mostarOpciones = bool.Parse(item.ItemArray[9].ToString());

            }
            return objPregunta;
        }
        public int consultarNumeroPreguntasActividad(int _idActMod)
        {
            int numeroPreguntas = 0;
            foreach (DataRow item in objConsultasDB.consultarNumeroPreguntasActividad(_idActMod).Rows)
            {
                numeroPreguntas = Convert.ToInt32(item.ItemArray[0]);
            }
            return numeroPreguntas;
        }

        public int consultarNumeroPreguntas(int _idActividad)
        {
            int numeroPreguntas = 0;
            foreach (DataRow item in objConsultasDB.consultarNumeroPreguntas(_idActividad).Rows)
            {
                numeroPreguntas = Convert.ToInt32(item.ItemArray[0]);
            }
            return numeroPreguntas;
        }


        public List<OpcionesEntidad> consultarOpciones(int _idPregunta, string _tipoPregunta)
        {
            List<OpcionesEntidad> listaOpciones = new List<OpcionesEntidad>();

            foreach (DataRow item in objConsultasDB.consultarOpciones(_idPregunta).Rows)
            {
                listaOpciones.Add(new OpcionesEntidad
                {
                    idOpciones = Convert.ToInt32(item.ItemArray[0]),
                    detallePregunta = item.ItemArray[1].ToString(),
                    idCabeceraPregunta = Convert.ToInt32(item.ItemArray[2])
                });
            }
            return listaOpciones;
        }


        //bryan
        MetodosIngreso objIngreso = new MetodosIngreso();

        public List<PreguntaEntidad> consultarPreguntasXAct(int _idActi)
        {
            List<PreguntaEntidad> listaPregunta = new List<PreguntaEntidad>();
            LogicTipoPregunta objTipoPreg = new LogicTipoPregunta();
            LogicApoyo objApoyo = new LogicApoyo();
            string destreza = "";

            foreach (DataRow item in objConsultasDB.consultarPreguntasXActividad(_idActi).Rows)
            {
                char itemDeztreza = Convert.ToChar(item.ItemArray[4]);
                switch (itemDeztreza)
                {
                    case 'L':
                        destreza = "Listening";
                        break;
                    case 'R':
                        destreza = "Reading";
                        break;
                    case 'G':
                        destreza = "Gramar/Vocabulary";
                        break;
                    default:
                        break;
                }
                TipoPreguntaEntidad objTipo = objTipoPreg.consultarTipoPreguntaId(Convert.ToInt32(item.ItemArray[5]));
                if (Convert.ToInt32(item.ItemArray[6]) == 0)
                {
                    listaPregunta.Add(
                        new PreguntaEntidad
                        {
                            idPregunta = Convert.ToInt32(item.ItemArray[0]),
                            indicaciones = item.ItemArray[1].ToString(),
                            ponderacion = Convert.ToInt32(item.ItemArray[2]),
                            idActividad = Convert.ToInt32(item.ItemArray[3]),
                            destreza = destreza,
                            objTipo = objTipo,
                            duracion = Convert.ToInt32(item.ItemArray[7]),
                            listaOpciones = consultarOpciones(Convert.ToInt32(item.ItemArray[0]), objTipo.tipo),
                            listaRespuesta = consultarRespuestas(Convert.ToInt32(item.ItemArray[0])),
                            mostarOpciones = Convert.ToBoolean(item.ItemArray[8])
                        });

                }
                else
                {
                    listaPregunta.Add(
                         new PreguntaEntidad
                         {
                             idPregunta = Convert.ToInt32(item.ItemArray[0]),
                             indicaciones = Convert.ToString(item.ItemArray[1]),
                             ponderacion = Convert.ToInt32(item.ItemArray[2]),
                             idActividad = Convert.ToInt32(item.ItemArray[3]),
                             destreza = item.ItemArray[4].ToString(),
                             objTipo = objTipo,
                             objApoyo = objApoyo.consultarApoyoId(Convert.ToInt32(item.ItemArray[6])),
                             duracion = Convert.ToInt32(item.ItemArray[7]),
                             listaOpciones = consultarOpciones(Convert.ToInt32(item.ItemArray[0]), objTipo.tipo),
                             listaRespuesta = consultarRespuestas(Convert.ToInt32(item.ItemArray[0])),
                             mostarOpciones = Convert.ToBoolean(item.ItemArray[8])
                         });
                }
            }
            return listaPregunta;
        }

        public int[,] preguntaCud(VariosModel variosModels)
        {
            PreguntaEntidad objPregunta = variosModels.modelPregunta;

            DataTable id = objIngreso.preguntaCud(int.Parse("3"), int.Parse("0"), objPregunta.indicaciones, objPregunta.ponderacion, objPregunta.idActividad,
                objPregunta.destreza, objPregunta.idTipo, objPregunta.idApoyo, objPregunta.duracion, objPregunta.mostarOpciones);
            DataRow fila = id.Rows[0];
            int idPregunta = Convert.ToInt32(fila.ItemArray[0]);
            List<int> _listIdOp = new List<int>();
            if ((objPregunta.idTipo >= 1 && objPregunta.idTipo <= 3) || objPregunta.idTipo == 7)
            {
                ing1to3And7(variosModels.modelListaOpciones, variosModels.modelListaRespuesta, idPregunta);
            }
            else if (objPregunta.idTipo == 4)
            {
                _listIdOp = ing4(variosModels.files, variosModels.modelListaRespuesta, idPregunta);
            }
            else if (objPregunta.idTipo == 5)
            {
                ing5(variosModels.modelListaOpciones, variosModels.modelListaRespuesta, idPregunta);
            }
            else if (objPregunta.idTipo == 6)
            {
                ing6(variosModels.modelListaOpciones, variosModels.modelListaRespuesta, idPregunta);
            }
            int[,] arrayPO = new int[1,1];
            if (_listIdOp.Count>0)
            {
                arrayPO = new int[2, _listIdOp.Count];
                
            }
            arrayPO[0, 0] = idPregunta;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _listIdOp.Count; j++)
                {
                    if (i == 0)
                    {
                        arrayPO[i, j] = idPregunta;
                        continue;
                    }
                    arrayPO[i, j] = _listIdOp[j];
                }
            }
            return arrayPO;
        }

        private void ing1to3And7(List<string> opciones, List<string> respuestas, int idPregu)
        {
            for (int i = 0; i < opciones.Count; i++)
            {
                int idOpciones = opcionesCud(int.Parse("3"), 0, opciones[i], idPregu);
                respuestaCud(int.Parse("3"), int.Parse("0"), idOpciones, respuestas[i]);
            }
        }

        private List<int> ing4(List<HttpPostedFileBase> files, List<string> respuesta, int idPregu)
        {
            List<int> _listIdOp = new List<int>();
            for (int i = 0; i < respuesta.Count; i++)
            {
                string imgName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "-imgActividad" + System.IO.Path.GetFileName(files[i].FileName);
                string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("..//Img//imgActividad"), (DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "-imgActividad" + System.IO.Path.GetFileName(files[i].FileName)));
                files[i].SaveAs(path);

                int idOpciones = opcionesCud(int.Parse("3"), int.Parse("0"), imgName, idPregu);
                respuestaCud(int.Parse("3"), int.Parse("0"), idOpciones, respuesta[i]);
                _listIdOp.Add(idOpciones);
            }
            return _listIdOp;
        }

        private void ing5(List<string> opc, List<string> resp, int idPregun)
        {
            for (int i = 0; i < opc.Count; i++)
            {
                int idOpciones = opcionesCud(int.Parse("3"), int.Parse("0"), opc[i], idPregun);
                respuestaCud(int.Parse("3"), int.Parse("0"), idOpciones, resp[i]);
                int filas = resp.Count / opc.Count;
                int a = i;
                for (int j = 0; j < filas; j++)
                {
                    a = a + opc.Count;
                    if (a < resp.Count)
                    {
                        respuestaCud(int.Parse("3"), int.Parse("0"), idOpciones, resp[a]);
                    }
                }
            }
        }

        private void ing6(List<string> opc, List<string> resp, int idPregun)
        {
            for (int i = 0; i < opc.Count; i++)
            {
                int control = 0;
                for (int j = 0; j < resp.Count; j++)
                {
                    control++;
                    if (opc[i] == resp[j])
                    {
                        int idOpciones = opcionesCud(int.Parse("3"), int.Parse("0"), opc[i], idPregun);
                        respuestaCud(int.Parse("3"), int.Parse("0"), idOpciones, resp[j]);
                        break;
                    }
                    if (opc[i] != resp[j])
                    {
                        if (control == resp.Count)
                        {
                            opcionesCud(int.Parse("3"), int.Parse("0"), opc[i], idPregun);
                            break;
                        }
                    }
                }
            }

        }

        public int opcionesCud(int _opcion, int _idopciones, string _detallepre, int _idpregun)
        {
            DataTable id = objIngreso.opcionesCud(_opcion, _idopciones, _detallepre, _idpregun);
            DataRow fila = id.Rows[0];
            return Convert.ToInt32(fila.ItemArray[0]);
        }

        public void respuestaCud(int _opcion, int? _idrespuesta, int _idopciones, string _detallerespu)
        {
            objIngreso.respuestaCud(_opcion, _idrespuesta, _idopciones, _detallerespu);
        }

        /// <summary>
        /// <para>Elimnar una pregunta, sus opciones y respuestas</para>
        /// </summary>
        /// <param name="_idPregunta"></param>
        public void preguntaEliminar(int _idPregunta)
        {
            objIngreso.preguntaEliminarTodo(_idPregunta);
        }


        /// <summary>
        /// <para>editar una la imagen de una opcion</para>
        /// </summary>
        /// <param name="variosModels"></param>
        public List<int> editarPreguntaCudIMG(VariosModel variosModels)
        {
            PreguntaEntidad objPregunta = variosModels.modelPregunta;
            List<int> _listOP = new List<int>();
            DataTable id = objIngreso.preguntaCud(int.Parse("2"), objPregunta.idPregunta, objPregunta.indicaciones, objPregunta.ponderacion, objPregunta.idActividad,
                objPregunta.destreza, objPregunta.idTipo, objPregunta.idApoyo, objPregunta.duracion, objPregunta.mostarOpciones);
            if (variosModels.modelListaRespuesta == null || variosModels.modelListaRespuesta.Count == 0)
            {
                return _listOP;
            }
            for (int i = 0; i < variosModels.modelListaRespuesta.Count; i++)
            {
                string imgName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "-imgActividad" + System.IO.Path.GetFileName(variosModels.files[i].FileName);
                string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("..//Img//imgActividad"), (DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "-imgActividad" + System.IO.Path.GetFileName(variosModels.files[i].FileName)));
                variosModels.files[i].SaveAs(path);
                if (variosModels.idOpcionImg[i] == 0)
                {
                    int idOpciones = opcionesCud(int.Parse("3"), int.Parse("0"), imgName, objPregunta.idPregunta);
                    respuestaCud(int.Parse("3"), int.Parse("0"), idOpciones, variosModels.modelListaRespuesta[i]);
                    _listOP.Add(idOpciones);
                    continue;
                }
                objIngreso.opcionesImgEditar(1, variosModels.idOpcionImg[i], imgName, variosModels.modelListaRespuesta[i]);
                _listOP.Add(variosModels.idOpcionImg[i]);
            }
            return _listOP;
        }

        public void eliminarOpcionImg(int _idOpcion)
        {
            objIngreso.opcionesImgEditar(2, _idOpcion, "", "");
        }
    }
}