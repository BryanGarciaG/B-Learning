using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicAcceso
    {
        MetodosIngreso _objIngresos = new MetodosIngreso();
        MetodosConsultar _objConsultas = new MetodosConsultar();
        /// <summary>
        /// <para>Ingresa un resgistro de acceso al sistema</para>
        /// </summary>
        /// <param name="_idAcceso"></param>
        /// <param name="_fechaEntrada"></param>
        /// <param name="_idPersona"></param>
        /// <param name="_tipo"></param>
        /// <param name="_opcion"></param>
        /// <returns></returns>
        public void ingresarAcceso(int _idAcceso, DateTime _fechaEntrada, string _tipo, int _idPersona, int _opcion)
        {
            _objIngresos.ingresarAcceso(_idAcceso, _fechaEntrada, _tipo, _idPersona, _opcion);           
        }

        /// <summary>
        /// <para>Consulta el historial de acceso al sistema de una persona</para>
        /// </summary>
        /// <param name="_nombres">Apellidos y nombres</param>
        /// <returns></returns>
        public List<AccesoEntidad> consultarHistorialDeAcceso(string _nombres)
        {
            List<AccesoEntidad> _objListaAccesos = new List<AccesoEntidad>();
            foreach (DataRow item in _objConsultas.consultarHistorialDeAcceso(_nombres).Rows)
            {
                _objListaAccesos.Add(new AccesoEntidad
                { 
                    idAcceso = Convert.ToInt32(item.ItemArray[0]),
                    nombre= item.ItemArray[4].ToString(),
                    tipoAcceso= item.ItemArray[2].ToString(),
                    idPersona = Convert.ToInt32(item.ItemArray[3]), 
                    entrada = Convert.ToDateTime(item.ItemArray[1]).ToShortDateString(), 
                    horaEntrada= Convert.ToDateTime(item.ItemArray[1]).ToShortTimeString() });
            }
            return _objListaAccesos;
        }

        
        /// <summary>
        /// <para>Consulta los nombres segun como va escribiendo el usuario</para>
        /// </summary>
        /// <param name="_nombres">Apellidos y nombres</param>
        /// <returns></returns>
        public List<string> predecirNombre(string _nombres)
        {
            List<string> _objListaPersonas = new List<string>();
            foreach (DataRow item in _objConsultas.predecirNombre(_nombres).Rows)
            {
                _objListaPersonas.Add(item.ItemArray[0].ToString());
            }
            return _objListaPersonas;
        }
    }
}