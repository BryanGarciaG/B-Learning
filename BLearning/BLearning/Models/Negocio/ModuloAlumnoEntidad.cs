using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLearning.Models.Negocio
{
    /// <summary>
    /// <para>Tipo de clase para consultar los datos del alumno y del módulo en que está metriculado</para>
    /// </summary>
    public class ModuloAlumnoEntidad
    {
        public int idAlumno {get; set;}
        public int idPersona {get; set;}
        public string nombres {get; set;}
        public string documento {get; set;}
        public string codigoModulo {get; set;}
        public string equivalente {get; set;}
        public int idModulo {get; set;}
        public int nuemeroNivel {get; set;}
        public string nivelEquivalente { get; set;}
        public int idCiclo {get; set;}
        public int idCodigo {get; set;}
        public int estado { get; set; }
       
    }
}