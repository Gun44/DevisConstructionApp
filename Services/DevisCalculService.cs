using DevisConstructionApp.Models;

namespace DevisConstructionApp.Services
{
    public class DevisCalculService
    {
        public decimal CalculerCiment(decimal surfaceM2)
        {
            return surfaceM2 * 30;
        }

        public decimal CalculerSable(decimal surfaceM2)
        {
            return surfaceM2 * 0.05m;
        }

        public decimal CalculerGravier(decimal surfaceM2)
        {
            return surfaceM2 * 0.05m;
        }

        public decimal CalculerFer(decimal surfaceM2)
        {
            return surfaceM2 * 2;
        }

        public decimal CalculerFilsElectriques(decimal longueurM, decimal largeurM)
        {
            decimal perimetre = 2 * (longueurM + largeurM);
            return perimetre * 2;
        }

        public decimal CalculerCarreaux(decimal surfaceM2)
        {
            return surfaceM2 * 1.10m;
        }

        public decimal CalculerPeinture(decimal surfaceM2, decimal hauteurM)
        {
            decimal surfaceParois = 2 * (surfaceM2 / hauteurM) * hauteurM;
            return surfaceParois / 10;
        }

        public decimal CalculerMainOeuvre(decimal surfaceM2, decimal heuresParM2 = 2)
        {
            return surfaceM2 * heuresParM2;
        }

        public decimal CalculerTotalAvecTVA(decimal montantHT, decimal tauxTVA)
        {
            return montantHT * (1 + (tauxTVA / 100));
        }
    }
}