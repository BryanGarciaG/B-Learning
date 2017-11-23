using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace BLearning.Models.Datos
{
    public class MetodosIngreso
    {

        Conexion conexionServidor = new Conexion();
        SqlCommand varComando = new SqlCommand();

        /// <summary>
        /// <para>actualiza la imagen de una opcion y elimina la imagen de una opcion</para>
        /// </summary>
        /// <param name="_opcion">1 para actualizar. 2 para eliminar</param>
        /// <param name="_idOpcion">id de la opcion de la pregunta</param>
        /// <param name="_detalleOpcion">Sera el nombre con el que se guardo la imagen en el servidor</param>
        /// <param name="_detalleRespuesta">el significado/texto de la imagen</param>
        public void opcionesImgEditar(int _opcion, int _idOpcion, string _detalleOpcion, string _detalleRespuesta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_OpcionImgEditar");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idOpcion", SqlDbType.Int).Value = _idOpcion;
            varComando.Parameters.Add("@detallepregunta", SqlDbType.NVarChar).Value = _detalleOpcion;
            varComando.Parameters.Add("@detalleRespuesta", SqlDbType.NVarChar).Value = _detalleRespuesta;
            conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>verificar que una actividad reportada ha sido revisada y ha sido eliminada o no</para>
        /// </summary>
        /// <param name="opcion">1 si se desea eliminar la actividad. 2 para ingresar que una actividad ha sido revisada</param>
        /// <param name="idActi">id de la actividad a trabajar</param>
        public void actividadRevisada(int opcion,int idActi)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadYaRevisada");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = opcion;
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = idActi;
            conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>reportar una actividad para su revision</para>
        /// </summary>
        /// <param name="opcion">1 para ingresar un nuevo reporte. 2 para actualizar el estado de un reporte</param>
        /// <param name="_idActiRepor">id de ActiReporte a actualizar</param>
        /// <param name="_idActi">id de la actividad a reportar</param>
        /// <param name="_idDocente">id del docente que la reporta</param>
        /// <param name="_fecha">fecha del reporte</param>
        /// <param name="revisada">false si opcion es 1. true si opcion es 2</param>
        public void reportarActividad(int opcion, int _idActiRepor, int _idActi, int _idDocente, DateTime _fecha, bool revisada)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_NuevaActiRepor");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = opcion;
            varComando.Parameters.Add("@idActiRepor", SqlDbType.Int).Value = _idActiRepor;
            varComando.Parameters.Add("@idActi", SqlDbType.Int).Value = _idActi;
            varComando.Parameters.Add("@idDocente", SqlDbType.Int).Value = _idDocente;
            varComando.Parameters.Add("@fecha", SqlDbType.Date).Value = _fecha;
            varComando.Parameters.Add("@revisada", SqlDbType.Bit).Value = revisada;
            conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>actualiza el estado de una leccion</para>
        /// </summary>
        /// <param name="_idleccion"></param>
        /// <param name="_estado"></param>
        public void actualizarEstadoLeccion(int _idleccion, bool _estado)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_LeccionesDisponiblesActualizar");
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idleccion;
            varComando.Parameters.Add("@estado", SqlDbType.Bit).Value = _estado;
            conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>cud de leccion</para>
        /// </summary>
        /// <param name="_opcion">1 para eliminar, 2 para actualizar, 3 para ingresar</param>
        /// <param name="_idLeccion"></param>
        /// <param name="numLeccion"></param>
        /// <param name="_idNivel"></param>
        /// <param name="_estado"></param>
        /// <returns></returns>
        public DataTable leccionCud(int _opcion, int _idLeccion, string numLeccion, int _idNivel, bool _estado)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_LeccionCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@numLeccion", SqlDbType.NVarChar).Value = numLeccion;
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;
            varComando.Parameters.Add("@estado", SqlDbType.Bit).Value = _estado;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Eliminar el apoyo asignado a una pregunta</para>
        /// </summary>
        /// <param name="_idPregunta">pregunta en la que se elimina el apoyo</param>
        public void eliminarApoyoPregunta(int _idPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EliminarApoyoActividad");
            varComando.Parameters.Add("@idPregunta", SqlDbType.Int).Value = _idPregunta;
            conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>Eiminar un apoyo</para>
        /// </summary>
        /// <param name="_idApoyo">apoyo a eliminar</param>
        public void apoyoEliminar(int _idApoyo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ApoyoEliminar");
            varComando.Parameters.Add("@idApoyo", SqlDbType.Int).Value = _idApoyo;
            conexionServidor.ejecutarComando(varComando);
        }


        public DataTable actividadModuloCud(int _opcion, int _idActModulo,int _idModulo, int _idActividad,
            DateTime _fechaIni,DateTime _fechaFin, int _idLeccion, string _tipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadModuloCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActModulo;
            varComando.Parameters.Add("@idModulo", SqlDbType.Int).Value = _idModulo;
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@fechaInicio", SqlDbType.Date).Value = _fechaIni;
            varComando.Parameters.Add("@fechaFin", SqlDbType.Date).Value = _fechaFin;
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@tipo", SqlDbType.Char).Value = _tipo;
            return conexionServidor.ejecutarComandoConsultar(varComando);

        }

        //tabla actividad
        public DataTable actividadCud(int _opcion, int _idactivi, int _npreguntas, int _duracion, string _descripcion,int _idNivel)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idactiv", SqlDbType.Int).Value = _idactivi;
            varComando.Parameters.Add("@npreguntas", SqlDbType.Int).Value = _npreguntas;
            varComando.Parameters.Add("@duracion", SqlDbType.Int).Value = _duracion;
            varComando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = _descripcion;
            varComando.Parameters.Add("@idNivel", SqlDbType.Int).Value = _idNivel;

            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Actualizar los nPreguntas y duracion de la tabla actividad. nPreguntas no se envia</para>
        /// </summary>
        /// <param name="_opcion"></param>
        /// <param name="_idactivi"></param>
        /// <param name="_duracion"></param>
        public void actividadUpdate(int _opcion, int _idactivi, int _duracion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActivdadUpdate");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idactivi;
            varComando.Parameters.Add("@npreguntas", SqlDbType.Int).Value = 1;
            varComando.Parameters.Add("@duracion", SqlDbType.Int).Value = _duracion;

            conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Eliminar una actividad, sus preguntas, opciones y respuestas</para>
        /// </summary>
        /// <param name="_idactividad">Actividad a eliminar</param>
        public void actividadEliminar(int _idactividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActividadEliminar");
            varComando.Parameters.Add("@idActivdad", SqlDbType.Int).Value = _idactividad;
            conexionServidor.ejecutarComandoConsultar(varComando); 
        }

        //Pregunta
        /// <summary>
        /// <para>Elminar una pregunta, sus opciones y respuestas</para>
        /// </summary>
        /// <param name="_idPregunta"></param>
        public void preguntaEliminarTodo(int _idPregunta)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_PreguntaEliminarTodo");
            varComando.Parameters.Add("@idPregunta", SqlDbType.Int).Value = _idPregunta;
            conexionServidor.ejecutarComandoConsultar(varComando);
        }

        //tabla apoyo
        public DataTable apoyoCud(int _opcion, int? _idapoyo, string _enunciado, string _link, string _imagen, string _titulo, string _audio)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ApoyoCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idapoyo", SqlDbType.Int).Value = _idapoyo;
            varComando.Parameters.Add("@titulo", SqlDbType.VarChar).Value = _titulo;
            varComando.Parameters.Add("@enunciado", SqlDbType.VarChar).Value = _enunciado;
            varComando.Parameters.Add("@link", SqlDbType.VarChar).Value = _link;
            varComando.Parameters.Add("@imagen", SqlDbType.VarChar).Value = _imagen;
            varComando.Parameters.Add("@audio", SqlDbType.VarChar).Value = _audio;

            return conexionServidor.ejecutarComandoConsultar(varComando);
        }


        //tabla opciones
        public DataTable opcionesCud(int _opcion, int? _idopciones, string _detallepre, int _idpregun)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_OpcionesCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idopciones", SqlDbType.Int).Value = _idopciones;
            varComando.Parameters.Add("@detallepregunta", SqlDbType.VarChar).Value = _detallepre;
            varComando.Parameters.Add("@idpregunta", SqlDbType.Int).Value = _idpregun;

            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        //tabla pregunta
        public DataTable preguntaCud(int _opcion, int? _idpregunta, string _indicaciones, decimal _ponderacion, int _idactividad,
            string _destreza, int _idtipo, int? _idapoyo, int _duracion, bool _mostrarOp)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_PreguntaCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idpregunta", SqlDbType.Int).Value = _idpregunta;
            varComando.Parameters.Add("@indicaciones", SqlDbType.VarChar).Value = _indicaciones;
            varComando.Parameters.Add("@ponderacion", SqlDbType.Decimal).Value = _ponderacion;
            varComando.Parameters.Add("@idactividad", SqlDbType.Int).Value = _idactividad;
            varComando.Parameters.Add("@destreza", SqlDbType.Char).Value = _destreza;
            varComando.Parameters.Add("@idtipo", SqlDbType.Int).Value = _idtipo;
            varComando.Parameters.Add("@idapoyo", SqlDbType.Int).Value = _idapoyo;
            varComando.Parameters.Add("@duracion", SqlDbType.Int).Value = _duracion;
            varComando.Parameters.Add("@mostrarOpc", SqlDbType.Bit).Value = _mostrarOp;

            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        //tabla respuesta
        public int respuestaCud(int _opcion, int? _idrespuesta, int _idopciones, string _detallerespu)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_RespuestaCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idrespuesta", SqlDbType.Int).Value = _idrespuesta;
            varComando.Parameters.Add("@idopciones", SqlDbType.Int).Value = _idopciones;
            varComando.Parameters.Add("@detallerespuesta", SqlDbType.VarChar).Value = _detallerespu;

            return conexionServidor.ejecutarComando(varComando);
        }

        public int activDocenteCud(int _opcion,int? _idActDocente, int _idDocente, int _idActi, DateTime _fecha)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActivDocenteCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idActDocente", SqlDbType.Int).Value = _idActDocente;
            varComando.Parameters.Add("@idDocente", SqlDbType.Int).Value = _idDocente;
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActi;
            varComando.Parameters.Add("@fecha", SqlDbType.Date).Value = _fecha;
            return conexionServidor.ejecutarComando(varComando);
        }

        //tabla tipo pregunta
        public int tipoPreguntaCud(int _opcion, int _idtipo, string _tipo)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_TipoPreguntaCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idtipo", SqlDbType.Int).Value = _idtipo;
            varComando.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = _tipo;

            return conexionServidor.ejecutarComando(varComando);

        }

        public DataTable leccionCud(int _opcion, int _idLeccion, string _numLeccion, int _idNivel)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_LeccionCud");
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            varComando.Parameters.Add("@idLeccion", SqlDbType.Int).Value = _idLeccion;
            varComando.Parameters.Add("@numLeccion", SqlDbType.NVarChar).Value = _numLeccion;
            varComando.Parameters.Add("@idnivel", SqlDbType.Int).Value = _idNivel;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        


        
        /// <summary>
        /// <para>Ingresa, actualiza o elimima un registro de un quiz</para>
        /// </summary>
        /// <param name="_idQuiz"></param>
        /// <param name="_idActividad"></param>
        /// <param name="_idEstudiante"></param>
        /// <param name="_fecha"></param>
        /// <param name="_completado"></param>
        /// <param name="_indicePregunta"></param>
        /// <param name="_duracion"></param>
        /// <param name="_nVecesResuelto"></param>
        /// <param name="_opcion"></param>
        /// <returns></returns>
        public DataTable ingresarQuiz(int _idQuiz, int _idActividad, int _idEstudiante, DateTime _fecha, bool _completado, int _indicePregunta, int _duracion, int? _nVecesResuelto, int _opcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_QuizCud");
            varComando.Parameters.Add("@idQuiz", SqlDbType.Int).Value = _idQuiz;
            varComando.Parameters.Add("@idActModulo", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idEstudiante", SqlDbType.Int).Value = _idEstudiante;
            varComando.Parameters.Add("@fecha", SqlDbType.Date).Value = _fecha;
            varComando.Parameters.Add("@completado", SqlDbType.Bit).Value = _completado;
            varComando.Parameters.Add("@indicePregunta", SqlDbType.Int).Value = _indicePregunta;
            varComando.Parameters.Add("@duracion", SqlDbType.Int).Value = _duracion;
            varComando.Parameters.Add("@nVecesResuelto", SqlDbType.Int).Value = _nVecesResuelto;
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        public int ingresarDetalleQuiz(int _idDetalleQuiz, int _idOpciones, string _detalleRespuesta, int _idQuiz, int _opcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_DetalleQuizCud");
            varComando.Parameters.Add("@idDetalleQuiz", SqlDbType.Int).Value = _idDetalleQuiz;
            varComando.Parameters.Add("@idOpciones", SqlDbType.Int).Value = _idOpciones;
            varComando.Parameters.Add("@detalleRespuesta", SqlDbType.VarChar).Value = _detalleRespuesta;
            varComando.Parameters.Add("@idQuiz", SqlDbType.Int).Value = _idQuiz;
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            return conexionServidor.ejecutarComando(varComando);
        }

        //Ingreso de la calificacion de la efectividad en las respuestas
        public int ingresarDetalleCalificacion(int _idDetalleCalificacion, decimal _calificacion, string _destreza, int _idEfectividad, int _opcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_DetalleCalificacionCud");
            varComando.Parameters.Add("@idDetalleCalificacion", SqlDbType.Int).Value = _idDetalleCalificacion;
            varComando.Parameters.Add("@calificacion", SqlDbType.Decimal).Value = _calificacion;
            varComando.Parameters.Add("@destreza", SqlDbType.Char).Value = _destreza;
            varComando.Parameters.Add("@idCalificacion", SqlDbType.Int).Value = _idEfectividad;
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            return conexionServidor.ejecutarComando(varComando);
        }

        public DataTable ingresarCalificacion(int _idCalificacion, int _idQuiz, int _opcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_CalificacionCud");
            varComando.Parameters.Add("@idCalificacion", SqlDbType.Int).Value = _idCalificacion;
            varComando.Parameters.Add("@idQuiz", SqlDbType.Int).Value = _idQuiz;
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Ingresa un resgistro de acceso al sistema</para>
        /// </summary>
        /// <param name="_idAcceso"></param>
        /// <param name="_fechaEntrada"></param>
        /// <param name="_idPersona"></param>
        /// <param name="_tipo">Tipo de acceso</param>
        /// <param name="_opcion"></param>
        /// <returns></returns>
        public DataTable ingresarAcceso(int _idAcceso, DateTime _fechaEntrada, string _tipo, int _idPersona, int _opcion)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_AccesoCud");
            varComando.Parameters.Add("@idAcceso", SqlDbType.Int).Value = _idAcceso;
            varComando.Parameters.Add("@fechaEntrada", SqlDbType.DateTime).Value = _fechaEntrada;
            varComando.Parameters.Add("@tipo", SqlDbType.Char).Value = _tipo;
            varComando.Parameters.Add("@idPersona", SqlDbType.Int).Value = _idPersona;
            varComando.Parameters.Add("@opcion", SqlDbType.Int).Value = _opcion;
            return conexionServidor.ejecutarComandoConsultar(varComando);
        }

        /// <summary>
        /// <para>Ingresa una notificación</para>
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <param name="_idPersona"></param>
        /// <param name="_idPersonaDestino"></param>
        /// <param name="_fecha"></param>
        /// <param name="_asunto"></param>
        /// <param name="_estado"></param>
        /// <returns></returns>
        public int ingresarNotificacion(int _idActividad, int? _idPersona, int _idPersonaDestino,  DateTime _fecha,  string _asunto, string _estado)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_IngresarNotificacion");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            varComando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = _idPersona;
            varComando.Parameters.Add("@idUsuarioDestino", SqlDbType.Int).Value = _idPersonaDestino;
            varComando.Parameters.Add("@fecha", SqlDbType.DateTime).Value = _fecha;
            varComando.Parameters.Add("@asunto", SqlDbType.VarChar).Value = _asunto;
            varComando.Parameters.Add("@estado", SqlDbType.Char).Value = _estado;
            return conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>Modifica el estado de una notificación</para>
        /// </summary>
        /// <param name="_idNotificacion"></param>
        /// <param name="_estado"></param>
        /// <returns></returns>
        public int actualizarNotificacion(int _idNotificacion, string _estado)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_ActualizarNotificacion");
            varComando.Parameters.Add("@idNotificaacion", SqlDbType.Int).Value = _idNotificacion;
            varComando.Parameters.Add("@estado", SqlDbType.Char).Value = _estado;
            return conexionServidor.ejecutarComando(varComando);
        }

        /// <summary>
        /// <para>Elimina una notificación</para>
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <returns></returns>
        public int eliminarNotificacion(int _idActividad)
        {
            varComando = conexionServidor.crearComandoProcedi("sp_EliminarNotificacion");
            varComando.Parameters.Add("@idActividad", SqlDbType.Int).Value = _idActividad;
            return conexionServidor.ejecutarComando(varComando);
        }




    }
}