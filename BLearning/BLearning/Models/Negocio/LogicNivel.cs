using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicNivel
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta los niveles dependiendo del tipo de módulo</para>
        /// </summary>
        /// <param name="_ciclo">Numero del ciclo del que se consultan los niveles</param>
        /// <param name="_idTipoModulo">Identificador del tipo de odulo del ciclo/param>
        /// <returns>Lista de niveles del ciclo especificado</returns>
        public List<NivelEntidad> consultarNivelesXtipoModulo (int _ciclo, int _idTipoModulo)
        {
            List<NivelEntidad> _objListaNivelEntidad = new List<NivelEntidad>();
            foreach (DataRow item in _objConsultas.consultarNivelesXtipoModulo(_ciclo, _idTipoModulo).Rows)
            {
                _objListaNivelEntidad.Add(new NivelEntidad { idNivel = Convert.ToInt32(item.ItemArray[0]), numeroNivel = Convert.ToInt32(item.ItemArray[1]), equivalente = item.ItemArray[2].ToString() });
            }
            return _objListaNivelEntidad;
        }
    }
}