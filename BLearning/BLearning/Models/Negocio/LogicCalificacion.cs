using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicCalificacion
    {
        MetodosConsultar objConsultasDB = new MetodosConsultar();
        MetodosIngreso objIngresosDB = new MetodosIngreso();

        public CalificacionEntidad consultaCalificacionIdQuiz(int idQUiz)
        {
            CalificacionEntidad objCalificacion = new CalificacionEntidad();
            DataTable calificacion = objConsultasDB.consultarCalificacionIdQuiz(idQUiz);
            if (calificacion.Rows.Count != 0)
            {
                foreach (DataRow item in calificacion.Rows)
                {
                    objCalificacion.idCalificacion = Convert.ToInt32(item.ItemArray[0].ToString());
                    objCalificacion.idQuiz = Convert.ToInt32(item.ItemArray[1].ToString());
                }
            }

            return objCalificacion;
        }

        public DetalleCalificacionEntidad consultarDetCalifXdestrYcalifica(int idCalificacion, string parametro)
        {
            DetalleCalificacionEntidad objDetalleCalificacion = new DetalleCalificacionEntidad();
            DataTable detalleEfectividad = objConsultasDB.consultarDetalleCalifXcalifYparam(idCalificacion, parametro);
            if (detalleEfectividad.Rows.Count != 0)
            {
                foreach (DataRow item in detalleEfectividad.Rows)
                {
                    objDetalleCalificacion.idDetalleCalificacion = Convert.ToInt32(item.ItemArray[0].ToString());
                    objDetalleCalificacion.calificacion = Convert.ToDecimal(item.ItemArray[1].ToString());
                    objDetalleCalificacion.destreza = item.ItemArray[2].ToString();
                    objDetalleCalificacion.idCalificacion = Convert.ToInt32(item.ItemArray[3].ToString());
                }
            }
            return objDetalleCalificacion;
        }

        public List<DetalleCalificacionEntidad> consultarDetalleEfectividadXefect(int idCalificacion)
        {
            List<DetalleCalificacionEntidad> listaDetalleCalificacion = new List<DetalleCalificacionEntidad>();
            DataTable detalleCalificacion = objConsultasDB.consultarDetalleCalifiXidCalif(idCalificacion);
            if (detalleCalificacion.Rows.Count != 0)
            {
                foreach (DataRow item in detalleCalificacion.Rows)
                {
                    listaDetalleCalificacion.Add(new DetalleCalificacionEntidad { idDetalleCalificacion = Convert.ToInt32(item.ItemArray[0].ToString()), calificacion = Convert.ToDecimal(item.ItemArray[1].ToString()), destreza = item.ItemArray[2].ToString(), idCalificacion = Convert.ToInt32(item.ItemArray[3].ToString()) });
                }
            }
            return listaDetalleCalificacion;
        }



        public void ingresoEfectividad(int idQuiz, decimal calificacion, string parametro)
        {
            CalificacionEntidad objConsultaEfectAingresar = consultaCalificacionIdQuiz(idQuiz);
            if (objConsultaEfectAingresar.idCalificacion != 0)
            {
                DetalleCalificacionEntidad objDetalleEfectConsulta = consultarDetCalifXdestrYcalifica(objConsultaEfectAingresar.idCalificacion, parametro);

                if (objDetalleEfectConsulta.idCalificacion != 0)
                {
                    objIngresosDB.ingresarDetalleCalificacion(objDetalleEfectConsulta.idDetalleCalificacion, calificacion, parametro, objDetalleEfectConsulta.idCalificacion, 2);
                }
                else
                    objIngresosDB.ingresarDetalleCalificacion(0, calificacion, parametro, objConsultaEfectAingresar.idCalificacion, 3);

            }
            else
            {
                DataTable idCalificacion = objIngresosDB.ingresarCalificacion(0, idQuiz, 3);
                objIngresosDB.ingresarDetalleCalificacion(0, calificacion, parametro, int.Parse(idCalificacion.Rows[0].ItemArray[0].ToString()), 3);
            }
        }

        public decimal calculoCalificacion(int nPreguntas, int nPreguntasCorrectas, decimal ponderacion)
        {
            decimal calificacion = 0;
            calificacion = (ponderacion / Convert.ToDecimal(nPreguntas.ToString())) * Convert.ToDecimal(nPreguntasCorrectas.ToString());
            string reempCal = calificacion.ToString();
            calificacion = Math.Round(Convert.ToDecimal(reempCal.Replace(".", ",")), 2);
            return calificacion;
        }


        /// <summary>
        /// <para>Consulta la calificicion de una actividad resuelta</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idActividadModulo"></param>
        /// <returns></returns>
        public decimal consultarCalificacionObtenida(int _idEstudiante, int _idActividadModulo)
        {
            decimal calificacion = 0;
            foreach (DataRow item in objConsultasDB.consultarCalificacionObtenidaActividad(_idEstudiante, _idActividadModulo).Rows)
            {
                calificacion = Convert.ToDecimal(item.ItemArray[0]);
            }
            return calificacion;
        }

        public List<DetalleCalificacionEntidad> consultarCalificActividad(int idAct)
        {
            DataTable listaCalificacion = objConsultasDB.consultarCalificActividad(idAct);
            List<DetalleCalificacionEntidad> listaDetalleCalif = new List<DetalleCalificacionEntidad>();
            foreach (DataRow item in listaCalificacion.Rows)
            {
                listaDetalleCalif.Add(new DetalleCalificacionEntidad { calificacion = Convert.ToDecimal(item.ItemArray[1]), destreza = item.ItemArray[0].ToString() });
            }
            return listaDetalleCalif;
        }
    }   
}