using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.DTOs
{
    public class CreacionProductoDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50)]
        public string nombre { get; set; }
        public int valor { get; set; }
    }
}
