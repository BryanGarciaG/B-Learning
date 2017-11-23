using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLearning.Models.Datos;
using System.Data;

namespace BLearning.Models.Negocio
{
    public class LogicReporteDocente
    {
        MetodosConsultar _objMetConsultar = new MetodosConsultar();
        /// <summary>
        /// <para>Consultar el promedio de los estudiantes en un ciclo, ciclo/nivel o ciclo/nivel/paralelo</para>
        /// </summary>
        /// <param name="_opcion">1 para consultar ciclo/nivel. 2 para consultar ciclo. 3 para ciclo/nivel/paralelo</param>
        /// <param name="_idCiclo">id del ciclo a consultar</param>
        /// <param name="_idNivel">id del nivel a consultar. si "_opcion" es 2, esta variable debe ser 0</param>
        /// <param name="_numParalelo">id del paralelo a consultar. si "_opcion" es 1 o 2, esta variable debe ser 0</param>
        /// <param name="_idTipoModulo">tipo del modulo que se consulta que consulta</param>
        /// <returns></returns>
        public List<EstudiantePromedioEntidad> ConsultarPosicionesPastel(int _opcion, int _idCiclo, int _idNivel, string _numParalelo, int _idTipoModulo)
        {
            List<EstudiantePromedioEntidad> listPromeEstudi = new List<EstudiantePromedioEntidad>();
            foreach (DataRow item in _objMetConsultar.consultarPromedios(_opcion,_idCiclo,_idNivel,_numParalelo,_idTipoModulo).Rows)
            {
                listPromeEstudi.Add(new EstudiantePromedioEntidad
                {
                    idEstudiante = Convert.ToInt32(item.ItemArray[0]),
                    nombres = item.ItemArray[1].ToString(),
                    promedio = Convert.ToDecimal(item.ItemArray[2])
                });
            }
            return listPromeEstudi;
        }

        /// <summary>
        /// <para>consultar cuantas actividades han resuelto los estudiantes en un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        public List<ActividadesResueltasEntidad> consultarActiResueltasXModulo(int _idModulo)
        {
            LogicReporteEstudiante objReporteEntidad = new LogicReporteEstudiante();
            List<ActividadesResueltasEntidad> listPXME = new List<ActividadesResueltasEntidad>();
            LogicReporteEstudiante listPromeEstudi = new LogicReporteEstudiante();
            foreach (DataRow item in _objMetConsultar.consultarActiResueltasXModulo(_idModulo).Rows)
            {
                listPXME.Add(new ActividadesResueltasEntidad
                {
                    idAlumno = Convert.ToInt32(item.ItemArray[0]),
                    nombres = item.ItemArray[1].ToString(),
                    cantidadActiResu = Convert.ToInt32(item.ItemArray[2]),
                    objProLecc = objReporteEntidad.CalificacionesXleccion(_idModulo, Convert.ToInt32(item.ItemArray[0]))
                });
            }
            return listPXME;
        }

        

        /// <summary>
        /// <para>consutar los promedios d elos estudiantes de un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public List<EstudiantePromedioEntidad> ConsultarPosicionesXModulo(int _idModulo)
        {
            List<EstudiantePromedioEntidad> listPromeEstudi = new List<EstudiantePromedioEntidad>();
            foreach (DataRow item in _objMetConsultar.consultarPromediosXmodulo(_idModulo).Rows)
            {
                listPromeEstudi.Add(new EstudiantePromedioEntidad
                    {
                        idEstudiante = Convert.ToInt32(item.ItemArray[0]),
                        nombres = item.ItemArray[1].ToString(),
                        promedio = Math.Round(Convert.ToDecimal(item.ItemArray[2]),2)
                    });
            }
            return listPromeEstudi;
        }

