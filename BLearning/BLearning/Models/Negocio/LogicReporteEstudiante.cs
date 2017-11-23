using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicReporteEstudiante
    {
        MetodosConsultar objMetodosConsultar = new MetodosConsultar();

        public List<RendimientoLeccionEntidad> CalificacionesXleccion(int idModulo, int idEstudiante)
        {
            List<RendimientoLeccionEntidad> listaCalificaciones = new List<RendimientoLeccionEntidad>();
            foreach (DataRow item in objMetodosConsultar.CalificacionesXleccion(idEstudiante, idModulo).Rows)
            {
                listaCalificaciones.Add(new RendimientoLeccionEntidad { numLeccion = item.ItemArray[0].ToString(), valor = Math.Round(Convert.ToDecimal(item.ItemArray[1].ToString()),2)});
            }
            return listaCalificaciones;
        }

        public List<EfectividadEntidad> efectividadDeRespuestasXtipo (int idModulo, int idEstudiante)
        {
            List<EfectividadEntidad> lista = new List<EfectividadEntidad>();
            string destreza = "";
            foreach (DataRow item in objMetodosConsultar.efectividadDeRespuestasEvaluativasXestudiante(idEstudiante,idModulo).Rows)
            {
                if (!(item.ItemArray[1] is DBNull))
                {
                    if (item.ItemArray[0].ToString() == "L")
                        destreza = "Listening";
                    else if (item.ItemArray[0].ToString() == "R")
                        destreza = "Reading";
                    else
                        destreza = "Grammar/Vocabulary";

                    lista.Add(new EfectividadEntidad { tipoEfectividad = destreza, porcentaje = Math.Round(Convert.ToDecimal(item.ItemArray[1].ToString()), 2)});
                }
            }
            return lista;
        }

        /// <summary>
        /// <para>Consulta el porcentaje de lecciones completadas</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idModulo"></param>
        /// <param name="_tipo">Ingresar P (Práctica) o E (Evaluativa)</param>
        /// <returns></returns>
        public List<RendimientoLeccionEntidad> LeccionesCompletadas(int _idModulo, int _idEstudiante, string _tipo)
        {
            List<RendimientoLeccionEntidad> lista = new List<RendimientoLeccionEntidad>();
            decimal porcentaje = 0;
            foreach (DataRow item in objMetodosConsultar.porcentajeLeccionesCompletadasXestudiante(_idEstudiante, _idModulo, _tipo).Rows)
            {
                if (item.ItemArray[1] is DBNull)
                    porcentaje = 0;
                else
                    porcentaje = Convert.ToDecimal(item.ItemArray[1].ToString());
                lista.Add(new RendimientoLeccionEntidad { numLeccion = item.ItemArray[0].ToString(), valor = porcentaje});
            }
            return lista;
        }

        /// <summary>
        /// <para>Muestra una lista de las calificaciones de los estudiantes en las actividades de una leccion</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idLeccion"></param>
        /// <param name="_idModulo"></param>
        /// <returns></returns>
        public List<RendimientoLeccionEntidad> CalificacionesDeActividadesXleccion(int _idEstudiante, int _idLeccion, int _idModulo)
        {
            List<RendimientoLeccionEntidad> lista = new List<RendimientoLeccionEntidad>();
            decimal porcentaje = 0;
            foreach (DataRow item in objMetodosConsultar.CalificacionesDeActividadesXleccion(_idEstudiante, _idLeccion, _idModulo).Rows)
            {
                if (item.ItemArray[1] is DBNull)
                    porcentaje = 0;
                else
                    porcentaje = Convert.ToDecimal(item.ItemArray[1].ToString());
                lista.Add(new RendimientoLeccionEntidad { numLeccion = item.ItemArray[0].ToString(), valor = porcentaje });
            }
            return lista;
        }

        public List<LeccionesXcompletarEntidad> CantidadDeLeccioneXcompletar (int idModulo, int idEstudiante)
        {
            List<LeccionesXcompletarEntidad> lista = new List<LeccionesXcompletarEntidad>();
            var numAct = 0;
            foreach (DataRow item in objMetodosConsultar.numLeccionesXcompletar(idEstudiante,idModulo).Rows)
            {
                if (item.ItemArray[1] is DBNull)
                    numAct = 0;
                else
                    numAct = Convert.ToInt32(item.ItemArray[1]);
                lista.Add(new LeccionesXcompletarEntidad { idLeccion = Convert.ToInt32(item.ItemArray[0].ToString()), numActividades = numAct});
            }
            return lista;

        }

    }
}