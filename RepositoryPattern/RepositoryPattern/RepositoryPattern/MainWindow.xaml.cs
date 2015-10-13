using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RepositoryPattern.Annotations;
using RepositoryPattern.Models;
using RepositoryPattern.Repositories;

namespace RepositoryPattern
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml.
    /// 
    /// Elle implément INotifyPropertyChanged pour des raisons de data binding
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this; // La fenêtre est son propre ViewModel
        }

        private IPersonRepository _repository;
        private ObservableCollection<Person> _persons;
        private bool _canDelete;

        /// <summary>
        /// Remarque: Le code de la fenêtre utulise le repository par son interface (IPersonRepository) et pas par son implémentation (MemoryPersonRepository)
        /// </summary>
        private IPersonRepository PersonRepository
        {
            get
            {
                if(_repository == null)
                    _repository = new MemoryPersonRepository();

                return _repository;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set
            {
                if (Equals(value, _persons)) return;
                _persons = value;
                OnPropertyChanged();
            }
        }

        public bool CanDelete
        {
            get { return _canDelete; }
            set
            {
                if (value == _canDelete) return;
                _canDelete = value;
                OnPropertyChanged();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // On charge les données par le biais du repository
                var personnes = PersonRepository.Persons;
                Persons = new ObservableCollection<Person>(personnes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private List<Person> _selectedPersons; 
        /// <summary>
        /// Methode appelée après une selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On sauvegarde les personnes à supprimer
            _selectedPersons = e.AddedItems.OfType<Person>().ToList();
            if (_selectedPersons.Any())
            {
                CanDelete = true;
            }
            else
            {
                CanDelete = false;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Maintenant on supprimer :" + _selectedPersons.Count + " personnes");
        }
    }
}
