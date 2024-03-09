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
    internal class ViewModelDelete: ViewModelBase
    {
        public ObservableCollection<BookViewModel> Collection { get; private set; }

        private BookViewModel _selected;

        public BookViewModel Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public ICommand DelateCommand { get; private set; } 

        public ViewModelDelete(ObservableCollection<BookViewModel> books) 
        {
            Collection = books;
            DelateCommand = new DelegateCommand(DeleteAction, CanDelete);
        }

        private bool CanDelete(object parameter)
        {
            return Selected != null;
        }

        private void DeleteAction(object parameter)
        {
            var result = MessageBox.Show($"Вы действительно желаете удалить книгу {Selected.Title} ?",
                                "Удаление книги", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
                return;

            try
            {
                using (var db = new LibraryContext())
                {
                    var bookToDelete = db.Books.Find(Selected.BookId);
                    if (bookToDelete != null)
                    {
                        db.Books.Remove(bookToDelete);
                        db.SaveChanges();
                        Collection.Remove(Selected);
                        MessageBox.Show("Книга удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Selected = null; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
