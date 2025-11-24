using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiJuegos_PNT1.Models
{
    public class Juego
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Nombre { get; set; }
        public required string Desarrollador { get; set; }

        [Display(Name = "Fecha de lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }

        [EnumDataType(typeof(Genero))]
        public Genero Genero { get; set; }
    }
}


