using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;


namespace BLearning.Models.Negocio
{
    public class LogicTipoPregunta
    {
        MetodosConsultar objConsultasDB = new MetodosConsultar();
        public TipoPreguntaEntidad consultarTipoPreguntaId(int _idTipoPregunta)
        {
            TipoPreguntaEntidad objTipoPregunta = new TipoPreguntaEntidad();
            foreach (DataRow item in objConsultasDB.consultarTipoPreguntaId(_idTipoPregunta).Rows)
            {
                objTipoPregunta.idTipo = Convert.ToInt32(item.ItemArray[0]);
                objTipoPregunta.tipo = item.ItemArray[1].ToString();

            }
            return objTipoPregunta;
        }

        public List<TipoPreguntaEntidad> tiposPreguntaConsultar()
        {
            List<TipoPreguntaEntidad> listTP = new List<TipoPreguntaEntidad>();
            foreach (DataRow item in objConsultasDB.tiposPreguntaConsultar().Rows)
            {
                listTP.Add(new TipoPreguntaEntidad
                    {
                        idTipo = Convert.ToInt32(item.ItemArray[0]),
                        tipo = item.ItemArray[1].ToString()
                    });
            }
            return listTP;
        }

    }
}