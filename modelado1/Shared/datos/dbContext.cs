using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelado1.Comunes.datos.Entidades;


namespace modelado1.Comunes.datos
{
    public class dbContext: DbContext// esta va a ser mi base de datos, dentro de db context voy a tener todas la partes
        //referidas a la base de datos en si
        // es la clase especializada en trabajar con la base de datos
        // es como una interfaz entre nuestro codigo y labase de datos, como un traductor
        //es el servidor de la base de datos
    {

        public DbSet<Curso> Cursos { get; set; } // cursos va a ser el nombre de mi tabla dentro de la base de datos
                                                 // curso va a ser la configuracion de esa tabla es decir los campos de esa tabla

        public DbSet<Horario> Horarios { get; set; }

        public dbContext(DbContextOptions<dbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            }

            base.OnModelCreating(modelBuilder);

        }



    }
}
