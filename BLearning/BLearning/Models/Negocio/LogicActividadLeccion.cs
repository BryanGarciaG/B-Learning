using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicActividadLeccion
    {
        MetodosConsultar objConsultas = new MetodosConsultar();
        public List<ActividadLeccionEntidad> consultarListaActividades(int idModulo, int idLeccion, int idEstudiante)
        {
            List<ActividadLeccionEntidad> listaActividades = new List<ActividadLeccionEntidad>();
            LogicActividad objActEnt = new LogicActividad();
            foreach (DataRow item in objConsultas.consultarListaActividades(idModulo, idLeccion, idEstudiante).Rows)
            {
                listaActividades.Add(new ActividadLeccionEntidad { idActModulo = Convert.ToInt32(item.ItemArray[0]), idModulo = Convert.ToInt32(item.ItemArray[1]), idActividad = objActEnt.consultarActividad(Convert.ToInt32(item.ItemArray[2])), fechaInicio = Convert.ToDateTime(item.ItemArray[3]), fechAFin = Convert.ToDateTime(item.ItemArray[4]), IdLeccion = Convert.ToInt32(item.ItemArray[5]), nVecesResuelta = Convert.ToInt32(item.ItemArray[7]), indicePregunta = Convert.ToInt32(item.ItemArray[8]), tipo = item.ItemArray[6].ToString() });

            }
            return listaActividades;
        }

        
    }
}