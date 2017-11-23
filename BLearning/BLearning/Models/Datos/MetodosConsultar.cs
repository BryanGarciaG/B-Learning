using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BLearning.Models.Datos
{
    public class MetodosConsultar
    {
        Conexion conexionServidor = new Conexion();
        public SqlCommand varComando = new SqlCommand();


        public DataTable actvidadesPerdidasConsultar()
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadesPerdidas");
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consulta si una acitividad esta reportada</para>
        /// </summary>
        /// <param name="idActividad">Actividad a consultar</param>
        /// <returns></returns>
        public DataTable estaReportada(int idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CnsultarSiReportada");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>consultar las actividades pendientes de revision</para>
        /// </summary>
        /// <returns></returns>
        public DataTable actividadesXRevisar()
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarActiReportadas");
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consultar el id de un modulo</para>
        /// </summary>
        /// <param name="_idTipoModulo">id del tipo de modulo</param>
        /// <param name="_ciclo">id del ciclo</param>
        /// <param name="_nivel">id del nivel</param>
        /// <param name="_numPara">numero del paralelo</param>
        /// <returns></returns>
        public DataTable consultarIdModulo(int _idTipoModulo, int _ciclo, int _nivel, string _numPara)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_consultarIdModulo");
            varComando.Parameters.Add("@idCiclo", SqlDbType.Int).Value = _ciclo;
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _nivel;
            varComando.Parameters.Add("@numPara", SqlDbType.VarChar).Value = _numPara;
            varComando.Parameters.Add("@idTipoModulo", SqlDbType.Int).Value = _idTipoModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consultar las actividades creadas para un nivel</para>
        /// </summary>
        /// <param name="_idNivel">nivel a consultar</param>
        /// <returns></returns>
        public DataTable actividadesXNivel(int _idNivel)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarActiviXNivel");
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consultar las destrezas que tiene una actividad</para>
        /// </summary>
        /// <param name="idActividad">Id de la actividad a consultar</param>
        /// <returns></returns>
        public DataTable consultarDestrezasXActividad(int idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_DestrezaXActividad");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>Consultar todos los tipos de pregunta y sus Id</para>
        /// </summary>
        /// <returns></returns>
        public DataTable tiposPreguntaConsultar()
        {
            varComando = conexionServidor.crearComandoProcedi("sp_TipoPreguntaConsultar");
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consulta todos los estudiantes de un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarEstudiantesXModulo(int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarEstudiantesXmodulo");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consutar cuantas lecciones activas existen en un nivel</para>
        /// </summary>
        /// <param name="_idNivel">nivel a consultar</param>
        /// <returns></returns>
        public DataTable consultarLeccionesActivasXnivel(int _idNivel)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarLeccionesActivasXnivel");
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consutar que estudiantes han resuelto una actividad practica dentro de un modulo</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarPromediosXmoduloActividadPractica(int _idActividad, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_calificacionXactividadPractica");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>consutar los promedios de los estudiantes de una actividad dentro de un modulo</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarPromediosXmoduloActividad(int _idActividad, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_calificacionXactividad");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>consutar la calificacion por efectividad de los estudiantes de una actividad dentro de un modulo</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarEfectividadXmoduloActividad(int _idActividad, int _idModulo, int _idEstudiante)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EfectividadXmodulo");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante; 
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>saber cuantas veces es usado un apoyo en una actividad</para>
        /// </summary>
        /// <param name="_idActividad">actividad a consultar</param>
        /// <param name="_idApoyo">apoyo a consultar</param>
        /// <returns></returns>
        public DataTable apoyoContar(int _idActividad, int _idApoyo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ApoyoContar");
            varComando.Parameters.Add("@idActivdad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idApoyo", SqlDbType.Int).Value = _idApoyo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consutar los promedios de los estudiantes de una actividad dentro de un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarPromediosXmodulo(int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ReportePastelCalificaciones");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consultar el promedio de los estudiantes en un ciclo o un ciclo/nivel</para>
        /// </summary>
        /// <param name="_opcion">1 para consultar ciclo/nivel. 2 para consultar ciclo. 3 para ciclo/nivel/paralelo</param>
        /// <param name="_idCiclo">id del ciclo a consultar</param>
        /// <param name="_idNivel">id del nivel a consultar. si "_opcion" es 2, esta variable debe ser 0</param>
        /// <param name="_idParalelo">id del paralelo a consultar. si "_opcion" es 2, esta variable debe ser 0</param>
        /// <param name="_idTipoModulo">id del docente que consulta</param>
        /// <returns></returns>
        public DataTable consultarPromedios(int _opcion, int _idCiclo, int _idNivel, string _numParalelo, int _idTipoModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ReportePastelXCiclo/Nivel");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            varComando.Parameters.Add("@idCiclo", SqlDbType.Int).Value = _idCiclo;
            varComando.Parameters.Add("@numParalelo", SqlDbType.VarChar).Value = _numParalelo;
            varComando.Parameters.Add("@idTipoModulo", SqlDbType.Int).Value = _idTipoModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consultar cuantas actividades han resuelto los estudiantes en un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarActiResueltasXModulo(int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadesResueltasXModulo");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el historial de acceso al sistema de una persona</para>
        /// </summary>
        /// <param name="_nombres">Apellidos y nombres</param>
        /// <returns></returns>
        public DataTable consultarHistorialDeAcceso(string _nombres)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_consultarHistorialDeAcceso");
            varComando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = _nombres;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Predice el nombre que esta escribiendo el usuario</para>
        /// </summary>
        /// <param name="_nombres">Apellidos y nombres</param>
        /// <returns></returns>
        public DataTable predecirNombre(string _nombres)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_predecirNonbre");
            varComando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = _nombres;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consultar cuantas actividades estan asignadas a una leccion dentro de un modulo</para>
        /// </summary>
        /// <param name="_idLeccion">leccion a consultar</param>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarActividadesAsigModuloLeccion(int _idLeccion, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadsAsignadasXLeccionModulo");
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>consultar todas las lecciones asignadas a un modulo</para>
        /// </summary>
        /// <param name="_idModulo">modulo a consultar</param>
        /// <returns></returns>
        public DataTable consultarLeccionesXModulo(int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarLeccionesXModulo");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>saber si una actividad ha sido resuelta y esta asignada</para>
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <returns></returns>
        public int actividadResueltaYasignada(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadResueltaYAsignada");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>saber si una actividad ha sido resuelta</para>
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <returns></returns>
        public DataTable actividadResuelta(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActvidadResueltaQuiz");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Saber el ponderacion total que tiene una actividad</para>
        /// </summary>
        /// <param name="_idActividad">Actividad a consultar</param>
        /// <returns></returns>
        public DataTable actvidadPonderacion(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadPonderacion");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Para saber si la actividad asignada ya ha sido resuelta</para>
        /// </summary>
        /// <param name="_idActiModu">ID de la asignacion a verficar</param>
        /// <returns></returns>
        public DataTable asignacionResuelta(int _idActiModu)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadModuloNoResuelto");
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActiModu;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>eliminar una asignacion de una actividad</para>
        /// </summary>
        /// <param name="_idActiModulo">ID de la asignacion a eliminar</param>
        public void asignacionesEliminar(int _idActiModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadModuloEliminar");
            varComando.Parameters.Add("@idActiModu", SqlDbType.Int).Value = _idActiModulo;
            conexionServidor.ejecutarComandoConsultar(varComando);
        }
        public DataTable consultarPreguntasXActividad(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_PreguntaConsultarXActividad");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarIngreso(string _user, string _pass, string _userType) 
        {
            varComando = conexionServidor.crearComandoProcedi("sp_IngresoConsultar");
            varComando.Parameters.Add("@user", SqlDbType.NVarChar).Value = _user;
            varComando.Parameters.Add("@pass", SqlDbType.NVarChar).Value = _pass;
            varComando.Parameters.Add("@userType", SqlDbType.NVarChar).Value = _userType;

            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarCursosDocente(int _idPersona)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CursosConsultar");
            varComando.Parameters.Add("@idPersona", SqlDbType.Int).Value = _idPersona;

            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarLeccion(int _idNivel)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_LeccionConsultarXNivel");
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable ConsultarActividadXModuloLeccion(int _idLeccion, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_Consultar_ActividadXModuloLeccion");
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Lista de docentes con el numero de actividades creadas en el ultimo ciclo</para>
        /// </summary>
        /// <returns></returns>
        public DataTable Consultar_NumDeActividadesCreadasXciclo()
        {
            varComando = conexionServidor.crearComandoProcedi("sp_Consultar_NumDeActividadesCreadasXciclo");
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Retorna las actividades que ha creado una persona en el ultimo ciclo</para>
        /// </summary>
        /// <param name="_idPersona">Identificador de la persona de la que quiere consultar</param>
        /// <returns></returns>

        public DataTable Consultar_ReporteDeActividadesXDocente(int _idPersona)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_Consultar_ReporteDeActividadesXDocente");
            varComando.Parameters.Add("@idPersona", SqlDbType.Int).Value = _idPersona;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarActividadesXDocente(int _idPersona)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_Consultar_ActividadesXDocente");
            varComando.Parameters.Add("@idPersona", SqlDbType.Int).Value = _idPersona;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarModulosXActi(int _idActi)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarModulosXActividad");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActi;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        //Metodos Nathy

        /// <summary>
        /// <para>Hace una consulta del usuario para acceder al sistema</para>
        /// </summary>
        /// <param name="_usuario">Número de cédula del usuario</param>
        /// <param name="_clave">Clave de acceso</param>
        /// <param name="_rol">El tipo de usuario con el que accede</param>
        /// <returns>Datos de la persona</returns>
        public DataTable loginUsuarios(string _usuario, string _clave, string _rol)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_loginUsuarios");
            varComando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = _usuario;
            varComando.Parameters.Add("@clave", SqlDbType.VarChar).Value = _clave;
            varComando.Parameters.Add("@rol", SqlDbType.VarChar).Value = _rol;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Hace una consulta del usuario estudiante para acceder al sistema</para>
        /// </summary>
        /// <param name="_usuario">Número de cédula del usuario</param>
        /// <returns>Datos de la persona</returns>
        public DataTable loginEstudiante(string _usuario)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_LoginEstudiante");
            varComando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = _usuario;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consuta los datos del módulo en que está matriculado el alumno y los datos del alumno</para>
        /// </summary>
        /// <param name="_cedula">Número de documento del estudiante</param>
        /// <returns>Datos del módulo y del alumno</returns>
        public DataTable consultaModuloDeAlumno(string _cedula)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarModuloAlumnoCedula");
            varComando.Parameters.Add("@cedul", SqlDbType.VarChar).Value = _cedula;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta un registro de la tabla actividad con un idActividad especificado</para>
        /// </summary>
        /// <param name="_idActividad">Recibe el idActividad de la actividad que se va a consultar</param>
        /// <returns>Todos los campos de la tabla Actividad con el idActividad consultado</returns>
        public DataTable consultarActividad(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadConsultarId");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>Consulta un registro en la tabla actividad que está asignada a un modulo en la tabla ActividadModulo</para>
        /// </summary>
        /// <param name="_idActModulo">Recibe el id de la tabla ActividadModulo</param>
        /// <returns>Todos los campos de la tabla Actividad Asignada con el idActividadModulo consultado</returns>
        public DataTable consultarActividadModuloXidActModulo(int _idActModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadModuloConsultarXidActModul");
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el encabezado de la pregunta de una actividad</para>
        /// </summary>
        /// <param name="_idActModulo">id de la actividad asignada a un módulo</param>
        /// <param name="_numPregunta">número de la pregunta en la actividad a consultar</param>
        /// <returns>Encabezado de la pregunta especificada de una actividad asignada a un módulo</returns>
        public DataTable consultarPregunta(int _idActModulo, int _numPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_PreguntaConsultarId");
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActModulo;
            varComando.Parameters.Add("@numPregunta", SqlDbType.Int).Value = _numPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el encabezado de la pregunta de una actividad</para>
        /// </summary>
        /// <param name="_idActModulo">id de la actividad asignada a un módulo</param>
        /// <param name="_numPregunta">número de la pregunta en la actividad a consultar</param>
        /// <returns>Encabezado de la pregunta especificada de una actividad asignada a un módulo</returns>
        public DataTable consultarPreguntaActividad(int _idActividad, int _numPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_PreguntaConsultarIdActividad");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@numPregunta", SqlDbType.Int).Value = _numPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta las opciones de una pregunta</para>
        /// </summary>
        /// <param name="_idPregunta">identificaador de la pregunta</param>
        /// <returns>Opciones de la pregunta especificada</returns>
        public DataTable consultarOpciones(int _idPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_OpcionesConsultarId");
            varComando.Parameters.Add("@idPregunta", SqlDbType.Int).Value = _idPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta las respuestas de una pregunta</para>
        /// </summary>
        /// <param name="_idPregunta">identificador de la pregunta</param>
        /// <returns>Respuestas de la pregunta especificada</returns>
        public DataTable consultarRespuestaId(int _idPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_RespuestaConsultarId");
            varComando.Parameters.Add("@idPregunta", SqlDbType.Int).Value = _idPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el apoyo para resolver una pregunta</para>
        /// </summary>
        /// <param name="_idApoyo">identificador del apoyo guardado en una pregunta</param>
        /// <returns>link de video, nombre de una imagen, o un texto de apoyo para la pregunta </returns>
        public DataTable consultarApoyoId(int _idApoyo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ApoyoConsultarId");
            varComando.Parameters.Add("@idApoyo", SqlDbType.Int).Value = _idApoyo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el tipo de pregunta de una pregunta</para>
        /// </summary>
        /// <param name="_idTipoPregunta">identificador del tipo de pregunta registrado en la pregunta</param>
        /// <returns>El identificador del tipo y el tipo de la pregunta</returns>
        public DataTable consultarTipoPreguntaId(int _idTipoPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_TipoPreguntaConsultaId");
            varComando.Parameters.Add("@idTipoPregunta", SqlDbType.Int).Value = _idTipoPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el encabezado de la pregunta de una actividad</para>
        /// </summary>
        /// <param name="_idPregunta">id de la pregunta a consultar</param>
        /// <returns>Encabezado de la pregunta especificada de una actividad asignada a un módulo</returns>
        public DataTable consultarPreguntaIdPregunta(int _idPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_PreguntaConsultarIdPregunta");
            varComando.Parameters.Add("@idPregunta", SqlDbType.Int).Value = _idPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el número de peguntas que tiene la actividad</para>
        /// </summary>
        /// <param name="_idActMod">Identificador de la actividad asignada a un módulo</param>
        /// <returns>Número de preguntas que tiene una actividad</returns>
        public DataTable consultarNumeroPreguntasActividad(int _idActMod)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_NumPreguntasActividad");
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActMod;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el número de peguntas que tiene la actividad</para>
        /// </summary>
        /// <param name="_idActividad">Identificador de la actividad</param>
        /// <returns>Número de preguntas que tiene una actividad</returns>
        public DataTable consultarNumeroPreguntas(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_NumPreguntasActividad2");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el idRespuesta, idOpciones y el detale de la respuesta para verificar la respuesta de la pregunta</para>
        /// </summary>
        /// <param name="_idPregunta">identificador de la pregunta a verificar</param>
        /// <returns>idRespuesta, idOpciones y el detale de la pregunta a verificar</returns>
        public DataTable verificadorRespuestaPertenencia(int _idPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_VerificarRespuestaPertenencia");
            varComando.Parameters.Add("@idPregunta", SqlDbType.Int).Value = _idPregunta;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consuta la cabecera de un quiz de X actividad resuelto por Y estudiante</para>
        /// </summary>
        /// <param name="_idActividad">identificador de la actividad resuelta a consultar</param>
        /// <param name="_idEstudiante">identificdor del estudiante</param>
        /// <returns>Quiz de acuerdo al parameto idActividad e idEstuiante</returns>
        public DataTable consultarQuizPorActividad(int _idActividad, int _idEstudiante)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_QuizConsultarActividad");
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el detalle del quiz </para>
        /// </summary>
        /// <param name="_idQuiz"></param>
        /// <param name="_idOpcion"></param>
        /// <returns></returns>
        public DataTable consultarDetalleQuizPorQuizYopcion(int _idQuiz, int _idOpcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_DetalleQuizConsultarXquiz");
            varComando.Parameters.Add("@idQuiz", SqlDbType.Int).Value = _idQuiz;
            varComando.Parameters.Add("@idOpccion", SqlDbType.Int).Value = _idOpcion;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el listado de las lecciones de un nivel</para>
        /// </summary>
        /// <param name="_numeroNivel">nivel a consultar</param>
        /// <returns>Lista de lecciones</returns>
        public DataTable consultarLecciones(int _numeroNivel)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_LeccionesConsultar");
            varComando.Parameters.Add("@numNivel", SqlDbType.Int).Value = _numeroNivel;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        //Consulta lista de actividades
        public DataTable consultarListaActividades(int _idModulo, int _idLeccion, int _idEstudiante)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadesXmoduloConsultar");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }
        
        //Consultas a las tablas de efectividad, detallede efectividad 
        public DataTable consultarCalificacionIdQuiz(int _idQuiz)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CalificacionConsultarIdQuiz");
            varComando.Parameters.Add("@idQuiz", SqlDbType.Int).Value = _idQuiz;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarDetalleCalifXcalifYparam(int _idCalificacion, string _parametro)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_DetalleCalifConsultarXcalifYparam");
            varComando.Parameters.Add("@idCalificacion", SqlDbType.Int).Value = _idCalificacion;
            varComando.Parameters.Add("@destreza", SqlDbType.Char).Value = _parametro;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarDetalleCalifiXidCalif(int _idCalificacion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_DetalleCalifConsultarXidCalif");
            varComando.Parameters.Add("@idCalificacion", SqlDbType.Int).Value = _idCalificacion;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta la calificicion de una actividad resuelta</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idActividadModulo"></param>
        /// <returns></returns>
        public DataTable consultarCalificacionObtenidaActividad(int _idEstudiante, int _idActividadModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CalificacionActividad");
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@idActividadModulo", SqlDbType.Int).Value = _idActividadModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable consultarCalificActividad(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_consultarCalifActividad");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        

        public DataTable CalificacionesXleccion(int _idEstudiante, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CalificacionXleccionesReporte");
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Muestra una lista de las calificaciones de los estudiantes en las actividades de una leccion</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idLeccion"></param>
        /// <param name="_idModulo"></param>
        /// <returns></returns>
        public DataTable CalificacionesDeActividadesXleccion(int _idEstudiante, int _idLeccion, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CalificacionXleccion");
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable efectividadDeRespuestasEvaluativasXestudiante(int _idEstudiante, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_efectividadDeRespuestasEvaluativasXestudiante");
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consuta la efectividad de un estudiante en una actividad específica</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idModulo"></param>
        /// <returns></returns>
        public DataTable efectividadXactividadEstudiante(int _idEstudiante, int _idActividadModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EfectividadXactividad");
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@idActividadModulo", SqlDbType.Int).Value = _idActividadModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }
        /// <summary>
        /// <para>Consulta el porcentaje de lecciones completadas</para>
        /// </summary>
        /// <param name="_idEstudiante"></param>
        /// <param name="_idModulo"></param>
        /// <param name="_tipo">Ingresar P (Práctica) o E (Evaluativa)</param>
        /// <returns></returns>
        public DataTable porcentajeLeccionesCompletadasXestudiante(int _idEstudiante, int _idModulo, string _tipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_porcentajeLeccionesCompletadasXestudiante");
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@tipo", SqlDbType.Char).Value = _tipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public DataTable numLeccionesXcompletar(int _idEstudiante, int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_NumeroDeLeccionesXcompletar");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta los 10 últimos ciclos</para>
        /// </summary>
        /// <returns>Lista de con 10 registros de los ciclos</returns>
        public DataTable consultarCiclos()
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarCiclos");
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta los niveles dependiendo del tipo de módulo</para>
        /// </summary>
        /// <param name="_ciclo">Numero del ciclo del que se consultan los niveles</param>
        /// <param name="_idTipoModulo">Identificador del tipo de odulo del ciclo/param>
        /// <returns>Lista de niveles del ciclo especificado</returns>
        public DataTable consultarNivelesXtipoModulo(int _ciclo, int _idTipoModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_getNivelesConTipo");
            varComando.Parameters.Add("@ciclo", SqlDbType.Int).Value = _ciclo;
            varComando.Parameters.Add("@idTipoModulo", SqlDbType.Int).Value = _idTipoModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }
        /// <summary>
        /// <para>Consulta los tipos de módulo existentes dentro de un ciclo</para>
        /// </summary>
        /// <param name="_ciclo">Numero del ciclo del que se consultan los tipos de módulo</param>
        /// <returns>Lista de los tipos de módulo</returns>
        public DataTable consultarTipoModulo(int _ciclo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_getTiposModulos");
            varComando.Parameters.Add("@ciclo", SqlDbType.Int).Value = _ciclo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta los paralelos de un nivel específico</para>
        /// </summary>
        /// <param name="_codigo">código del módulo</param>
        /// <param name="_ciclo">número del ciclo</param>
        /// <param name="_tipoModulo">identificador dell tipo de módulo</param>
        /// <param name="_nivel">Equivalente del nivel</param>
        /// <returns>Lista de string con los paralelos</returns>
        public DataTable consultarParalelos(string _codigo, int _ciclo, int _tipoModulo, string _nivel )
        {
            varComando = conexionServidor.crearComandoProcedi("sp_getParalelosXnivel");
            varComando.Parameters.Add("@codigo", SqlDbType.Char).Value = _codigo;
            varComando.Parameters.Add("@ciclo", SqlDbType.Int).Value = _ciclo;
            varComando.Parameters.Add("@idTipoModulo", SqlDbType.Int).Value = _tipoModulo;
            varComando.Parameters.Add("@nivelEquivalente", SqlDbType.VarChar).Value = _nivel;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consultas las carreras registradas en el sistema</para>
        /// </summary>
        /// <returns>Lista de tipo CarreraEntidad de las carreras</returns>
        public DataTable consultarCarreras()
        {
            varComando = conexionServidor.crearComandoProcedi("sp_getCarreras");
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta la efectividad en las respuestas de un ciclo y nivel especificado</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public DataTable EfectividadXcicloYnivel(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EfectividadXcicloYnivel");
            varComando.Parameters.Add("@numCiclo", SqlDbType.Int).Value = _numCiclo;
            varComando.Parameters.Add("@equivalenteNivel", SqlDbType.Char).Value = _equivlenteNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta la efectividad en las respuestas de un ciclo, nivel y paralelo especificado</para>
        /// </summary>
        /// <param name="_idModulo">identificador del módulo</param>
        /// <param name="_idCiclo">identificador del ciclo</param>
        /// <param name="_idNivel">identificador del nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public DataTable EfectividadXcicloNivelYparalelo(int? _idModulo, int? _idCiclo, int? _idNivel, string _codigoModulo, string _equivalenteTipo, string _paralelo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EfectividadXparalelo");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@idCiclo", SqlDbType.Int).Value = _idCiclo;
            varComando.Parameters.Add("@idNivel", SqlDbType.Char).Value = _idNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            varComando.Parameters.Add("@paralelo", SqlDbType.Char).Value = _paralelo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>Consulta la efectividad de los estudiantes agrupadas por los paralelos de un nivel especificado</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>

        public DataTable EfectividadComparativaXparalelos(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EfectividadComparativaXparalelos");
            varComando.Parameters.Add("@numCiclo", SqlDbType.Int).Value = _numCiclo;
            varComando.Parameters.Add("@equivalenteNivel", SqlDbType.Char).Value = _equivlenteNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        /// <summary>
        /// <para>Consulta la efectividad en las respuestas de un ciclo, nivel y carrera especificado</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <param name="_carrera"></param>
        /// <returns></returns>
        public DataTable EfectividadXcicloNivelYcarrera(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo, int _idCarrera)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EfectividadXcicloNivelYcarrera");
            varComando.Parameters.Add("@numCiclo", SqlDbType.Int).Value = _numCiclo;
            varComando.Parameters.Add("@equivalenteNivel", SqlDbType.Char).Value = _equivlenteNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            varComando.Parameters.Add("@idCarrera", SqlDbType.Int).Value = _idCarrera;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el tiempo de trabajo autónomo realizado por los estudiantes de un nivel</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
         
        public DataTable TiempoTrabajoXcicloYnivel(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_TiempoTrabajoXcicloYnivel");
            varComando.Parameters.Add("@numCiclo", SqlDbType.Int).Value = _numCiclo;
            varComando.Parameters.Add("@equivalenteNivel", SqlDbType.Char).Value = _equivlenteNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el tiempo de trabajo autónomo realizado por los estudiantes de un paralelo</para>
        /// </summary>
        /// <param name="_idModulo">identificador del módulo</param>
        /// <param name="_idCiclo">identificador del ciclo</param>
        /// <param name="_idNivel">identificador del nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <param name="_paralelo">paralelo</param>
        /// <returns></returns>
        public DataTable TiempoTrabajoXparalelo(int? _idModulo, int? _idCiclo, int? _idNivel, string _codigoModulo, string _equivalenteTipo, string _paralelo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_TiempoTrabajoXparalelo");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@idCiclo", SqlDbType.Int).Value = _idCiclo;
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            varComando.Parameters.Add("@paralelo", SqlDbType.Char).Value = _paralelo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el tiempo de trabajo autónomo por carrera</para>
        /// </summary>
        /// <param name="_numCiclo">número del ciclo</param>
        /// <param name="_equivlenteNivel">equivalente del número de nivel</param>
        /// <param name="_codigoModulo">código del módulo</param>
        /// <param name="_equivalenteTipo">equivalente del tipo de curso</param>
        /// <returns></returns>
        public DataTable TiempoTrabajoXcarrera(int _numCiclo, string _equivlenteNivel, string _codigoModulo, string _equivalenteTipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_TiempoTrabajoXcicloNivelYcarrera");
            varComando.Parameters.Add("@numCiclo", SqlDbType.Int).Value = _numCiclo;
            varComando.Parameters.Add("@equivalenteNivel", SqlDbType.Char).Value = _equivlenteNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
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
        public DataTable ConsultaDocenteXmodulo(int _idCiclo, int _idNivel, string _codigoModulo, string _equivalenteTipo, string _paralelo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultaDocenteXmodulo");
            varComando.Parameters.Add("@idCiclo", SqlDbType.Int).Value = _idCiclo;
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            varComando.Parameters.Add("@codigoModulo", SqlDbType.Char).Value = _codigoModulo;
            varComando.Parameters.Add("@equivalenteTipo", SqlDbType.Char).Value = _equivalenteTipo;
            varComando.Parameters.Add("@paralelo", SqlDbType.Char).Value = _paralelo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta el codigo del modulo especificado</para>
        /// </summary>
        /// <param name="_idModulo">Identificador del modulo especificado</param>
        /// <returns></returns>
        public DataTable ConsultarCodigoModulo(int _idModulo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarCodigoModulo");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        
        /// <summary>
        /// <para>Consulta el numero de actividades asignadas a un modulo agrupadas por leccion</para>
        /// </summary>
        /// <param name="_idModulo">Identificador del modulo</param>
        /// <param name="_tipo">Tipo de actividad (P o E)</param>
        /// <returns></returns>

        public DataTable ConsultarActividadesAsignadasXleccion(int _idModulo, string _tipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarActividadesAsignadasXleccion");
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@tipo", SqlDbType.Char).Value = _tipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consulta las notificaciones de un usuario</para>
        /// </summary>
        /// <param name="_idPersona"></param>
        /// <param name="_fecha"></param>
        /// <returns></returns>
        public DataTable consultarNotificaciones(int _idPersona, DateTime _fecha)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ConsultarNotificacion");
            varComando.Parameters.Add("@idUsuarioDestino", SqlDbType.Int).Value = _idPersona;
            varComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = _fecha;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Consultar personas a notificar</para>
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <param name="_idPersona"></param>
        /// <param name="_opcion">1: Administradores, 2: Docente, 3: Estudiante</param>
        /// <returns></returns>
        public DataTable consultarPersonasANotificar(int _idActividad, int? _idPersona, int _opcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_consultarAministradores"); 
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = _idPersona;
            varComando.Parameters.Add("@op", SqlDbType.Int).Value = _opcion;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }
    }
}