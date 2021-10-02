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

    // PARA QUE HAYA VALIDACION PONEMOS
    [ApiController]
    //ruta
    [Route("api/Horarios")]
    public class HorariosController : ControllerBase // hereda de basecontroles
    {
        private readonly dbContext context; // readonly es de solo lectura

        public HorariosController(dbContext context)//constructor
        {
            this.context = context;
        }

        [HttpGet] // traer la lista de todo los objetos q hemos cargado
        public async Task <ActionResult<List<Horario>>> Get()
        {
            //select*from horarios.....sql
            return await context.Horarios.Include(X =>X.Curso).ToListAsync();
            //devuelve una lista de horarios
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Horario>> Get(int id)

        {
            //select*from cursos where Id=id.....sql
            var horario = await context.Horarios.Where(X => X.Id == id).Include(X => X.Curso).FirstOrDefaultAsync();



            if (horario == null)

            {
                return NotFound($"no existe el horario con id igual a { id}.");// error 404 no se encontro la pagina



            }

               return Ok (horario);
        }


        [HttpPost] // crea o inserta un gegistro nuevo en una tabla en una la base de datos 

        public async Task <ActionResult<Horario>> Post(Horario horario)
        {

            try
            {
                context.Horarios.Add(horario);// me adiciona 
               await context.SaveChangesAsync();
                return horario;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

       
        [HttpPut("{id:int}")] // modificar algun dato

        public async Task <ActionResult<Curso>> Put(int id, [FromBody] Horario horario)
        {
            if (id != horario.Id)
                
            {
                return BadRequest("datos incorrectos");
            }


            var pe = await context.Horarios.Where(X => X.Id == id).FirstOrDefaultAsync(); // hay que ver q exista el dato en la base de datos 
                                                                            

            if (pe == null)
            {
                return NotFound($"no existe el horario a modificar"); //si no existe le contesto con un notfound 404
            }

            pe.DiaCursado = horario.DiaCursado;
            pe.Aula = horario.Aula;
            pe.CursoId = horario.CursoId;

            try
            {
                context.Horarios.Update(pe); // update es actualizar un dato
               await context.SaveChangesAsync();
                return Ok("los datos han sido cambiados");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }



        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            var horario = await context.Horarios.Where(X => X.Id == id).FirstOrDefaultAsync ();


            if (horario == null)

            {
                return NotFound($"no existe el  horario con id igual a {id}.");
                 // error 404 no se encontro la pagina
            }

            try
            {
                context.Horarios.Remove(horario);
                    
                await context.SaveChangesAsync ();
                return Ok($"el horario{horario.DiaCursado} ha sido borrado");


            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }


        }

    }
}
