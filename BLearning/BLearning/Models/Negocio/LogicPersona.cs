using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BLearning.Models.Datos;

namespace BLearning.Models.Negocio
{

    public class LogicPersona
    {
        MetodosConsultar objConsultas = new MetodosConsultar();

        /// <summary>
        /// <para>consutar cuantas lecciones activas existen en un nivel</para>
        /// </summary>
        /// <param name="_idModulo"></param>
        /// <returns></returns>
        public List<PersonaEntidad> consultarEstudiantesXModulo(int _idModulo)
        {
            List<PersonaEntidad> listPersona = new List<PersonaEntidad>();
            foreach (DataRow item in objConsultas.consultarEstudiantesXModulo(_idModulo).Rows)
            {
                listPersona.Add(new PersonaEntidad
                    {
                        idAlumno = Convert.ToInt32(item.ItemArray[0]),
                        nombres = item.ItemArray[1].ToString()
                    });
            }
            return listPersona;
        }


        public PersonaEntidad loginDocente(string _usuario, string _clave, string _rol)
        {
            PersonaEntidad objDocente = new PersonaEntidad();
            foreach (DataRow item in objConsultas.loginUsuarios(_usuario, _clave, _rol).Rows)
            {
                objDocente.nombres = item.ItemArray[4].ToString();
                objDocente.apellidos = item.ItemArray[5].ToString();
                objDocente.idPersona = Convert.ToInt32(item.ItemArray[10]);
                objDocente.Nivel = Nivel.Docente;
            }
            return objDocente;
        }

        /// <summary>
        /// <para>Consuta los datos del módulo en que está matriculado el alumno y los datos del alumno</para>
        /// </summary>
        /// <param name="cedula">Número de documento del estudiante</param>
        /// <returns>Datos del módulo y del alumno</returns>
        public ModuloAlumnoEntidad consultarModuloDeAlumno(string cedula)
        {
            DataTable Modulo = objConsultas.consultaModuloDeAlumno(cedula);
            ModuloAlumnoEntidad objModuloAlumno = new ModuloAlumnoEntidad();
            foreach (DataRow item in Modulo.Rows)
            {
                objModuloAlumno.idAlumno = Convert.ToInt32(item.ItemArray[0]);
                objModuloAlumno.idPersona = Convert.ToInt32(item.ItemArray[1]);
                objModuloAlumno.nombres = item.ItemArray[2].ToString();
                objModuloAlumno.documento = item.ItemArray[3].ToString();
                objModuloAlumno.codigoModulo = item.ItemArray[4].ToString();
                objModuloAlumno.equivalente = item.ItemArray[5].ToString();
                objModuloAlumno.idModulo = Convert.ToInt32(item.ItemArray[6]);
                objModuloAlumno.nuemeroNivel = Convert.ToInt32(item.ItemArray[7]);
                objModuloAlumno.nivelEquivalente = item.ItemArray[8].ToString();
                objModuloAlumno.idCiclo = Convert.ToInt32(item.ItemArray[9]);
                objModuloAlumno.idCodigo = Convert.ToInt32(item.ItemArray[10]);
                objModuloAlumno.estado = Convert.ToInt32(item.ItemArray[11]);
            }
            return objModuloAlumno;
        }

