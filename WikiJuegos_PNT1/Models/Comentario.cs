using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WikiJuegos_PNT1.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "No puedes dejar el comentario vacío.")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres, sé breve.")]
        public string Texto { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "La puntuación es del 1 al 5.")]
        public int Puntaje { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        // Guardamos el nombre de quien comentó para mostrarlo en el foro
        [ForeignKey("UsuarioId")]
        public int? UsuarioId { get; set; }

        // RELACIÓN: Un comentario pertenece a UN juego
        [ForeignKey("JuegoId")]
        public int JuegoId { get; set; }

        public Juego? Juego { get; set; }
    }
} 
