using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domain.DTOs
{
    public class UserDTO
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public int Ciudad { get; set; }
    }
}
