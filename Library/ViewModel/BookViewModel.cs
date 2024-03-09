using Library.Model;
using System;

namespace Library.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private Book _book;


        public BookViewModel(Book book)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
        }

        public int BookId
        {
            get => _book.BookId;
            set
            {
                if (_book.BookId != value)
                {
                    _book.BookId = value;
                    OnPropertyChanged(nameof(BookId));
                }
            }
        }

        public string Title
        {
            get => _book.Title;
            set
            {
                if (_book.Title != value)
                {
                    _book.Title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public int? AuthorId
        {
            get => _book.AuthorId;
            set
            {
                if (_book.AuthorId != value)
                {
                    _book.AuthorId = value;
                    OnPropertyChanged(nameof(AuthorId));
                }
            }
        }

        public int? PublicationYear
        {
            get => _book.PublicationYear;
            set
            {
                if (_book.PublicationYear != value)
                {
                    _book.PublicationYear = value;
                    OnPropertyChanged(nameof(PublicationYear));
                }
            }
        }

        public Author? Author
        {
            get => _book.Author;
            set
            {
                if (_book.Author != value)
                {
                    _book.Author = value;
                    OnPropertyChanged(nameof(Author));
                }
            }
        }


    }
}
