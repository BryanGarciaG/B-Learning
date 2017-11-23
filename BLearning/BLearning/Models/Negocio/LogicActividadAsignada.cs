using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicActividadAsignada
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta el numero de actividades asignadas a un modulo agrupadas por leccion</para>
        /// </summary>
        /// <param name="_idModulo">Identificador del modulo</param>
        /// <param name="_tipo">Tipo de actividad (P o E)</param>
        /// <returns></returns>

        public List<RendimientoLeccionEntidad> ConsultarActividadesAsignadasXleccion(int _idModulo, string _tipo)
        {
            List<RendimientoLeccionEntidad> _objListaRendimientoLeccionEntidad = new List<RendimientoLeccionEntidad>();
            foreach (DataRow item in _objConsultas.ConsultarActividadesAsignadasXleccion(_idModulo, _tipo).Rows)
            {
                if (item.ItemArray[1] is DBNull)
	            {
                    _objListaRendimientoLeccionEntidad.Add(new RendimientoLeccionEntidad
                    {
                        numLeccion = item.ItemArray[0].ToString(),
                        valor = 0
                    });
                }
                else
                {
                    _objListaRendimientoLeccionEntidad.Add(new RendimientoLeccionEntidad
                    {
                        numLeccion = item.ItemArray[0].ToString(),
                        valor = Convert.ToInt32(item.ItemArray[1])
                    });
                }
                
            }
            return _objListaRendimientoLeccionEntidad;
        }

        /// <summary>
        /// <para>Consulta el numero de actividades asignadas a un modulo agrupadas por leccion segun el tipo de actividad</para>
        /// </summary>
        /// <param name="_idModulo">Identificador del modulo</param>
        /// <returns></returns>
        public List<ActividadAsignadaEntidad> ConsultarReporteActividadesAsignadasXtipoActividad( int _idModulo)
        {
            List<ActividadAsignadaEntidad> _objListaActividadesAsignadas = new List<ActividadAsignadaEntidad>();
            _objListaActividadesAsignadas.Add(new ActividadAsignadaEntidad{ tipoActividad = "PRACTICAL ACTIVITIES", _objListaLeccionesAsignadas = ConsultarActividadesAsignadasXleccion(_idModulo, "P")});
            _objListaActividadesAsignadas.Add(new ActividadAsignadaEntidad { tipoActividad = "EVALUATIVE ACTIVITIES", _objListaLeccionesAsignadas = ConsultarActividadesAsignadasXleccion(_idModulo, "E") });
            return _objListaActividadesAsignadas;
        }

    }
}