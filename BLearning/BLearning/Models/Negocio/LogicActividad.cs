using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{
    public class LogicActividad
    {
        MetodosIngreso objIngresoActividad = new MetodosIngreso();
        MetodosConsultar objConsultasActividad = new MetodosConsultar();


        /// <summary>
        /// <para>consulta si una acitividad esta reportada</para>
        /// </summary>
        /// <param name="idActividad">Actividad a consultar</param>
        /// <returns>retorna 1 si esta reportada. 0 si no lo esta</returns>
        public int estaReportada(int idActividad)
        {
            int estado = 0;
            DataTable tabla = objConsultasActividad.estaReportada(idActividad);
            estado = Convert.ToInt32(tabla.Rows[0].ItemArray[0]);
            if (estado !=0)
            {
                estado = 1;
            }
            return estado;
        }

        /// <summary>
        /// <para>verificar que una actividad reportada ha sido revisada y ha sido eliminada o no</para>
        /// </summary>
        /// <param name="opcion">1 si se desea eliminar la actividad. 2 para ingresar que una actividad ha sido revisada</param>
        /// <param name="idActi">id de la actividad a trabajar</param>
        public void actividadRevisada(int opcion, int idActi)
        {
            objIngresoActividad.actividadRevisada(opcion, idActi);
        }
        
        /// <summary>
        /// <para>reportar una actividad para su revision</para>
        /// </summary>
        /// <param name="opcion">1 para ingresar un nuevo reporte. 2 para actualizar el estado de un reporte</param>
        /// <param name="_idActiRepor">0 si opcion es 1. id de ActiReporte a actualizar si opcion es 2</param>
        /// <param name="_idActi">id de la actividad a reportar</param>
        /// <param name="_idDocente">id del docente que la reporta</param>
        /// <param name="_fecha">fecha del reporte</param>
        /// <param name="_revisada">false si opcion es 1. true si opcion es 2</param>
        public void reportarActividad(int opcion, int _idActiRepor, int _idActi, int _idDocente, DateTime _fecha, bool _revisada)
        {
            objIngresoActividad.reportarActividad(opcion, _idActiRepor, _idActi, _idDocente, _fecha, _revisada);
        }

        /// <summary>
        /// <para>consultar las actividades pendientes de revision</para>
        /// </summary>
        /// <returns></returns>
        public List<ActividadesXDocenteEntidad> actividadesXRevisar()
        {
            List<ActividadesXDocenteEntidad> listActi = new List<ActividadesXDocenteEntidad>();
            foreach (DataRow item in objConsultasActividad.actividadesXRevisar().Rows)
            {
                listActi.Add(new ActividadesXDocenteEntidad
                {
                    idActividad = Convert.ToInt32(item.ItemArray[0]),
                    nPreguntas = Convert.ToInt32(item.ItemArray[1]),
                    duracion = Convert.ToInt32(item.ItemArray[2]),
                    descripcion = Convert.ToString(item.ItemArray[3]),
                    idNivel = Convert.ToInt32(item.ItemArray[4]),
                    docente = item.ItemArray[6].ToString(),
                    docenteReporta = item.ItemArray[7].ToString(),
                    reportadaEl = Convert.ToDateTime(item.ItemArray[8]).ToString("d"),
                    idActiRepor = Convert.ToInt32(item.ItemArray[9]),
                    Destrezas = consultarDestrzasXActividad(Convert.ToInt32(item.ItemArray[0])),
                });
            }
            return listActi;
        }

        /// <summary>
        /// <para>Consultar las actividades creadas para un nivel</para>
        /// </summary>
        /// <param name="_idNivel">nivel a consultar</param>
        /// <returns></returns>
        public List<ActividadesXDocenteEntidad> actividadesXNivel(int _idNivel)
        {
            List<ActividadesXDocenteEntidad> listAxN = new List<ActividadesXDocenteEntidad>();
            foreach (DataRow item in objConsultasActividad.actividadesXNivel(_idNivel).Rows)
            {
                if (actividadPonderacion(Convert.ToInt32(item.ItemArray[0])) == 100)
                {
                    listAxN.Add(new ActividadesXDocenteEntidad
                    {
                        idActividad = Convert.ToInt32(item.ItemArray[0]),
                        nPreguntas = Convert.ToInt32(item.ItemArray[1]),
                        duracion = Convert.ToInt32(item.ItemArray[2]),
                        descripcion = Convert.ToString(item.ItemArray[3]),
                        idNivel = Convert.ToInt32(item.ItemArray[4]),
                        creada = Convert.ToDateTime(item.ItemArray[6]).ToString("d"),
                        Destrezas = consultarDestrzasXActividad(Convert.ToInt32(item.ItemArray[0])),
                        docente = item.ItemArray[7].ToString(),
                        isReportad = estaReportada(Convert.ToInt32(item.ItemArray[0]))
                    });
                }
                
            }
            return listAxN;
        }

        /// <summary>
        /// <para>saber si una actividad ha sido resuelta y esta asignada</para>
        /// </summary>
        /// <param name="_idActi"></param>
        /// <returns></returns>
        public int actividadResueltaYAsignada(int _idActi)
        {
            return objConsultasActividad.actividadResueltaYasignada(_idActi);
        }

        /// <summary>
        /// <para>saber si una actividad ha sido resuelta</para>
        /// </summary>
        /// <param name="_idActi"></param>
        /// <returns></returns>
        public int actividadResuelta(int _idActi)
        {
            DataTable aaa = objConsultasActividad.actividadResuelta(_idActi);
            return Convert.ToInt32(aaa.Rows[0].ItemArray[0]);
        }

        /// <summary>
        /// <para>Saber el ponderacion total que tiene una actividad</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <returns></returns>
        public decimal actividadPonderacion(int _idActividad)
        {
            DataTable ponde = objConsultasActividad.actvidadPonderacion(_idActividad);
            return Convert.ToDecimal(ponde.Rows[0].ItemArray[0]);
        }

        /// <summary>
        /// <para>Eliminar una actividad, sus preguntas, opciones y respuestas</para>
        /// </summary>
        /// <param name="_idactividad">Actividad a eliminar</param>
        public void eliminarActivdad(int _idActividad)
        {
            objIngresoActividad.actividadEliminar(_idActividad);
        }

        public List<ActividadEntidad> actividadesModuloLeccion(int _idLeccion, int _idModulo)
        {
            List<ActividadEntidad> lisActividad = new List<ActividadEntidad>();
            ActividadEntidad actiEva = new ActividadEntidad();
            DataTable table = objConsultasActividad.ConsultarActividadXModuloLeccion(_idLeccion, _idModulo);
            foreach (DataRow item in objConsultasActividad.ConsultarActividadXModuloLeccion(_idLeccion, _idModulo).Rows)
            {
                lisActividad.Add(new ActividadEntidad
                    {
                        idActividad = Convert.ToInt32(item.ItemArray[0]),
                        nPreguntas = Convert.ToInt32(item.ItemArray[1]),
                        duracion = Convert.ToInt32(item.ItemArray[2]),
                        descripcion = Convert.ToString(item.ItemArray[3]),
                        tipo = Convert.ToString(item.ItemArray[6]),
                        fechaInicio = Convert.ToDateTime(item.ItemArray[7]).ToString("d"),
                        fechaFin = Convert.ToDateTime(item.ItemArray[8]).ToString("d"),
                    });
                
            }
            return lisActividad;
        }


        /// <summary>
        /// <para>consultar las destrezas que tiene una actividad</para>
        /// </summary>
        /// <param name="idActividad">Id de la actividad a consultar</param>
        /// <returns></returns>
        public string consultarDestrzasXActividad(int idActi)
        {
            string destrezas = "";
            foreach (DataRow item in objConsultasActividad.consultarDestrezasXActividad(idActi).Rows)
            {
                char dest = Convert.ToChar(item.ItemArray[0]);
                switch (dest)
                {
                    case 'G':
                        destrezas = "Grammar/Vocabulary, " + destrezas;
                        break;
                    case 'L':
                        destrezas = "Listening, "+ destrezas;
                        break;
                    case 'R':
                        destrezas = "Reading, " + destrezas;
                        break;
                    default:
                        break;
                }

            }
            return destrezas;
        }

        public List<ActividadesXDocenteEntidad> actiXDocente(int _idPersona)
        {
            List<ActividadesXDocenteEntidad> listAxD = new List<ActividadesXDocenteEntidad>();
            foreach (DataRow item in objConsultasActividad.consultarActividadesXDocente(_idPersona).Rows)
            {
                listAxD.Add(new ActividadesXDocenteEntidad
                {
                    idActividad = Convert.ToInt32(item.ItemArray[0]),
                    nPreguntas = Convert.ToInt32(item.ItemArray[1]),
                    duracion = Convert.ToInt32(item.ItemArray[2]),
                    descripcion = Convert.ToString(item.ItemArray[3]),
                    idNivel = Convert.ToInt32(item.ItemArray[4]),
                    fechaCreacion = Convert.ToDateTime(item.ItemArray[6]),
                    Destrezas = consultarDestrzasXActividad(Convert.ToInt32(item.ItemArray[0])),
                    isReportad = estaReportada(Convert.ToInt32(item.ItemArray[0]))
                });
            }
            return listAxD;
        }


        
        public List<ModulosXActiEntidad> modulXActiv(int _idActiv)
        {
            List<ModulosXActiEntidad> listMXD = new List<ModulosXActiEntidad>();
            string tipoAsig = "";
            foreach (DataRow item in objConsultasActividad.consultarModulosXActi(_idActiv).Rows)
            {
                char tipo = Convert.ToChar(item.ItemArray[6]);
                switch (tipo)
                {
                    case 'E':
                        tipoAsig = "Evaluative";
                        break;
                    case 'P':
                        tipoAsig = "Practice";
                        break;
                    default:
                        break;
                }
                listMXD.Add(new ModulosXActiEntidad
                    {
                        numNivel = Convert.ToString(item.ItemArray[0]),
                        numPara = Convert.ToString(item.ItemArray[1]),
                        numLeccion = Convert.ToString(item.ItemArray[2]),
                        fechaInicio = Convert.ToDateTime(item.ItemArray[3]).ToString("d"),
                        fechaFin = Convert.ToDateTime(item.ItemArray[4]).ToString("d"),
                        idActModulo = Convert.ToInt32(item.ItemArray[5]),
                        tipo = tipoAsig
                    });
            }
            return listMXD;
        }

        public PersonaEntidad consultarIngreso(string _user, string _pass, string _userType)
        {
            DataTable datos = objConsultasActividad.consultarIngreso(_user, _pass, _userType);
            PersonaEntidad objPersona = new PersonaEntidad();
            if (datos.Rows.Count != 0)
            {
                DataRow fila = datos.Rows[0];

                objPersona.idPersona = Convert.ToInt32(fila.ItemArray[0]);
                objPersona.nombres = Convert.ToString(fila.ItemArray[1]);
                objPersona.apellidos = Convert.ToString(fila.ItemArray[2]);
            }

            return objPersona;
        }

        /// <summary>
        /// <para>Ingresar o editar una actividad</para>
        /// </summary>
        /// <param name="_opcion">2 para editar. 3 para ingresar</param>
        /// <param name="_idactivi"></param>
        /// <param name="_npreguntas"></param>
        /// <param name="_duracion"></param>
        /// <param name="_tipo"></param>
        /// <param name="_descripcion"></param>
        /// <returns></returns>
        public int actividadCud(int _opcion, int _idactivi, int _npreguntas, int _duracion, string _descripcion, int _idNivel)
        {
            DataTable id = objIngresoActividad.actividadCud(_opcion, _idactivi, _npreguntas, _duracion, _descripcion, _idNivel);

            DataRow fila = id.Rows[0];
            if (fila.ItemArray[0] is DBNull)
            {
                return 0;
            }
            return Convert.ToInt32(fila.ItemArray[0]);
        }

        public void actvDocenteCud(int _opcion, int _idActDocente, int _idDocente, int _idActi, DateTime _fecha)
        {
            objIngresoActividad.activDocenteCud(_opcion, _idActDocente, _idDocente, _idActi, _fecha);
        }

        /// <summary>
        /// <para>Actualizar los campos nPreguntas y duracion de una actividad. nPreguntas no se envia</para>
        /// </summary>
        /// <param name="_opcion">1 para sumar. 2 para restar</param>
        /// <param name="_idactivi"></param>
        /// <param name="_duracion"></param>
        public void actividadActualizar(int _opcion, int _idactivi, int _duracion)
        {
            objIngresoActividad.actividadUpdate(_opcion, _idactivi, _duracion);
        }



        //metodos Nathy
        public ActividadEntidad consultarActividad(int _idActividad)
        {
            ActividadEntidad objActividad = new ActividadEntidad();
            foreach (DataRow item in objConsultasActividad.consultarActividad(_idActividad).Rows)
            {
                objActividad.idActividad = Convert.ToInt32(item.ItemArray[0]);
                objActividad.nPreguntas = Convert.ToInt32(item.ItemArray[1]);
                objActividad.duracion = Convert.ToInt32(item.ItemArray[2]);
                objActividad.descripcion = item.ItemArray[3].ToString();
            }
            return objActividad;
        }

        public ActividadModuloEntidad consultarActividadModuloXidActMod(int _idActModulo)
        {
            ActividadModuloEntidad objActividad = new ActividadModuloEntidad();
            foreach (DataRow item in objConsultasActividad.consultarActividadModuloXidActModulo(_idActModulo).Rows)
            {
                objActividad.idActModulo = Convert.ToInt32(item.ItemArray[0]);
                objActividad.tipo = item.ItemArray[6].ToString();
            }
            return objActividad;
        }


        public List<ActividadEntidad> actividadesPerdidasLista()
        {
            List<ActividadEntidad> _listaActi = new List<ActividadEntidad>();

            foreach (DataRow item in objConsultasActividad.actvidadesPerdidasConsultar().Rows)
            {
                _listaActi.Add(new ActividadEntidad
                {
                    idActividad = Convert.ToInt32(item.ItemArray[0]),
                    nPreguntas = Convert.ToInt32(item.ItemArray[1]),
                    duracion = Convert.ToInt32(item.ItemArray[2]),
                    descripcion = item.ItemArray[3].ToString(),
                    fechaInicio = Convert.ToDateTime(item.ItemArray[6]).ToString("d")
                });
            }

            return _listaActi;
        }
    }
}