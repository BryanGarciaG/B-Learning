using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicTipoModulo
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta los tipos de módulo existentes dentro de un ciclo</para>
        /// </summary>
        /// <param name="_ciclo">Numero del ciclo del que se consultan los tipos de módulo</param>
        /// <returns>Lista de los tipos de módulo</returns>
        public List<TipoModuloEntidad> consultarTiposModulo(int _ciclo)
        {
            List<TipoModuloEntidad> _objLstaTipoModulo = new List<TipoModuloEntidad>();
            foreach (DataRow item in _objConsultas.consultarTipoModulo(_ciclo).Rows)
            {
                _objLstaTipoModulo.Add(new TipoModuloEntidad { idTipoModulo = Convert.ToInt32(item.ItemArray[0]), equivalenteTipo = item.ItemArray[1].ToString() });
            }
            return _objLstaTipoModulo;
        }
    }
}