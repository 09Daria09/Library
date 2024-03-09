using Library.Commands;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModel
{
    internal class ViewModelEditA : INotifyPropertyChanged
    {
        public ObservableCollection<AuthorViewModel> Authors { get; set; }

        private AuthorViewModel _selectedAuthor;
        public AuthorViewModel SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged();
            }
        }

        private string _newAuthorName;
        public string NewAuthorName
        {
            get => _newAuthorName;
            set
            {
                _newAuthorName = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ViewModelEditA(ObservableCollection<AuthorViewModel> author) 
        {
            Authors = author;
            SaveChangesCommand = new DelegateCommand(SaveChanges, (object parameter) => true);
        }

        private void SaveChanges(object obj)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    var authorToUpdate = db.Authors.Find(SelectedAuthor.AuthorId);
                    if (authorToUpdate != null)
                    {
                        authorToUpdate.AuthorName = NewAuthorName;
                        db.SaveChanges();
                        MessageBox.Show("Информация об авторе обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                     
                        SelectedAuthor.AuthorName = NewAuthorName;
                        NewAuthorName = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении информации об авторе: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
