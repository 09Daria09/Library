using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Library.Commands;
using Library.Model;

namespace Library.ViewModel
{
    internal class ViewModelEditB : INotifyPropertyChanged
    {
        private ObservableCollection<BookViewModel> _books;
        public ObservableCollection<BookViewModel> Books
        {
            get => _books;
            set { _books = value; OnPropertyChanged(); }
        }

        private ObservableCollection<AuthorViewModel> _authors;
        public ObservableCollection<AuthorViewModel> Authors
        {
            get => _authors;
            set { _authors = value; OnPropertyChanged(); }
        }

        private BookViewModel _selectedBook;
        public BookViewModel SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                if (value != null)
                {
                    BookTitle = value.Title;
                    PublicationYear = value.PublicationYear.ToString();
                    SelectedAuthor = Authors.FirstOrDefault(a => a.AuthorId == value.AuthorId);
                }
            }
        }

        private AuthorViewModel _selectedAuthor;
        public AuthorViewModel SelectedAuthor
        {
            get => _selectedAuthor;
            set { _selectedAuthor = value; OnPropertyChanged(); }
        }

        private string _bookTitle;
        public string BookTitle
        {
            get => _bookTitle;
            set { _bookTitle = value; OnPropertyChanged(); }
        }

        private string _publicationYear;
        public string PublicationYear
        {
            get => _publicationYear;
            set { _publicationYear = value; OnPropertyChanged(); }
        }

        public ICommand SaveChangesCommand { get; private set; }

        public ViewModelEditB(ObservableCollection<AuthorViewModel> author, ObservableCollection<BookViewModel> books)
        {
            Books = books;
            Authors = author;
            SaveChangesCommand = new DelegateCommand(SaveChanges, (object param)=>true);
        }


        private void SaveChanges(object obj)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    var bookToUpdate = db.Books.Find(SelectedBook.BookId);
                    if (bookToUpdate == null)
                    {
                        MessageBox.Show("Выбранная книга не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    bookToUpdate.Title = BookTitle;
                    bookToUpdate.AuthorId = SelectedAuthor.AuthorId; 
                    bookToUpdate.PublicationYear = int.TryParse(PublicationYear, out var year) ? year : (int?)null;

                    db.SaveChanges();

                    SelectedBook.Title = BookTitle;
                    SelectedBook.AuthorId = SelectedAuthor.AuthorId;
                    SelectedBook.PublicationYear = year;

                    MessageBox.Show($"Информация о книге '{BookTitle}' обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении информации о книге: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
