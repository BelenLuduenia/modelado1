using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelado1.Comunes.datos.Entidades
{
    [Index(nameof(DiaCursado), Name = "UQ_Horaio_DiaCursado", IsUnique = true)]

    public class Horario
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "el dia de cursado es obligatorio.")]

        [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

        public string DiaCursado { get; set; }

        [Required(ErrorMessage = "el nombre del aula es obligatorio.")]

        [MaxLength(120, ErrorMessage = "el campo tiene como maximo {1} caracteres.")]

        public string Aula { get; set; }

        [Required(ErrorMessage = "el curso es obligatorio.")]

        public int CursoId { get; set; } // este Id me va a relacionar con el Id de horarios

        public Curso Curso { get; set; } //


    }
}
