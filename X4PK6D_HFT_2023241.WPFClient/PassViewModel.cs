using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using X4PK6D_HFT_2023241.Models;

namespace X4PK6D_HFT_2023241.WPFClient
{
    public class PassViewModel : ObservableRecipient
    {
        public RestCollection<Pass> Passes { get; set; }

        private Pass selectedPass;

        public Pass SelectedPass
        {
            get { return selectedPass; }
            set
            {
                if (value != null)
                {
                    selectedPass = new Pass()
                    {
                        Id = value.Id,
                        PassType = value.PassType,
                        StartDate = value.StartDate,
                        EndDate = value.EndDate,
                        Price = value.Price,
                        CrossfitGymUsage = value.CrossfitGymUsage,
                        GroupTrainingUsage = value.GroupTrainingUsage,
                        PoolUsage = value.PoolUsage,
                        SaunaUsage = value.SaunaUsage,
                        MassageUsage = value.MassageUsage
                    };
                }
                OnPropertyChanged();
                (DeletePassCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreatePassCommand { get; set; }

        public ICommand DeletePassCommand { get; set; }

        public ICommand UpdatePassCommand { get; set; }

        public PassViewModel()
        {
            Passes = new RestCollection<Pass>("http://localhost:20677/", "pass", "hub");
            CreatePassCommand = new RelayCommand(() =>
            {
                Passes.Add(new Pass()
                {
                    PassType = SelectedPass.PassType,
                    StartDate = SelectedPass.StartDate,
                    EndDate = SelectedPass.EndDate,
                    Price = SelectedPass.Price,
                    CrossfitGymUsage = SelectedPass.CrossfitGymUsage,
                    GroupTrainingUsage = SelectedPass.GroupTrainingUsage,
                    PoolUsage = SelectedPass.PoolUsage,
                    SaunaUsage = SelectedPass.SaunaUsage,
                    MassageUsage = SelectedPass.MassageUsage
                });
                ShowMessageBox("Pass created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });

            UpdatePassCommand = new RelayCommand(() =>
            {
                Passes.Update(SelectedPass);
                ShowMessageBox("Pass updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });


            DeletePassCommand = new RelayCommand(() =>
            {
                Passes.Delete(SelectedPass.Id);
                ClearSelectedPassFields();
                ShowMessageBox("Pass deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            },
            () =>
            {
                return SelectedPass != null;
            });

            SelectedPass = new Pass();
        }

        public void ClearSelectedPassFields()
        {
            SelectedPass = new Pass();
        }

        public void ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, button, icon);
        }
    }
}