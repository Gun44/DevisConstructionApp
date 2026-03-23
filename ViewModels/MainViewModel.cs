using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using DevisConstructionApp.Models;
using DevisConstructionApp.Services;

namespace DevisConstructionApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Projet> Projets { get; } = new();
        public ObservableCollection<Devis> Devis { get; } = new();

        public MainViewModel()
        {
            _databaseService = new DatabaseService();
        }

        [RelayCommand]
        public async Task LoadProjets()
        {
            IsLoading = true;
            try
            {
                var projets = await _databaseService.GetProjets();
                Projets.Clear();
                foreach (var projet in projets)
                {
                    Projets.Add(projet);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task LoadDevis()
        {
            IsLoading = true;
            try
            {
                var devis = await _databaseService.GetDevis();
                Devis.Clear();
                foreach (var d in devis)
                {
                    Devis.Add(d);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task DeleteProjet(Projet projet)
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                "Confirmation",
                $"Êtes-vous sûr de vouloir supprimer le projet '{projet.Nom}' ?",
                "Oui", "Non");

            if (result)
            {
                await _databaseService.DeleteProjet(projet.Id);
                await LoadProjets();
            }
        }
    }
}