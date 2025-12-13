using System;
using System.Collections.Generic; // IMPORTANTE: Necesario para usar List<>
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiJuegos_PNT1.Models
{
    public class Juego
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // --- VALIDACIONES NUEVAS ---

        [Required(ErrorMessage = "¡El nombre es obligatorio!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 letras.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "Falta el desarrollador.")]
        [StringLength(50, ErrorMessage = "El nombre del desarrollador es muy largo (máx 50).")]
        public required string Desarrollador { get; set; }

        [Required(ErrorMessage = "La descripción no puede estar vacía.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Escribe al menos 10 caracteres de descripción.")]
        public required string Descripcion { get; set; }

        [Display(Name = "Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime FechaLanzamiento { get; set; }

        [EnumDataType(typeof(Genero))]
        [Required(ErrorMessage = "Debes elegir un género.")]
        public Genero Genero { get; set; }

        // --- RELACIÓN NUEVA (El Foro) ---
        // Un juego tiene una lista de muchos comentarios
        public List<Comentario>? Comentarios { get; set; }
    }
}

