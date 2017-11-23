using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicEfectividad
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta la efectividad en las respuestas de un ciclo y nivel especificado</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public List<EfectividadEntidad> EfectividadXcicloYnivel(int _numCiclo, string _equivlenteNivel, string _codigoModulo,string _equivalenteTipo)
        {
            List<EfectividadEntidad> _objListaEfectividadEntidad = new List<EfectividadEntidad>();
            string destreza = "Grammar/Vocabulary";
            foreach (DataRow item in _objConsultas.EfectividadXcicloYnivel(_numCiclo,_equivlenteNivel,_codigoModulo,_equivalenteTipo).Rows)
            {
                if (item.ItemArray[0].ToString() == "L")
                    destreza= "Listening";
                if (item.ItemArray[0].ToString() == "R")
                    destreza= "Reading";

                _objListaEfectividadEntidad.Add(new EfectividadEntidad { tipoEfectividad = destreza , porcentaje = Math.Round(Convert.ToDecimal(item.ItemArray[1]),2)});
            }
            return _objListaEfectividadEntidad;
        }

        /// <summary>
        /// <para>Consulta la efectividad en las respuestas de un ciclo, nivel y paralelo especificado</para>
        /// </summary>
        /// <param name="_idModulo">identificador del modulo</param>
        /// <param name="_idCiclo">identificador del ciclo</param>
        /// <param name="_idNivel">identificador del nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public List<EfectividadEntidad> EfectividadXcicloNivelYparalelo(int? _idModulo, int? _idCiclo, int? _idNivel, string _codigoModulo, string _equivalenteTipo, string _paralelo)
        {
            List<EfectividadEntidad> _objListaEfectividadEntidad = new List<EfectividadEntidad>();
            string destreza = "Grammar/Vocabulary";
            foreach (DataRow item in _objConsultas.EfectividadXcicloNivelYparalelo(_idModulo,_idCiclo, _idNivel, _codigoModulo, _equivalenteTipo, _paralelo).Rows)
            {

                if (item.ItemArray[0].ToString() == "L")
                    destreza = "Listening";
                if (item.ItemArray[0].ToString() == "R")
                    destreza = "Reading";
                _objListaEfectividadEntidad.Add(new EfectividadEntidad { tipoEfectividad = destreza, porcentaje = Math.Round(Convert.ToDecimal(item.ItemArray[1]),2)});
            }
            return _objListaEfectividadEntidad;
        }


        /// <summary>
        /// <para>Consulta la efectividad en las respuestas de un ciclo, nivel y carrera especificado</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public List<EfectividadEntidad> EfectividadXcicloNivelYcarrera(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo, int _idCarrera)
        {
            List<EfectividadEntidad> _objListaEfectividadEntidad = new List<EfectividadEntidad>();
            string destreza = "Grammar/Vocabulary";
            foreach (DataRow item in _objConsultas.EfectividadXcicloNivelYcarrera(_numCiclo, _equivlenteNivel, _codigoModulo, _equivalenteTipo, _idCarrera).Rows)
            {
                if (item.ItemArray[0].ToString() == "L")
                    destreza = "Listening";
                if (item.ItemArray[0].ToString() == "R")
                    destreza = "Reading";
                _objListaEfectividadEntidad.Add(new EfectividadEntidad { tipoEfectividad = destreza, porcentaje = Convert.ToDecimal(item.ItemArray[1])});
            }
            return _objListaEfectividadEntidad;
        }

        /// <summary>
        /// <para>Consulta la efectividad de los estudiantes agrupadas por los paralelos de un nivel especificado</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>

        public List<EfectividadEntidad> EfectividadComparativaXparalelos(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            List<EfectividadEntidad> _objListaEfectividadEntidad = new List<EfectividadEntidad>();
            decimal L = 0;
            decimal R = 0;
            decimal G = 0;

            foreach (DataRow item in _objConsultas.EfectividadComparativaXparalelos(_numCiclo, _equivlenteNivel, _codigoModulo, _equivalenteTipo).Rows)
            {
                if (!(item.ItemArray[1] is DBNull))
                {
                    L = Math.Round(Convert.ToDecimal(item.ItemArray[1]), 2);
                }
                if (!(item.ItemArray[2] is DBNull))
                {
                    R = Math.Round(Convert.ToDecimal(item.ItemArray[2]), 2);
                } if (!(item.ItemArray[3] is DBNull))
                {
                    G = Math.Round(Convert.ToDecimal(item.ItemArray[3]), 2);
                }
                _objListaEfectividadEntidad.Add(new EfectividadEntidad
                {
                    modulo = ConsultarCodigoModulo(Convert.ToInt32(item.ItemArray[0])),
                    Listening = L,
                    Reading = R,
                    GrammarVocabulary = G
                });
            }
            return _objListaEfectividadEntidad;
        }

        /// <summary>
        /// <para>Consulta el codigo del modulo especificado</para>
        /// </summary>
        /// <param name="_idModulo">Identificador del modulo especificado</param>
        /// <returns></returns>
        public string ConsultarCodigoModulo(int _idModulo)
        {
            string codigo = "";
            foreach (DataRow item in _objConsultas.ConsultarCodigoModulo(_idModulo).Rows)
            {
                codigo = item.ItemArray[0].ToString();
            }
            return codigo;
        }



        /// <summary>
        /// <para>Consuta la efectividad de un estudiante en una actividad específica</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idModulo"></param>
        /// <returns></returns>
        public List<EfectividadEntidad> EfectividadXactividad(int _idEstudiante, int _idActividadModulo)
        {
            List<EfectividadEntidad> _objListaEfectividadEntidad = new List<EfectividadEntidad>();
            foreach (DataRow item in _objConsultas.efectividadXactividadEstudiante(_idEstudiante, _idActividadModulo).Rows)
            {
                _objListaEfectividadEntidad.Add(new EfectividadEntidad { tipoEfectividad = item.ItemArray[0].ToString(), porcentaje = Convert.ToDecimal(item.ItemArray[1].ToString()) });
            }
            return _objListaEfectividadEntidad;
        }
        
         
    }
}