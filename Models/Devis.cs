using SQLite;

namespace DevisConstructionApp.Models
{
    [Table("Devis")]
    public class Devis
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int ProjetId { get; set; }

        public string NumeroDevis { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public decimal MontantTotal { get; set; }

        public decimal CoutMainOeuvre { get; set; }

        public decimal CoutMateriau { get; set; }

        public decimal TauxTVA { get; set; } = 20;

        public string ClientNom { get; set; }

        public string ClientTelephone { get; set; }

        public string ClientEmail { get; set; }

        public string Statut { get; set; } = "Brouillon";
    }
}