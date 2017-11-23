using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicCiclo
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta los 10 últimos ciclos</para>
        /// </summary>
        /// <returns>Lista de con 10 registros de los ciclos</returns>
        public List<CiclosEntidad> consultarCiclos()
        {
            List<CiclosEntidad> _objListaCicloEntidad = new List<CiclosEntidad>();
            foreach (DataRow item in _objConsultas.consultarCiclos().Rows)
            {
                _objListaCicloEntidad.Add(new CiclosEntidad { idCiclo = Convert.ToInt32(item.ItemArray[0]), ciclo = Convert.ToInt32(item.ItemArray[1]) });
            }
            return _objListaCicloEntidad;
        }
    }
}