using System.Collections.ObjectModel;
using System.Windows.Input;
using Library.Model;
using Library.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Windows; 

using static System.Reflection.Metadata.BlobBuilder;
using Library.Commands;
using Library.View;

internal class MainViewModel : ViewModelBase
{
    public ObservableCollection<AuthorViewModel> Authors { get; private set; }
    public ObservableCollection<BookViewModel> Books { get; private set; }
    private List<BookViewModel> _allBooks = new List<BookViewModel>();

    private AuthorViewModel _selectedAuthor;
    public AuthorViewModel SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            if (_selectedAuthor != value)
            {
                _selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
                FilterBooks();
            }
        }
    }

    private BookViewModel _selectedBook;
    public BookViewModel SelectedBook
    {
        get => _selectedBook;
        set
        {
            if (_selectedBook != value)
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }
    }

    private bool _isFilteringEnabled;
    public bool IsFilteringEnabled
    {
        get => _isFilteringEnabled;
        set
        {
            if (_isFilteringEnabled != value)
            {
                _isFilteringEnabled = value;
                OnPropertyChanged(nameof(IsFilteringEnabled));
                FilterBooks();
            }
        }
    }

    private void FilterBooks()
    {
        IEnumerable<BookViewModel> filteredBooks = _allBooks;

        if (IsFilteringEnabled && _selectedAuthor != null)
        {
            filteredBooks = _allBooks.Where(b => b.AuthorId == _selectedAuthor.AuthorId);
        }

        Books = new ObservableCollection<BookViewModel>(filteredBooks);
        OnPropertyChanged(nameof(Books)); 
    }



    public ICommand AddAuthorCommand { get; private set; }
    public ICommand EditAuthorCommand { get; private set; }
    public ICommand DeleteAuthorCommand { get; private set; }

    public ICommand AddBookCommand { get; private set; }
    public ICommand EditBookCommand { get; private set; }
    public ICommand DeleteBookCommand { get; private set; }

    public MainViewModel(List<Author> authors, List<Book> books)
    {
        Authors = new ObservableCollection<AuthorViewModel>(authors.Select(a => new AuthorViewModel(a)));
        Books = new ObservableCollection<BookViewModel>(books.Select(b => new BookViewModel(b)));
        DeleteBookCommand = new DelegateCommand(DeleteBook, (object parameter) => true);
        DeleteAuthorCommand = new DelegateCommand(DeleteAuthor, (object parameter) => true);
        AddAuthorCommand = new DelegateCommand(AddAuthor, (object parameter) => true);
        AddBookCommand = new DelegateCommand(AddBook, (object parameter) => true);
        EditAuthorCommand = new DelegateCommand(EditAuthor, (object parameter) => true);
        EditBookCommand = new DelegateCommand(EditBook, (object parameter) => true);
        LoadDataAsync(); 
    }

    private void EditBook(object obj)
    {
        var EditWindow = new WinEditB(Authors, Books);
        EditWindow.ShowDialog();
        LoadDataAsync();
    }

    private void EditAuthor(object obj)
    {
        var EditWindow = new WinEditA(Authors);
        EditWindow.ShowDialog();
        LoadDataAsync();
    }

    private void AddBook(object obj)
    {
        var AddWindow = new WinAddB(Authors);
        AddWindow.ShowDialog();
        LoadDataAsync();
    }

    private void AddAuthor(object obj)
    {
        var AddWindow = new WinAddA();
        AddWindow.ShowDialog();
        LoadDataAsync();
    }

    private void DeleteAuthor(object obj)
    {
        var DeletionWindow = new WinDeleteA(Authors);
        DeletionWindow.ShowDialog();
    }

    private void DeleteBook(object obj)
    {
        var DeletionWindow = new WinDelete(Books); 
        DeletionWindow.ShowDialog();
    }

    private async Task LoadDataAsync()
    {
        using (var context = new LibraryContext())
        {
            var authorsList = await context.Authors.ToListAsync();
            var booksList = await context.Books.ToListAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Authors.Clear();
                Books.Clear();
                _allBooks.Clear(); 

                foreach (var author in authorsList)
                {
                    Authors.Add(new AuthorViewModel(author));
                }
                foreach (var book in booksList)
                {
                    var bookViewModel = new BookViewModel(book);
                    _allBooks.Add(bookViewModel); 
                    Books.Add(bookViewModel); 
                }
            });
        }
    }


}
