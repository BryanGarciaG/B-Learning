using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicActividadModulo
    {
        MetodosIngreso objMI = new MetodosIngreso();

        /// <summary>
        /// <para>Ingresar o editar la asignacion de una actividad</para>
        /// </summary>
        /// <param name="_opcion">2 para editar. 3 para ingresar</param>
        /// <param name="_idActModulo"></param>
        /// <param name="_idModulo"></param>
        /// <param name="_idActividad"></param>
        /// <param name="_fechaIni"></param>
        /// <param name="_fechaFin"></param>
        /// <param name="_idLeccion"></param>
        /// <param name="_tipo"></param>
        /// <returns></returns>
        public int activModuloCud(int _opcion, int _idActModulo,int _idModulo, int _idActividad,
            DateTime _fechaIni,DateTime _fechaFin, int _idLeccion, string _tipo)
        {
            DataTable id = objMI.actividadModuloCud(_opcion, _idActModulo, _idModulo, _idActividad, _fechaIni, _fechaFin, _idLeccion,_tipo);
            DataRow fila = id.Rows[0];
            if (fila.ItemArray[0] is DBNull)
            {
                return 0;
            }
            return Convert.ToInt32(id.Rows[0].ItemArray[0]);
        }

        /// <summary>
        /// <para>Para saber si la actividad asignada ya ha sido resuelta y poderla eliminar</para>
        /// </summary>
        /// <param name="_idActiModu">ID de la asignacion a eliminar</param>
        /// <returns></returns>
        public int asignacionResuelta(int _idActiModu)
        {
            MetodosConsultar objConsultar = new MetodosConsultar();
            DataTable tablas = objConsultar.asignacionResuelta(_idActiModu);
            int estado = Convert.ToInt32(tablas.Rows[0].ItemArray[0]);
            if (estado == 0)
            {
                objConsultar.asignacionesEliminar(_idActiModu);
            }
            return estado;
        }
    }
}