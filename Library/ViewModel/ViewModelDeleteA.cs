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
using System.Windows.Input;
using System.Windows;

namespace Library.ViewModel
{
    internal class ViewModelDeleteA : ViewModelBase
    {
        public ObservableCollection<AuthorViewModel> Collection { get; private set; }

        private AuthorViewModel _selected;

        public AuthorViewModel Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public ICommand DelateCommand { get; private set; }

        public ViewModelDeleteA(ObservableCollection<AuthorViewModel> authors)
        {
            Collection = authors;
            DelateCommand = new DelegateCommand(DeleteAction, CanDelete);
        }

        private bool CanDelete(object parameter)
        {
            return Selected != null;
        }

        private void DeleteAction(object parameter)
        {
            var result = MessageBox.Show($"Вы действительно желаете удалить автора {Selected.AuthorName} ?",
                                "Удаление автора", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
                return;

            try
            {
                using (var db = new LibraryContext())
                {
                    var authorToDelete = db.Authors.Find(Selected.AuthorId);
                    if (authorToDelete != null)
                    {
                        var booksToDelete = db.Books.Where(book => book.AuthorId == Selected.AuthorId).ToList();
                        foreach (var book in booksToDelete)
                        {
                            db.Books.Remove(book);
                        }

                        db.Authors.Remove(authorToDelete);
                        db.SaveChanges(); 
                        Collection.Remove(Selected);
                        MessageBox.Show("Автор и все связанные книги удалены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Selected = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении автора и связанных книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
