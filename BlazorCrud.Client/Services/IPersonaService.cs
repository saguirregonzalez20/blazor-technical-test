using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IPersonaService
    {
        Task<List<PersonaDTO>> Lista();
        Task<PersonaDTO> Buscar(int id);
        Task<int> Guardar(PersonaDTO empleado);
        Task<int> Editar(PersonaDTO empleado);
        Task<bool> Eliminar(int id);
    }
}
