using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevisConstructionApp.Models;
using DevisConstructionApp.Services;

namespace DevisConstructionApp.ViewModels
{
    public partial class ProjetViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private Projet _projet;

        [ObservableProperty]
        public string nom = "";

        [ObservableProperty]
        public string description = "";

        [ObservableProperty]
        public decimal longueurM = 0;

        [ObservableProperty]
        public decimal largeurM = 0;

        [ObservableProperty]
        public decimal hauteurM = 0;

        [ObservableProperty]
        public string imagePlanPath = "";

        [ObservableProperty]
        public decimal surfaceM2 = 0;

        public ProjetViewModel()
        {
            _databaseService = new DatabaseService();
        }

        public void InitialiserPourEdition(Projet projet)
        {
            _projet = projet;
            Nom = projet.Nom;
            Description = projet.Description;
            LongueurM = projet.LongueurM;
            LargeurM = projet.LargeurM;
            HauteurM = projet.HauteurM;
            ImagePlanPath = projet.ImagePlanPath;
            UpdateSurface();
        }

        public void InitialiserNouveauProjet()
        {
            _projet = null;
            Nom = "";
            Description = "";
            LongueurM = 0;
            LargeurM = 0;
            HauteurM = 0;
            ImagePlanPath = "";
            SurfaceM2 = 0;
        }

        partial void OnLongueurMChanged(decimal value) => UpdateSurface();
        partial void OnLargeurMChanged(decimal value) => UpdateSurface();

        private void UpdateSurface()
        {
            SurfaceM2 = LongueurM * LargeurM;
        }

        [RelayCommand]
        public async Task SelectPlan()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Sélectionner le plan du projet",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    var appDataDir = FileSystem.AppDataDirectory;
                    var fileName = Path.GetFileName(result.FullPath);
                    var destPath = Path.Combine(appDataDir, fileName);

                    if (File.Exists(result.FullPath))
                    {
                        File.Copy(result.FullPath, destPath, true);
                        ImagePlanPath = destPath;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", $"Erreur lors de la sélection du plan: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task SauvegarderProjet()
        {
            if (string.IsNullOrWhiteSpace(Nom))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Le nom du projet est obligatoire", "OK");
                return;
            }

            IsLoading = true;
            try
            {
                var projet = _projet ?? new Projet();
                projet.Nom = Nom;
                projet.Description = Description;
                projet.LongueurM = LongueurM;
                projet.LargeurM = LargeurM;
                projet.HauteurM = HauteurM;
                projet.ImagePlanPath = ImagePlanPath;

                await _databaseService.SaveProjet(projet);
                await Application.Current.MainPage.DisplayAlert("Succès", "Projet sauvegardé avec succès", "OK");
                await Shell.Current.GoToAsync("main");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}