using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library.View
{
    /// <summary>
    /// Interaction logic for WinEditB.xaml
    /// </summary>
    public partial class WinEditB : Window
    {
        public ObservableCollection<BookViewModel> Books { get; private set; }
        public ObservableCollection<AuthorViewModel> Author { get; private set; }

        public WinEditB(ObservableCollection<AuthorViewModel> author, ObservableCollection<BookViewModel> books)
        {
            InitializeComponent();
            Books = books;
            this.DataContext = new ViewModelEditB(author, books);
        } 
    }
}
