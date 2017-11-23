using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicCarrera
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consultas las carreras registradas en el sistema</para>
        /// </summary>
        /// <returns>Lista de tipo CarreraEntidad de las carreras</returns>
        public List<CarreraEntidad> consultarCarreras()
        {
            List<CarreraEntidad> _objListaCarreraEntidad = new List<CarreraEntidad>();
            foreach (DataRow item in _objConsultas.consultarCarreras().Rows)
            {
                _objListaCarreraEntidad.Add(new CarreraEntidad { idCarrera = Convert.ToInt32(item.ItemArray[0]), carrera = item.ItemArray[1].ToString() });
            }
            return _objListaCarreraEntidad;
        }

    }
}