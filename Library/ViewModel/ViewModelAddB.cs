using Library.Commands;
using Library.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModel
{
    internal class ViewModelAddB : INotifyPropertyChanged
    {
        private ObservableCollection<AuthorViewModel> _authors;
        public ObservableCollection<AuthorViewModel> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged();
            }
        }

        private AuthorViewModel _selectedAuthor;
        public AuthorViewModel SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }

        private string _bookTitle;
        public string BookTitle
        {
            get => _bookTitle;
            set
            {
                _bookTitle = value;
                OnPropertyChanged(nameof(BookTitle));
            }
        }
        private string _publicationYear;
        public string PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChanged(nameof(PublicationYear));
            }
        }


        public ICommand AddBookCommand { get; private set; }

        public ViewModelAddB(ObservableCollection<AuthorViewModel> author)
        {
            Authors = author;

            AddBookCommand = new DelegateCommand(AddBook, CanAddBook);
        }


        private bool CanAddBook(object parameter)
        {
            return SelectedAuthor != null && !string.IsNullOrWhiteSpace(BookTitle);
        }

        private void AddBook(object parameter)
        {
            try
            {
                if (!int.TryParse(PublicationYear, out var year))
                {
                    MessageBox.Show("Год издания должен быть числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (var db = new LibraryContext())
                {
                    var book = new Book
                    {
                        Title = this.BookTitle,
                        AuthorId = this.SelectedAuthor.AuthorId,
                        PublicationYear = year 
                    };
                    db.Books.Add(book);
                    db.SaveChanges();
                    MessageBox.Show("Книга успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    BookTitle = string.Empty;
                    PublicationYear = string.Empty;
                    SelectedAuthor = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении книги: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
