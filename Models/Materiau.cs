using SQLite;

namespace DevisConstructionApp.Models
{
    [Table("Materiaux")]
    public class Materiau
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }

        [NotNull]
        public string Unite { get; set; }

        [NotNull]
        public decimal PrixUnitaire { get; set; }

        public string Description { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;
    }
}