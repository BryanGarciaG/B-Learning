using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicRespuesta
    {
        MetodosConsultar objConsultas = new MetodosConsultar();

        public List<string> verificarRespuesta(List<DatosDeVerificacionRespuestaEntidad> preguntaAverificar, string tipoPregunta)
        {
            List<string> verificador = new List<string>();
            List<RespuestaEntidad> respuestasPregunta = new List<RespuestaEntidad>();
            int i = -1;
            foreach (DataRow item1 in objConsultas.verificadorRespuestaPertenencia(preguntaAverificar[0].idPregunta).Rows)
            {
                respuestasPregunta.Add(new RespuestaEntidad
                {
                    idRespuesta = Convert.ToInt32(item1.ItemArray[0].ToString()),
                    idOpciones = Convert.ToInt32(item1.ItemArray[1].ToString()),
                    detalleRespuesta = item1.ItemArray[2].ToString()
                });

            }
            if (tipoPregunta == "Pertenencia" || tipoPregunta == "Seleccion Multiple" || tipoPregunta == "Emparejamiento")
            {
                foreach (var item in preguntaAverificar)
                {
                    bool termminar = true;
                    foreach (var item1 in respuestasPregunta)
                    {
                        i++;
                        if (tipoPregunta == "Pertenencia")
                        {
                            if (item.idOpcionPregunta == item1.idOpciones && String.Compare(item.respuestaIngresada, item1.detalleRespuesta) == 0)
                            {
                                termminar = false;
                                respuestasPregunta.RemoveAt(i);
                                verificador.Add("C");
                                break;
                            }
                        }
                        else if (tipoPregunta == "Seleccion Multiple")
                        {
                            if (item.idOpcionPregunta == item1.idOpciones && item.respuestaIngresada == "true")
                            {
                                termminar = false;
                                verificador.Add("C");
                                break;
                            }
                            else if (item.idOpcionPregunta == item1.idOpciones && item.respuestaIngresada == "false")
                            {
                                termminar = false;
                                verificador.Add("I");
                                break;
                            }
                        }
                        else
                        {
                            if (item.idOpcionPregunta == item1.idOpciones)
                            {
                                termminar = false;
                                respuestasPregunta.RemoveAt(i);
                                verificador.Add("C");
                                break;
                            }
                            else
                            {
                                termminar = false;
                                respuestasPregunta.RemoveAt(i);
                                verificador.Add("I");
                                break;
                            }
                        }

                    }
                    i = -1;
                    if (termminar && preguntaAverificar[0].tipoPregunta == "Pertenencia")
                    {
                        verificador.Add("I");
                    }
                    if (termminar && preguntaAverificar[0].tipoPregunta != "Pertenencia")
                    {
                        if (item.respuestaIngresada == "false")
                        {
                            verificador.Add("C");
                        }
                        else
                        {
                            verificador.Add("I");
                        }
                    }
                }
            }
            else
            {
                foreach (var item in preguntaAverificar)
                {
                    i++;
                    foreach (var item1 in respuestasPregunta)
                    {
                        if (item.idOpcionPregunta == item1.idOpciones && String.Compare(item.respuestaIngresada, item1.detalleRespuesta) == 0)
                        {
                            respuestasPregunta.RemoveAt(i);
                            verificador.Add("C");
                            break;
                        }
                        else
                        {
                            respuestasPregunta.RemoveAt(i);
                            verificador.Add("I");
                            break;
                        }
                    }
                    i--;
                }
            }
            return verificador;
        }
        
    }
}