using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modelado1.Comunes.datos;
using modelado1.Comunes.datos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace modelado1.Server.Controllers
{

    [ApiController]
    [Route("api/Cursos")]
    public class CursosController: ControllerBase //es una clase que se encuentra en Mvc
    {

        private readonly dbContext context;//DbContex manejador de la base de datos


        public CursosController(dbContext context)//constructor
        {
            this.context = context;
        }


        [HttpGet] // traer la lista de todo los objetos q hemos cargado
        public async Task <ActionResult<List<Curso>>> Get()
        {
            //select*from cursos.....sql
            return await context.Cursos.Include(x => x.Horarios).ToListAsync();
            //devuelve una lista de cursos
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Curso>> Get(int id)
        {
            //select*from cursos where Id=id.....sql
            var curso = await context.Cursos.Where(X => X.Id == id.ToString()).FirstOrDefaultAsync();

            if (curso == null)
            {
                return NotFound($"no existe el curso con id igual a {id}."); // error 404 no se encontro la pagina
            }

            return curso;
        }

        [HttpPost] // crea o inserta un gegistro nuevo en una tabla en una la base de datos 

        public async Task< ActionResult<Curso>> Post(Curso curso)
        {
           


            try
            {
                context.Cursos.Add(curso);// me adiciona 
                await context.SaveChangesAsync(); // este metodo se comunica con la base de datos
                return curso;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            
        }

        


        [HttpPut("{id:int}")] // modificar algun dato

        public async Task<ActionResult<Curso>> Put(int id,[FromBody]Curso curso)
        {
            if (id.ToString() != curso.Id)
            {
                return BadRequest("datos incorrectos");
            }


            var pe = await context.Cursos.Where(X => X.Id == id.ToString()).FirstOrDefaultAsync(); // hay que ver q exista el dato en la base de datos 
             //si no existe le contesto con un notfound 404

            if (pe == null)
            {
               return NotFound($"no existe el curso a modificar."); 
            }

            pe.NombreCurso = curso.NombreCurso;
            pe.TutorCurso = curso.TutorCurso;
            pe.Resumen = curso.Resumen;

            try
            {
                context.Cursos.Update(pe); // update es actualizar un dato
                await context.SaveChangesAsync();
                return Ok("los datos han sido cambiados");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }



        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) // metodo borrar
        {
            
            var curso = await context.Cursos.Where(X => X.Id == id.ToString()).FirstOrDefaultAsync();

            if (curso == null)
            {
                return NotFound($"no existe el curso con id igual a {id}."); // error 404 no se encontro la pagina
            }

            try
            {
                context.Cursos.Remove(curso);
                await context.SaveChangesAsync ();
                return Ok($"el curso {curso.NombreCurso} ha sido borrado");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
           
            
        }
    }
}
