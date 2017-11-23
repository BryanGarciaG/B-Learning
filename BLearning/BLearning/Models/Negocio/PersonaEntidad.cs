using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    public class PersonaEntidad
    {
        public int idPersona { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string cadenaDirFotoPerfil { get; set; }
        public string rol { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string genero { get; set; }
        public string estado { get; set; }
        public string semestre { get; set; }
        public string carrera { get; set; }
        public int idAlumno { get; set; }
        public int idUsuario { get; set; }
        public Nivel Nivel { get; set; }
    }

    public enum Nivel
    {
        Alumno,
        Docente,
        Administradores
    }
}