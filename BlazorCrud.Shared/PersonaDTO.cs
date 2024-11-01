using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Email { get; set; }
        public string? Foto { get; set; }
        public DateTime? EnteredDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
