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
    public class PersonViewModel : ObservableRecipient
    {
        private readonly RestService rest;

        // Commands for the non-crud methods
        public ICommand GetPersonCountCommand { get; }
        public ICommand GetPersonsWithoutPassesCommand { get; }
        public ICommand ShowPWithMonthlyPsAndTUDCommand { get; }
        public ICommand ShowStudentsWithActivePassesCommand { get; }
        public ICommand ShowPersonsWithExpiredPassesCommand { get; }
        public ICommand ShowPersonsWithEntriesExitsCommand { get; }

        // define the Persons, and setup the SelectedPerson
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

        public PersonViewModel()
        {
            rest = new RestService("http://localhost:20677/", "person");

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

            GetPersonCountCommand = new RelayCommand(async () =>
            {
                GetPersonCountAsync();
            });

            GetPersonsWithoutPassesCommand = new RelayCommand(async () =>
            {
                GetPersonsWithoutPassesAsync();
            });

            ShowPWithMonthlyPsAndTUDCommand = new RelayCommand(async () =>
            {
                ShowPWithMonthlyPsAndTUDAsync();
            });

            ShowStudentsWithActivePassesCommand = new RelayCommand(async () =>
            {
                ShowStudentsWithActivePassesAsync();
            });

            ShowPersonsWithExpiredPassesCommand = new RelayCommand(async () =>
            {
                ShowPersonsWithExpiredPassesAsync();
            });

            ShowPersonsWithEntriesExitsCommand = new RelayCommand(async () =>
            {
                ShowPersonsWithEntriesExitsAsync();
            });
        }


        public void ClearSelectedPersonFields()
        {
            SelectedPerson = new Person();
        }

        public void ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBox.Show(message, caption, button, icon);
        }

        private async void GetPersonCountAsync()
        {
            try
            {
                int personCount = await rest.GetSingleAsync<int>("/api/Stat/PersonCount");

                ShowMessageBox($"The count of the persons: {personCount}.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (ArgumentException ex)
            {
                // Handle exception appropriately

            }
        }

        private async void GetPersonsWithoutPassesAsync()
        {
            var persons = await rest.GetSingleAsync<List<FullNameClass>>("/api/Stat/PersonsWithoutPasses");

            string personsList = "\n\n";
            foreach (var person in persons)
            {
                personsList += person.FullName + '\n';
            }
            string message = $"The Persons without passes: {personsList}";
            ShowMessageBox(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void ShowPWithMonthlyPsAndTUDAsync()
        {
            var persons = await rest.GetSingleAsync<List<PWithMonthlyPsAndTUD>>("/api/Stat/PersonsWithMonthlyPassesAndTotalUsage");

            string personList = "\n\n";
            foreach (var person in persons)
            {
                personList += person.FullName + ": " + person.TotalUsageDuration + " times" + "\n";
            }

            string message = $"Persons who have Monthly pass and their total usage: {personList}";
            ShowMessageBox(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void ShowStudentsWithActivePassesAsync()
        {
            var students = await rest.GetSingleAsync<List<ActiveStudents>>("/api/Stat/StudentsWithActivePasses");

            string studentsList = "\n";
            foreach(var student in students)
            {
                studentsList += $"\nName: {student.FullName}\n\t- Pass Type: {student.PassType}\n\t- Expire date: {student.EndDate}\n";
            }

            string message = $"Students who have active passes: {studentsList}";
            ShowMessageBox(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void ShowPersonsWithExpiredPassesAsync()
        {
            var persons = await rest.GetSingleAsync<List<ExpiredPasses>>("/api/Stat/PersonsWithExpiredPasses");

            string personList = "\n";
            foreach (var person in persons)
            {
                personList += $"\nName: {person.FullName}\n\t- Pass Type: {person.PassType}\n\t- Expire date: {person.EndDate}";
            }

            string message = $"Persons who have expired passes: {personList}";
            ShowMessageBox(message, "Info", MessageBoxButton.OK , MessageBoxImage.Information);
        }

        private async void ShowPersonsWithEntriesExitsAsync()
        {
            var persons = await rest.GetSingleAsync<List<AllEntriesAndExits>>("/api/Stat/PersonsWithEntriesAndExits");

            string message = "Persons with their Entries and Exits:\n";
            foreach (var person in persons)
            {
                message += person.FullName + ":\n";

                for (int i = 0; i < person.Entries.Count; i++)
                {
                    message += "\tEntry: " + person.Entries[i] + " - Exit: " + person.Exits[i] + '\n';
                }
            }
            ShowMessageBox(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
