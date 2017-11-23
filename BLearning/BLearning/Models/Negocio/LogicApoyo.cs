using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLearning.Models.Datos;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BLearning.Models.Negocio
{
    public class LogicApoyo
    {
        MetodosConsultar objConsultasDB = new MetodosConsultar();

        public ApoyoEntidad consultarApoyoId(int _idApoyo)
        {
            ApoyoEntidad objApoyo = new ApoyoEntidad();
            foreach (DataRow item in objConsultasDB.consultarApoyoId(_idApoyo).Rows)
            {
                objApoyo.idApoyo = Convert.ToInt32(item.ItemArray[0]);
                objApoyo.titulo = item.ItemArray[1].ToString();
                objApoyo.enunciado = item.ItemArray[2].ToString();
                objApoyo.link = item.ItemArray[3].ToString();
                objApoyo.imagen = item.ItemArray[4].ToString();
                objApoyo.audio = item.ItemArray[5].ToString();
            }
            return objApoyo;
        }

        public int gestorApoyo(ApoyoEntidad _objApoyo)
        {
            int idApoyo = 0;
            if (_objApoyo.enunciado != null || _objApoyo.link != null || _objApoyo.imagenImagen != null || _objApoyo.audioAudio != null || _objApoyo.imagen != null || _objApoyo.audioAudio != null)
            {
                string path = null;
                string imgName = "";
                string pathAudio = null;
                string audioName = "";
                if (_objApoyo.imagenImagen != null)
                {
                    imgName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "" + "-imgApoyo" + "" + System.IO.Path.GetExtension(_objApoyo.imagenImagen.FileName);
                    path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("..//Img//imgApoyo"),
                        (DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "-imgApoyo" + System.IO.Path.GetExtension(_objApoyo.imagenImagen.FileName)));
                    _objApoyo.imagenImagen.SaveAs(path);
                }
                else if (_objApoyo.imagen != null)
                {
                    imgName = _objApoyo.imagen;
                }
                if (_objApoyo.audioAudio != null)
                {
                    audioName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "" + "-audioApoyo" + "" + System.IO.Path.GetExtension(_objApoyo.audioAudio.FileName);
                    pathAudio = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("..//Img//audioApoyo"),
                        (DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "-audioApoyo" + System.IO.Path.GetExtension(_objApoyo.audioAudio.FileName)));
                    _objApoyo.audioAudio.SaveAs(pathAudio);
                }
                else if (_objApoyo.audio != null)
                {
                    audioName = _objApoyo.audio;
                }
                if (_objApoyo.idApoyo != 0)
                {
                    idApoyo = _objApoyo.idApoyo;
                    apoyoCud(int.Parse("2"), _objApoyo.idApoyo, _objApoyo.enunciado, _objApoyo.link, imgName, _objApoyo.titulo, audioName);
                }
                else
                {
                    idApoyo = apoyoCud(int.Parse("3"), int.Parse("0"), _objApoyo.enunciado, _objApoyo.link, imgName, _objApoyo.titulo, audioName);

                }
            }
            if ((_objApoyo.enunciado == null && _objApoyo.link == null && _objApoyo.imagenImagen == null && _objApoyo.audioAudio == null && _objApoyo.imagen == null && _objApoyo.audioAudio == null)&&_objApoyo.idApoyo!=0  )
            {
                return _objApoyo.idApoyo;
            }
            return idApoyo;
        }

        //Bryan
        MetodosIngreso objIngreso = new MetodosIngreso();
        public int apoyoCud(int _opcion, int _idapoyo, string _enunciado, string _link, string _imagen, string _titutlo, string _audio)
        {
            DataTable id = objIngreso.apoyoCud(_opcion, _idapoyo, _enunciado, _link, _imagen, _titutlo, _audio);
            DataRow fila = id.Rows[0];
            if (fila.ItemArray[0] is DBNull)
            {
                return 0;
            }
            return Convert.ToInt32(fila.ItemArray[0]);
        }

        /// <summary>
        /// <para>saber cuantas veces es usado un apoyo en una actividad</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idApoyo">apoyo a consultar</param>
        /// <returns></returns>
        public int apoyoContar(int _idActividad, int _idApoyo)
        {
            DataTable cuenta = objConsultasDB.apoyoContar(_idActividad, _idApoyo);
            DataRow contar = cuenta.Rows[0];
            if (contar.ItemArray[0] is DBNull)
            {
                return 0;
            }
            return Convert.ToInt32(contar.ItemArray[0]);
        }

        /// <summary>
        /// <para>Eiminar un apoyo</para>
        /// </summary>
        /// <param name="_idApoyo">apoyo a eliminar</param>
        public void apoyoEliminar(int _idApoyo)
        {
            objIngreso.apoyoEliminar(_idApoyo);
        }

        /// <summary>
        /// <para>Eliminar el apoyo asignado a una pregunta</para>
        /// </summary>
        /// <param name="_idPregunta">pregunta en la que se elimina el apoyo</param>
        public void eliminarApoyoPregunta(int _idPregunta)
        {
            objIngreso.eliminarApoyoPregunta(_idPregunta);
        }

        public string base64ToImg(string base64Png)
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + "" + "-imgReporte" + ".png";
            string fileNameWitPath = Path.Combine(HttpContext.Current.Server.MapPath("..//Img//imgRepo"), fileName);
            string base64 = base64Png.Remove(0, 22);

            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }
            return fileName;
        }
    }
}