        /// <summary>
        /// <para>consutar los promedios de los estudiantes de una actividad dentro de un modulo</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public List<EstudiantePromedioEntidad> consultarPromediosXModuloActividad(int _idActividad, int _idModulo)
        {
            List<EstudiantePromedioEntidad> listEsuPromed = new List<EstudiantePromedioEntidad>();
            foreach (DataRow item in _objMetConsultar.consultarPromediosXmoduloActividad(_idActividad, _idModulo).Rows)
            {
                listEsuPromed.Add(new EstudiantePromedioEntidad
                    {
                        nombres = item.ItemArray[0].ToString(),
                        promedio = Convert.ToDecimal(item.ItemArray[1])
                    });
            }
            return listEsuPromed;
        }

        /// <summary>
        /// <para>consutar que estudiantes han resuelto una actividad practica dentro de un modulo</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public List<EstudiantePromedioEntidad> consultarPromediosXModuloActividadPractica(int _idActividad, int _idModulo)
        {
            List<EstudiantePromedioEntidad> listEstuDone = new List<EstudiantePromedioEntidad>();
            foreach (DataRow item in _objMetConsultar.consultarPromediosXmoduloActividadPractica(_idActividad, _idModulo).Rows)
            {
                listEstuDone.Add(new EstudiantePromedioEntidad
                {
                    nombres = item.ItemArray[0].ToString(),
                    vecesResuelto = Convert.ToInt32(item.ItemArray[1].ToString()),
                    duracion = Convert.ToInt32(item.ItemArray[2].ToString())
                });
            }
            return listEstuDone;
        }


        public List<EstudiantePromedioEntidad> consultarEfectividadXModuloActividad(int _idActividad, int _idModulo)
        {
            LogicPersona _objPersona = new LogicPersona();
            List<EstudiantePromedioEntidad> listEsuPromed = new List<EstudiantePromedioEntidad>();
            List<PersonaEntidad> listPersona = _objPersona.consultarEstudiantesXModulo(_idModulo);
            int cont = 0;
            for (int i = 0; i < listPersona.Count; i++)
            {
                DataTable efecti = _objMetConsultar.consultarEfectividadXmoduloActividad(_idActividad, _idModulo,listPersona[i].idAlumno);
                if (efecti.Rows.Count != 0)
                {
                    listEsuPromed.Add(new EstudiantePromedioEntidad
                    {
                        idEstudiante = listPersona[i].idAlumno,
                        nombres = listPersona[i].nombres
                    });

                    foreach (DataRow item in efecti.Rows)
                    {
                        if (item.ItemArray[0].ToString() == "L")
                        {
                            listEsuPromed[cont].listening = Math.Round(Convert.ToDecimal(item.ItemArray[1]),2);
                        }
                        if (item.ItemArray[0].ToString() == "R")
                        {
                            listEsuPromed[cont].reading = Math.Round(Convert.ToDecimal(item.ItemArray[1]), 2);
                        }
                        if (item.ItemArray[0].ToString() == "G")
                        {
                            listEsuPromed[cont].gramar = Math.Round(Convert.ToDecimal(item.ItemArray[1]), 2);
                        }
                    }

                    cont++;

                }
                
            }
           
            return listEsuPromed;
        }

        public decimal? consultarCalificacion(int _idEstudiante, int _idModulo)
        {
            decimal sumaCalificacion = 0;
            decimal? califiTotal = null;
            int numLecciones = 0;
            List<RendimientoLeccionEntidad> _objRendimientoLeccionCalificaciones = new List<RendimientoLeccionEntidad>();
            LogicReporteEstudiante objReporteEntidad = new LogicReporteEstudiante();
            _objRendimientoLeccionCalificaciones = objReporteEntidad.CalificacionesXleccion(_idModulo,_idEstudiante);
            if (_objRendimientoLeccionCalificaciones.Count() != 0)
            {
                foreach (var item in _objRendimientoLeccionCalificaciones)
                {
                    sumaCalificacion = sumaCalificacion + Math.Round(item.valor, 2);
                    numLecciones = numLecciones + 1;
                }
                califiTotal = Math.Round(sumaCalificacion / numLecciones, 2);
            }
            
            return califiTotal;
        }
    }
}