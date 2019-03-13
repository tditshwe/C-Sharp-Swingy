using System.ComponentModel.DataAnnotations.Schema;

namespace Swingy.Models
{
    [Table ("Hero")]
    public class HeroEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HitPonts { get; set; }
    }
}
