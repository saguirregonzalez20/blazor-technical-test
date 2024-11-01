using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbContext;

        public PersonaController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<PersonaDTO>>();
            var listaPersonaDTO = new List<PersonaDTO>();

            try
            {
                foreach (var item in await _dbContext.Personas.ToListAsync())
                {
                    listaPersonaDTO.Add(new PersonaDTO
                    {
                        IdPersona = item.IdPersona,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Email = item.Email,
                        Foto = item.Foto,
                        EnteredDate = item.EnteredDate,
                        UpdateDate = item.UpdateDate
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaPersonaDTO;
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<PersonaDTO>();
            var PersonaDTO = new PersonaDTO();

            try
            {
                var dbPersona = await _dbContext.Personas.FirstOrDefaultAsync(x => x.IdPersona == id);

                if (dbPersona != null)
                {
                    PersonaDTO.IdPersona = dbPersona.IdPersona;
                    PersonaDTO.Nombres = dbPersona.Nombres;
                    PersonaDTO.Apellidos = dbPersona.Apellidos;
                    PersonaDTO.Email = dbPersona.Email;
                    PersonaDTO.Foto = dbPersona.Foto;
                    PersonaDTO.EnteredDate = dbPersona.EnteredDate;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = PersonaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(PersonaDTO persona)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPersona = new Persona
                {
                    Nombres = persona.Nombres,
                    Apellidos = persona.Apellidos,
                    Email = persona.Email,
                    EnteredDate = DateTime.Now
                };

                _dbContext.Personas.Add(dbPersona);
                await _dbContext.SaveChangesAsync();

                if (dbPersona.IdPersona != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPersona.IdPersona;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(PersonaDTO persona, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPersona = await _dbContext.Personas.FirstOrDefaultAsync(e => e.IdPersona == id);

                if (dbPersona != null)
                {
                    dbPersona.Nombres = persona.Nombres;
                    dbPersona.Apellidos = persona.Apellidos;
                    dbPersona.Email = persona.Email;
                    dbPersona.UpdateDate = DateTime.Now;

                    _dbContext.Personas.Update(dbPersona);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPersona.IdPersona;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPersona = await _dbContext.Personas.FirstOrDefaultAsync(e => e.IdPersona == id);

                if (dbPersona != null)
                {
                    _dbContext.Personas.Remove(dbPersona);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }





    }
}
