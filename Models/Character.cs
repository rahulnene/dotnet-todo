using System.ComponentModel.DataAnnotations;

namespace dotnet_todo.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [Length(2, 50)]
        public string Name { get; set; }

        [Range(0, 1000)]
        public int HitPoints { get; set; } = 100;

        [Range(0, 1000)]
        public int Strength { get; set; } = 10;

        [Range(0, 1000)]
        public int Defense { get; set; } = 10;

        [Range(0, 1000)]
        public int Intelligence { get; set; } = 10;

        public RPGClass Class { get; set; } = RPGClass.Knight;
    }
}