using Library.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace Library.ViewModel
{
    public class AuthorViewModel : ViewModelBase
    {
        private Author _author;

        public AuthorViewModel(Author author)
        {
            _author = author ?? throw new ArgumentNullException(nameof(author));
            Books = new ObservableCollection<BookViewModel>(author.Books.Select(b => new BookViewModel(b)));
        }

        public int AuthorId
        {
            get => _author.AuthorId;
            set
            {
                if (_author.AuthorId != value)
                {
                    _author.AuthorId = value;
                    OnPropertyChanged(nameof(AuthorId));
                }
            }
        }

        public string AuthorName
        {
            get => _author.AuthorName;
            set
            {
                if (_author.AuthorName != value)
                {
                    _author.AuthorName = value;
                    OnPropertyChanged(nameof(AuthorName));
                }
            }
        }

        public ObservableCollection<BookViewModel> Books { get; private set; }

        
    }
}
