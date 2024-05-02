using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        FullName = value.FullName,
                        Id = value.Id,
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
            Persons = new RestCollection<Person>("http://localhost:20677/", "person");
            CreatePersonCommand = new RelayCommand(() =>
            {
                Persons.Add(new Person()
                {
                    FullName = SelectedPerson.FullName,
                });
            });

            UpdatePersonCommand = new RelayCommand(() =>
            {
                Persons.Update(SelectedPerson);
            });


            DeletePersonCommand = new RelayCommand(() =>
            {
                Persons.Delete(SelectedPerson.Id);
            },
            () =>
            {
                return SelectedPerson != null;
            });
        }
    }
}
