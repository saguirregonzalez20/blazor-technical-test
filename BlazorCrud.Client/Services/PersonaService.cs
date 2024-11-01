using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly HttpClient _http;

        public PersonaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PersonaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<PersonaDTO>>>("api/Persona/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<PersonaDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<PersonaDTO>>($"api/Persona/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Editar(PersonaDTO persona)
        {
            var result = await _http.PutAsJsonAsync($"api/Persona/Editar/{persona.IdPersona}", persona);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Persona/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Guardar(PersonaDTO persona)
        {
            var result = await _http.PostAsJsonAsync("api/Persona/Guardar", persona);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        
    }
}
