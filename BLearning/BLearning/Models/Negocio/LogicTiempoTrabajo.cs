using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicTiempoTrabajo
    {
        MetodosConsultar _objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>Consulta el tiempo de trabajo autónomo realizado por los estudiantes de un nivel</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>

        public List<TiempoTrabajoEntidad> TiempoTrabajoXcicloYnivel(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            List<TiempoTrabajoEntidad> _objListaTiempoTrabajoEntidad = new List<TiempoTrabajoEntidad>();
            foreach (DataRow item in _objConsultas.TiempoTrabajoXcicloYnivel(_numCiclo, _equivlenteNivel, _codigoModulo, _equivalenteTipo).Rows)
            {
                decimal tiempoTrabajado = Convert.ToDecimal(item.ItemArray[1].ToString());
                _objListaTiempoTrabajoEntidad.Add(new TiempoTrabajoEntidad { label = item.ItemArray[0].ToString(), tiempo = tiempoTrabajado, tiempoString = convertirMinutosAhorasMinutos(tiempoTrabajado) });
            }
            return _objListaTiempoTrabajoEntidad;
        }

        /// <summary>
        /// <para>Consulta el tiempo de trabajo autónomo realizado por los estudiantes de un paralelo</para>
        /// </summary>
        /// <param name="_idModulo">identificador del modulo</param>
        /// <param name="_idCiclo">identificador del ciclo</param>
        /// <param name="_idNivel">identificador del nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <param name="_paralelo">paralelo</param>
        /// <returns></returns>
        public List<TiempoTrabajoEntidad> TiempoTrabajoXparalelo(int? _idModulo, int? _idCiclo, int? _idNivel, string _codigoModulo, string _equivalenteTipo, string _paralelo)
        {
            List<TiempoTrabajoEntidad> _objListaTiempoTrabajoEntidad = new List<TiempoTrabajoEntidad>();

            decimal tiempoTrabajado = 0;
            foreach (DataRow item in _objConsultas.TiempoTrabajoXparalelo(_idModulo, _idCiclo, _idNivel, _codigoModulo, _equivalenteTipo, _paralelo).Rows)
            {
                tiempoTrabajado = Convert.ToDecimal(item.ItemArray[1].ToString());

                _objListaTiempoTrabajoEntidad.Add(new TiempoTrabajoEntidad { label = item.ItemArray[0].ToString(), tiempo = tiempoTrabajado, tiempoString = convertirMinutosAhorasMinutos(tiempoTrabajado) });
            }
            return _objListaTiempoTrabajoEntidad;
        }

        /// <summary>
        /// <para>Convierte los minutos a hh:mm</para>
        /// </summary>
        /// <param name="tiempoMinutos"></param>
        /// <returns></returns>
        public string convertirMinutosAhorasMinutos(decimal tiempoMinutos)
        {
            decimal horas = 0;
            decimal minutos = 0;
            string tiempoStr = "";
            horas = Math.Truncate(tiempoMinutos / 60);
            minutos = Math.Round(tiempoMinutos % 60);
            if (horas >= 10 && minutos >= 10)
                tiempoStr = horas + ":" + minutos + "(hh:mm)";
            else if (horas < 10 && minutos >= 10)
                tiempoStr = "0" + horas + ":" + minutos + "(hh:mm)";
            else if (horas >= 10 && minutos < 10)
                tiempoStr = horas + ":0" + minutos + "(hh:mm)";
            else
                tiempoStr = "0" + horas + ":0" + minutos + "(hh:mm)";
            return tiempoStr;
        }


        public decimal calcularTiempoPromedio(List<TiempoTrabajoEntidad> _objTiempoTrabajoEntidad, decimal tiempoTrabajado)
        {
            if (_objTiempoTrabajoEntidad.Count() != 0)
            {
                foreach (var item in _objTiempoTrabajoEntidad)
                {
                    tiempoTrabajado = tiempoTrabajado + item.tiempo;
                }
                tiempoTrabajado = Math.Round(tiempoTrabajado / _objTiempoTrabajoEntidad.Count(), 2);
            }
            return tiempoTrabajado;
        }

        /// <summary>
        /// <para>Consulta el tiempo de trabajo autónomo por carrera</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public List<TiempoTrabajoEntidad> TiempoTrabajoXcarrera(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            List<TiempoTrabajoEntidad> _objListaTiempoTrabajoEntidad = new List<TiempoTrabajoEntidad>();
            foreach (DataRow item in _objConsultas.TiempoTrabajoXcarrera(_numCiclo, _equivlenteNivel, _codigoModulo, _equivalenteTipo).Rows)
            {

                decimal tiempoTrabajado = Convert.ToDecimal(item.ItemArray[1].ToString());
                _objListaTiempoTrabajoEntidad.Add(new TiempoTrabajoEntidad
                {
                    label = item.ItemArray[0].ToString(),
                    tiempo = tiempoTrabajado,
                    tiempoString = convertirMinutosAhorasMinutos(tiempoTrabajado)
                });
            }
            return _objListaTiempoTrabajoEntidad;
        }
    }
}