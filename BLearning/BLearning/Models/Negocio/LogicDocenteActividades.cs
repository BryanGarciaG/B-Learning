using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BLearning.Models.Datos;


namespace BLearning.Models.Negocio
{
    public class LogicDocenteActividades
    {
        MetodosConsultar objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Retorna las actividades que ha creado una persona en el ultimo ciclo</para>
        /// </summary>
        /// <param name="_idPersona">Identificador de la persona de la que quiere consultar</param>
        /// <returns></returns>
        public List<ActividadesXDocenteEntidad> Consultar_ReporteDeActividadesXDocente(int _idPersona)
        {
            List<ActividadesXDocenteEntidad> listAxD = new List<ActividadesXDocenteEntidad>();
            LogicActividad _objActividadEntidad = new LogicActividad();
            foreach (DataRow item in objConsultas.Consultar_ReporteDeActividadesXDocente(_idPersona).Rows)
            {
                listAxD.Add(new ActividadesXDocenteEntidad
                {
                    idActividad = Convert.ToInt32(item.ItemArray[0]),
                    idNivel = Convert.ToInt32(item.ItemArray[1]),
                    descripcion = Convert.ToString(item.ItemArray[2]),
                    nPreguntas = Convert.ToInt32(item.ItemArray[3]),
                    duracion = Convert.ToInt32(item.ItemArray[4]),
                    fechaCreacion = Convert.ToDateTime(item.ItemArray[5]),
                    Destrezas = _objActividadEntidad.consultarDestrzasXActividad(Convert.ToInt32(item.ItemArray[0])),
                    idDocente = Convert.ToInt32(item.ItemArray[6]),
                });
            }
            return listAxD;
        }

        /// <summary>
        /// <para>Lista de docentes con el numero de actividades creadas en el ultimo ciclo</para>
        /// </summary>
        /// <returns></returns>
        public List<DocenteActividadesEntidad> Consultar_NumDeActividadesCreadasXciclo()
        {
            List<DocenteActividadesEntidad> _objListaNumActividadesXDocentes = new List<DocenteActividadesEntidad>();
            foreach (DataRow item in objConsultas.Consultar_NumDeActividadesCreadasXciclo().Rows)
            {
                _objListaNumActividadesXDocentes.Add(new DocenteActividadesEntidad
                {
                    idPersona = Convert.ToInt32(item.ItemArray[0]),
                    nombres = item.ItemArray[1].ToString(),
                    nActivCreadas = Convert.ToInt32(item.ItemArray[2])
                });
            }
            return _objListaNumActividadesXDocentes;
        }

    }
}