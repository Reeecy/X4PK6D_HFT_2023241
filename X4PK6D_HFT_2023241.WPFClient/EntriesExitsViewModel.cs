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
    public class EntriesExitsViewModel : ObservableRecipient
    {
        public RestCollection<EntriesExits> EntriesExits { get; set; }

        private EntriesExits selectedEntryExit;

        public EntriesExits SelectedEntryExit
        {
            get { return selectedEntryExit; }
            set
            {
                if (value != null)
                {
                    selectedEntryExit = new EntriesExits()
                    {
                        Id = value.Id,
                        EntryTime = value.EntryTime,
                        ExitTime = value.ExitTime,
                        PersonId = value.PersonId
                    };
                }
                OnPropertyChanged();
                (DeleteEntryExitCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateEntryExitCommand { get; set; }

        public ICommand DeleteEntryExitCommand { get; set; }

        public ICommand UpdateEntryExitCommand { get; set; }

        public EntriesExitsViewModel()
        {
            EntriesExits = new RestCollection<EntriesExits>("http://localhost:20677/", "entriesexits", "hub");
            CreateEntryExitCommand = new RelayCommand(() =>
            {
                EntriesExits.Add(new EntriesExits()
                {
                    EntryTime = SelectedEntryExit.EntryTime,
                    ExitTime = SelectedEntryExit.ExitTime,
                    PersonId = SelectedEntryExit.PersonId
                });
                ShowMessageBox("Entry/Exit created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });

            UpdateEntryExitCommand = new RelayCommand(() =>
            {
                EntriesExits.Update(SelectedEntryExit);
                ShowMessageBox("Entry/Exit updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });


            DeleteEntryExitCommand = new RelayCommand(() =>
            {
                EntriesExits.Delete(SelectedEntryExit.Id);
                ClearSelectedEntryExitFields();
                ShowMessageBox("Entry/Exit deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            },
            () =>
            {
                return SelectedEntryExit != null;
            });

            SelectedEntryExit = new EntriesExits();
        }

        public void ClearSelectedEntryExitFields()
        {
            SelectedEntryExit = new EntriesExits();
        }

        public void ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, button, icon);
        }
    }
}