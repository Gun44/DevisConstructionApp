using SQLite;
using DevisConstructionApp.Models;

namespace DevisConstructionApp.Services
{
    public class DatabaseService
    {
        private const string DatabaseFilename = "devis_construction.db3";
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        private static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
        }

        private async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(DatabasePath, Flags);
            await _database.CreateTableAsync<Materiau>();
            await _database.CreateTableAsync<Projet>();
            await _database.CreateTableAsync<Devis>();
            await _database.CreateTableAsync<LigneDevis>();

            await InitialiseMateriauxDefaut();
        }

        private async Task InitialiseMateriauxDefaut()
        {
            var count = await _database.Table<Materiau>().CountAsync();
            if (count == 0)
            {
                var materiaux = new List<Materiau>
                {
                    new Materiau { Nom = "Ciment", Unite = "kg", PrixUnitaire = 0.15m, Description = "Ciment gris Portland" },
                    new Materiau { Nom = "Sable", Unite = "m3", PrixUnitaire = 45.00m, Description = "Sable fin de construction" },
                    new Materiau { Nom = "Gravier", Unite = "m3", PrixUnitaire = 40.00m, Description = "Gravier 8/15" },
                    new Materiau { Nom = "Fer", Unite = "kg", PrixUnitaire = 0.80m, Description = "Acier HA 500" },
                    new Materiau { Nom = "Fils électriques", Unite = "m", PrixUnitaire = 0.50m, Description = "Câble électrique 2.5mm2" },
                    new Materiau { Nom = "Carreaux", Unite = "m2", PrixUnitaire = 25.00m, Description = "Carrelage 30x30cm" },
                    new Materiau { Nom = "Peinture", Unite = "litre", PrixUnitaire = 8.00m, Description = "Peinture mate intérieur" },
                    new Materiau { Nom = "Main d'œuvre", Unite = "heure", PrixUnitaire = 15.00m, Description = "Coût horaire ouvrier" }
                };

                await _database.InsertAllAsync(materiaux);
            }
        }

        public async Task<List<Materiau>> GetMateriaux()
        {
            await Init();
            return await _database.Table<Materiau>().ToListAsync();
        }

        public async Task<Materiau> GetMateriau(int id)
        {
            await Init();
            return await _database.Table<Materiau>().Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveMateriau(Materiau materiau)
        {
            await Init();
            if (materiau.Id != 0)
                return await _database.UpdateAsync(materiau);
            else
                return await _database.InsertAsync(materiau);
        }

        public async Task<int> DeleteMateriau(int id)
        {
            await Init();
            return await _database.DeleteAsync<Materiau>(id);
        }

        public async Task<List<Projet>> GetProjets()
        {
            await Init();
            return await _database.Table<Projet>().ToListAsync();
        }

        public async Task<Projet> GetProjet(int id)
        {
            await Init();
            return await _database.Table<Projet>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveProjet(Projet projet)
        {
            await Init();
            projet.DateModification = DateTime.Now;
            if (projet.Id != 0)
                return await _database.UpdateAsync(projet);
            else
                return await _database.InsertAsync(projet);
        }

        public async Task<int> DeleteProjet(int id)
        {
            await Init();
            return await _database.DeleteAsync<Projet>(id);
        }

        public async Task<List<Devis>> GetDevis()
        {
            await Init();
            return await _database.Table<Devis>().ToListAsync();
        }

        public async Task<Devis> GetDevis(int id)
        {
            await Init();
            return await _database.Table<Devis>().Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveDevis(Devis devis)
        {
            await Init();
            if (devis.Id != 0)
                return await _database.UpdateAsync(devis);
            else
                return await _database.InsertAsync(devis);
        }

        public async Task<int> DeleteDevis(int id)
        {
            await Init();
            return await _database.DeleteAsync<Devis>(id);
        }

        public async Task<List<LigneDevis>> GetLignesDevis(int devisId)
        {
            await Init();
            return await _database.Table<LigneDevis>().Where(l => l.DevisId == devisId).ToListAsync();
        }

        public async Task<int> SaveLigneDevis(LigneDevis ligne)
        {
            await Init();
            if (ligne.Id != 0)
                return await _database.UpdateAsync(ligne);
            else
                return await _database.InsertAsync(ligne);
        }

        public async Task<int> DeleteLigneDevis(int id)
        {
            await Init();
            return await _database.DeleteAsync<LigneDevis>(id);
        }
    }
}