        /// <summary>
        /// <para>Hace una consulta del usuario para acceder al sistema</para>
        /// </summary>
        /// <param name="_usuario">Número de cédula del usuario</param>
        /// <param name="_clave">Clave de acceso</param>
        /// <param name="_rol">El tipo de usuario con el que accede</param>
        /// <returns>Datos de la persona</returns>
        public PersonaEntidad loginAlumno(string _usuario)
        {
            PersonaEntidad objAlumno = new PersonaEntidad();
            foreach (DataRow item in objConsultas.loginEstudiante(_usuario).Rows)
            {
                objAlumno.idPersona = Convert.ToInt32(item.ItemArray[10].ToString());
                objAlumno.usuario = (item.ItemArray[0].ToString());
                objAlumno.clave = (item.ItemArray[1].ToString());
                objAlumno.cadenaDirFotoPerfil = (item.ItemArray[2].ToString());
                objAlumno.rol = (item.ItemArray[3].ToString());
                objAlumno.nombres = (item.ItemArray[4].ToString());
                objAlumno.apellidos = (item.ItemArray[5].ToString());
                objAlumno.genero = (item.ItemArray[6].ToString());
                objAlumno.estado = (item.ItemArray[7].ToString());
                objAlumno.semestre = (item.ItemArray[8].ToString());
                objAlumno.carrera = (item.ItemArray[9].ToString());
                objAlumno.idAlumno = Convert.ToInt32(item.ItemArray[11].ToString());
                objAlumno.idUsuario = Convert.ToInt32(item.ItemArray[12].ToString());
                objAlumno.Nivel = Nivel.Alumno;
            }
            return objAlumno;
        }


        //public PersonaEntidad loginAlumno(string _usuario, string _clave, string _rol)
        //{
        //    PersonaEntidad objAlumno = new PersonaEntidad();
        //    foreach (DataRow item in objConsultas.loginUsuarios(_usuario, _clave, _rol).Rows)
        //    {
        //        objAlumno.idPersona = Convert.ToInt32(item.ItemArray[10].ToString());
        //        objAlumno.usuario = (item.ItemArray[0].ToString());
        //        objAlumno.clave = (item.ItemArray[1].ToString());
        //        objAlumno.cadenaDirFotoPerfil = (item.ItemArray[2].ToString());
        //        objAlumno.rol = (item.ItemArray[3].ToString());
        //        objAlumno.nombres = (item.ItemArray[4].ToString());
        //        objAlumno.apellidos = (item.ItemArray[5].ToString());
        //        objAlumno.genero = (item.ItemArray[6].ToString());
        //        objAlumno.estado = (item.ItemArray[7].ToString());
        //        objAlumno.semestre = (item.ItemArray[8].ToString());
        //        objAlumno.carrera = (item.ItemArray[9].ToString());
        //        objAlumno.idAlumno = Convert.ToInt32(item.ItemArray[11].ToString());
        //        objAlumno.idUsuario = Convert.ToInt32(item.ItemArray[12].ToString());
        //        objAlumno.Nivel = Nivel.Alumno;
        //    }
        //    return objAlumno;
        //}

        /// <summary>
        /// <para>Hace una consulta del usuario para acceder al sistema</para>
        /// </summary>
        /// <param name="_usuario">Número de cédula del usuario</param>
        /// <param name="_clave">Clave de acceso</param>
        /// <param name="_rol">El tipo de usuario con el que accede</param>
        /// <returns>Datos de la persona</returns>
        public PersonaEntidad loginAdministrador(string _usuario, string _clave, string _rol)
        {
            PersonaEntidad objPersona = new PersonaEntidad();
            foreach (DataRow item in objConsultas.loginUsuarios(_usuario, _clave, _rol).Rows)
            {
                objPersona.usuario = (item.ItemArray[0].ToString());
                objPersona.clave = (item.ItemArray[1].ToString());
                objPersona.cadenaDirFotoPerfil = (item.ItemArray[2].ToString());
                objPersona.rol = (item.ItemArray[3].ToString());
                objPersona.nombres = (item.ItemArray[4].ToString());
                objPersona.apellidos = (item.ItemArray[5].ToString());
                objPersona.genero = (item.ItemArray[6].ToString());
                objPersona.estado = (item.ItemArray[7].ToString());
                objPersona.idPersona = Convert.ToInt32(item.ItemArray[8].ToString());
                objPersona.idUsuario = Convert.ToInt32(item.ItemArray[10].ToString());
                objPersona.Nivel = Nivel.Administradores;
            }
            return objPersona;
        }


        
    }
}