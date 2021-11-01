using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelado1.Comunes.datos.Entidades

{

    [Index(nameof(NombreCurso), Name = "UQ_Curso_NombreCurso", IsUnique = true)] //clave unica

    public  class Curso // esta es una clase que va a representar un registro en nuetra base de datos, esnuestra entidad y que tiene las siguientes propiedades
    {
        // curso va a tener las siguientes campos o propiedades

        


        public int Id { get; set; }
        //---------------------------------------------------

        // aca le digo que el nombre del curso no puede ser nulo 
        [Required(ErrorMessage = "el nombre del curso es obligatorio.")]

        [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

        public  string NombreCurso { get; set; }
        //--------------------------------------------
        [Required(ErrorMessage = "el nombre del tutor es obligatorio.")]
        [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]
        public string TutorCurso { get; set; }
        //--------------------------------
        [Required(ErrorMessage = "el resumen es obligatorio.")]
        [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]
        public string Resumen { get; set; }


        public List<Horario>Horarios { get; set; }


    }



}
