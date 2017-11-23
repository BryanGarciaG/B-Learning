using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicLeccion
    {
        MetodosConsultar objConsultas = new MetodosConsultar();
        MetodosIngreso objIngreso = new MetodosIngreso();

        public List<LeccionEntidad> consultarLeccion(int _idNivel)
        {
            DataTable datos = objConsultas.consultarLeccion(_idNivel);
            List<LeccionEntidad> listaLeccion = new List<LeccionEntidad>();
            if (datos.Rows.Count != 0)
            {
                foreach (DataRow item in datos.Rows)
                {
                    listaLeccion.Add(new LeccionEntidad
                    {
                        idLeccion = Convert.ToInt32(item.ItemArray[0]),
                        numLeccion = Convert.ToString(item.ItemArray[1]),
                        idNivel = Convert.ToInt32(item.ItemArray[2]),
                        isActive = Convert.ToBoolean(item.ItemArray[3])
                    });
                }
            }
            return listaLeccion;
        }

        /// <summary>
        /// <para>Consultar cuantas actividades estan asignadas a una leccion dentro de un modulo</para>
        /// </summary>
        /// <param name="_idLeccion">leccion a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public  int consultarCantActi(int _idLeccion, int _idModulo)
        {
            DataTable valor = objConsultas.consultarActividadesAsigModuloLeccion(_idLeccion, _idModulo);
            return Convert.ToInt32(valor.Rows[0].ItemArray[0]);
        }

        /// <summary>
        /// <para>consutar las lecciones asignadas un determinado modulo y nivel</para>
        /// </summary>
        /// <param name="numeroNivel">id del nivel a consultar</param>
        /// <param name="idModulo">ide del modulo a consultar</param>
        /// <returns></returns>
        public List<LeccionEntidad> consultarLeccionesYNumActi(int numeroNivel,int idModulo)
        {
            List<LeccionEntidad> listaLecciones = new List<LeccionEntidad>();
            foreach (DataRow item in objConsultas.consultarLecciones(numeroNivel).Rows)
            {
                listaLecciones.Add(new LeccionEntidad
                {
                    idLeccion = Convert.ToInt32(item.ItemArray[0]),
                    numLeccion = item.ItemArray[1].ToString(),
                    idNivel = Convert.ToInt32(item.ItemArray[2]),
                    cantActiAsig = consultarCantActi(Convert.ToInt32(item.ItemArray[0]), idModulo)
                });
            }
            return listaLecciones;
        }

        public List<LeccionEntidad> consultarLecciones(int numeroNivel)
        {
            List<LeccionEntidad> listaLecciones = new List<LeccionEntidad>();
            foreach (DataRow item in objConsultas.consultarLecciones(numeroNivel).Rows)
            {
                listaLecciones.Add(new LeccionEntidad
                {
                    idLeccion = Convert.ToInt32(item.ItemArray[0]),
                    numLeccion = item.ItemArray[1].ToString(),
                    idNivel = Convert.ToInt32(item.ItemArray[2])
                });
            }
            return listaLecciones;
        }

        /// <summary>
        /// <para>consultar cuantas lecciones activas tiene cada nivel</para>
        /// </summary>
        /// <returns></returns>
        public List<LeccionesXNivelEntidad> consultarLeccionesAllNivel()
        {
            List<LeccionesXNivelEntidad> listaLeccionXNivel = new List<LeccionesXNivelEntidad>();
            
            for (int i = 0; i <= 7; i++)
            {
                foreach (DataRow item in objConsultas.consultarLeccionesActivasXnivel(i+1).Rows)
	            {
	        	  listaLeccionXNivel.Add(new LeccionesXNivelEntidad
                    {
                        idNivel = Convert.ToInt32(item.ItemArray[0]),
                        cantLecciones = Convert.ToInt32(item.ItemArray[1])
                    });
	            }
            }
            return listaLeccionXNivel;
        }

        /// <summary>
        /// <para>actualizar los estados d elas lecciones por nivel</para>
        /// </summary>
        /// <param name="idNivel">nivel a actuaizar lecciones</param>
        /// <param name="newLeccionesActivas">cantidad de lecciones activas</param>
        public void actualizarEstadoLeccion(int idNivel, int newLeccionesActivas)
        {
            List<LeccionEntidad> listLeccion = consultarLeccion(idNivel);
            if (listLeccion.Count<=newLeccionesActivas)
            {
                for (int i = 0; i < newLeccionesActivas; i++)
                {
                    if (i < listLeccion.Count)
                    {
                        if (listLeccion[i].isActive != true)
                        {
                            objIngreso.actualizarEstadoLeccion(listLeccion[i].idLeccion, true);
                        }
                    }
                    else
                    {
                        string numleccion = "";
                        if (i < 10)
                        {
                            numleccion = "L0" + (i + 1);
                        }
                        else
                        {
                            numleccion = "L" + (i + 1);
                        }
                        objIngreso.leccionCud(3, 0, numleccion, idNivel, true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < listLeccion.Count; i++)
                {
                    if (i < newLeccionesActivas)
                    {
                        //if (listLeccion[i].isActive != true)
                        //{
                            objIngreso.actualizarEstadoLeccion(listLeccion[i].idLeccion, true);
                            continue;
                        //}
                    }
                    objIngreso.actualizarEstadoLeccion(listLeccion[i].idLeccion, false);
                }
            }
        }

        public int leccionCud(int _opcion, int _idLeccion, string _numLeccion, int _idNivel)
        {
            DataTable id = objIngreso.leccionCud(_opcion, _idLeccion, _numLeccion, _idNivel);
            DataRow fila = id.Rows[0];
            return Convert.ToInt32(fila.ItemArray[0]);
        }

        /// <summary>
        /// <para>consultar todas las lecciones asignadas a un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public List<LeccionEntidad> consultarLeccionesConActividades(int _idModulo)
        {
            List<LeccionEntidad> listaLecciones = new List<LeccionEntidad>();
            foreach (DataRow item in objConsultas.consultarLeccionesXModulo(_idModulo).Rows)
            {
                listaLecciones.Add(new LeccionEntidad
                {
                    idLeccion = Convert.ToInt32(item.ItemArray[0]),
                    numLeccion = item.ItemArray[1].ToString(),
                });
            }
            return listaLecciones;
        }
    }
}