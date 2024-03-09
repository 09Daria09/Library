using Library.Model;
using System.Linq;
using System.Windows;

namespace Library
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                using (var db = new LibraryContext())
                {
                    var authorsList = db.Authors.ToList(); 
                    var booksList = db.Books.ToList(); 

                    MainWindow view = new MainWindow();
                    MainViewModel viewModel = new MainViewModel(authorsList, booksList);
                    view.DataContext = viewModel;
                    view.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
