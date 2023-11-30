using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnet_todo.Models
{
    public class Character: IActor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Length(2, 50)]
        public string? Name { get; set; }

        [Range(0, 1000)]
        public int HitPoints { get; set; } = 100;

        [Range(0, 1000)]
        public int Strength { get; set; } = 10;

        [Range(0, 1000)]
        public int Defense { get; set; } = 10;

        [Range(0, 1000)]
        public int Intelligence { get; set; } = 10;

        [Required]
        [Length(2, 50)]
        [RegularExpression(@"^[A-Z][a-z]+$")]
        public string Class { get; set; } = "Knight";
    }
}