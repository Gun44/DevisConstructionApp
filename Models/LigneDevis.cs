using SQLite;

namespace DevisConstructionApp.Models
{
    [Table("LignesDevis")]
    public class LigneDevis
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int DevisId { get; set; }

        [NotNull]
        public int MateriaId { get; set; }

        public string NomMateriau { get; set; }

        public decimal QuantiteNecessaire { get; set; }

        public string Unite { get; set; }

        public decimal PrixUnitaire { get; set; }

        public decimal MontantLigne => QuantiteNecessaire * PrixUnitaire;
    }
}