using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicDocenteCursos
    {
        MetodosConsultar objMetodosConsultar = new MetodosConsultar();

        /// <summary>
        /// <para>Consultar el id de un modulo</para>
        /// </summary>
        /// <param name="_idTipoModulo">id del tipo de modulo</param>
        /// <param name="_ciclo">id del ciclo</param>
        /// <param name="_nivel">id del nivel</param>
        /// <param name="_numPara">numero del paralelo</param>
        /// <returns>id de un modulo</returns>
        public int consultarIdModulo(int _idTipoModulo, int _ciclo, int _nivel, string _numPara)
        {
            DataTable tabla = objMetodosConsultar.consultarIdModulo(_idTipoModulo, _ciclo, _nivel, _numPara);
            return Convert.ToInt32(tabla.Rows[0].ItemArray[0]);
        }


        /// <summary>
        /// <para>Consultar los modulos que tiene asignado un docente</para>
        /// </summary>
        /// <param name="_idPersona"></param>
        /// <returns></returns>
        public List<DocenteCursosEntidad> consultarCursosDocente(int _idPersona)
        {
            List<DocenteCursosEntidad> objDocenteCursos = new List<DocenteCursosEntidad>();

            foreach (DataRow item in objMetodosConsultar.consultarCursosDocente(_idPersona).Rows)
            {
                objDocenteCursos.Add(new DocenteCursosEntidad
                {
                    idNivel = Convert.ToInt32(item.ItemArray[0]),
                    numNivel = Convert.ToString(item.ItemArray[1]),
                    idParalelo = Convert.ToInt32(item.ItemArray[2]),
                    numParalelo = Convert.ToString(item.ItemArray[3]),
                    idModulo = Convert.ToInt32(item.ItemArray[4])
                });
            }


            return objDocenteCursos;
        }
    }
}