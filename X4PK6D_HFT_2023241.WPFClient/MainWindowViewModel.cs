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
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Person> Persons { get; set; }

        private Person selectedPerson;

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (value != null)
                {
                    selectedPerson = new Person()
                    {
                        Id = value.Id,
                        FullName = value.FullName,
                        DateOfBirth = value.DateOfBirth,
                        Address = value.Address,
                        Email = value.Email,
                        PhoneNumber = value.PhoneNumber,
                        IsStudent = value.IsStudent,
                        IsRetired = value.IsRetired,
                    };
                }
                OnPropertyChanged();
                (DeletePersonCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreatePersonCommand { get; set; }

        public ICommand DeletePersonCommand { get; set; }

        public ICommand UpdatePersonCommand { get; set; }

        public MainWindowViewModel()
        {
            Persons = new RestCollection<Person>("http://localhost:20677/", "person", "hub");
            CreatePersonCommand = new RelayCommand(() =>
            {
                Persons.Add(new Person()
                {
                    FullName = SelectedPerson.FullName,
                    DateOfBirth = SelectedPerson.DateOfBirth,
                    Address = SelectedPerson.Address,
                    Email = SelectedPerson.Email,
                    PhoneNumber = SelectedPerson.PhoneNumber,
                    IsRetired = SelectedPerson.IsRetired,
                    IsStudent = SelectedPerson.IsStudent,
                });
                ShowMessageBox("Person created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });

            UpdatePersonCommand = new RelayCommand(() =>
            {
                Persons.Update(SelectedPerson);
                ShowMessageBox("Person updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            });


            DeletePersonCommand = new RelayCommand(() =>
            {
                Persons.Delete(SelectedPerson.Id);
                ClearSelectedPersonFields();
                ShowMessageBox("Person deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            },
            () =>
            {
                return SelectedPerson != null;
            });

            SelectedPerson = new Person();
        }

        public void ClearSelectedPersonFields()
        {
            SelectedPerson = new Person();
        }

        public void ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, button, icon);
        }
    }
}
