using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Negocio
{
    public class LogicParalelo
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta los paralelos de un nivel específico</para>
        /// </summary>
        /// <param name="_codigo">código del módulo</param>
        /// <param name="_ciclo">número del ciclo</param>
        /// <param name="_tipoModulo">identificador dell tipo de módulo</param>
        /// <param name="_nivel">Equivalente del nivel</param>
        /// <returns>Lista de string con los paralelos</returns>
        public List<string> consultarParalelos( string _codigo, int  _ciclo, int _tipoModulo, string _nivel)
        {
            List<string> _listaParalelos = new List<string>();
            foreach (DataRow item in _objConsultas.consultarParalelos(_codigo, _ciclo, _tipoModulo, _nivel).Rows)
            {
                _listaParalelos.Add(item.ItemArray[0].ToString());
            }
            return _listaParalelos;
        }
        /// <summary>
        /// <para>Consulta el docente de un paralelo específico</para>
        /// </summary>
        /// <param name="_idCiclo">identificador del ciclo</param>
        /// <param name="_idNivel">identificador del nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <param name="_paralelo">paralelo</param>
        /// <returns></returns>
        public string ConsultaDocenteXmodulo(int _idCiclo, int _idNivel, string _codigoModulo, string _equivalenteTipo, string _paralelo)
        {
            string docente = "";
            foreach (DataRow item in _objConsultas.ConsultaDocenteXmodulo(_idCiclo, _idNivel, _codigoModulo, _equivalenteTipo, _paralelo).Rows)
            {
                docente = item.ItemArray[0].ToString();
            }
            return docente;
        }
    }
}