using Library.Commands;
using Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModel
{
    internal class ViewModelAddA: ViewModelBase
    {
        private string _authorName;
        public string AuthorName
        {
            get => _authorName;
            set
            {
                _authorName = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }

        public ICommand AddCommand { get; private set; }

        public ViewModelAddA() 
        {
            AddCommand = new DelegateCommand(AddAction, CanAdd);
        } 

        private bool CanAdd(object parameter)
        {
            return !string.IsNullOrEmpty(AuthorName);
        }

        private void AddAction(object parameter)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    var newAuthor = new Author { AuthorName = this.AuthorName };
                    db.Authors.Add(newAuthor);
                    db.SaveChanges();
                    MessageBox.Show($"Автор {AuthorName} добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.AuthorName = string.Empty;
                    Application.Current.Dispatcher.Invoke(() => OnPropertyChanged(nameof(AuthorName)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении автора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                    
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
