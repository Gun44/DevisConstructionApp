using SQLite;

namespace DevisConstructionApp.Models
{
    [Table("Projets")]
    public class Projet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }

        public string Description { get; set; }

        public string ImagePlanPath { get; set; }

        public decimal LongueurM { get; set; }

        public decimal LargeurM { get; set; }

        public decimal HauteurM { get; set; }

        public decimal SurfaceM2 => LongueurM * LargeurM;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public DateTime DateModification { get; set; } = DateTime.Now;
    }
}