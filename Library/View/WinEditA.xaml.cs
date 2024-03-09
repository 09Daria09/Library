﻿using Library.ViewModel;
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
    /// Interaction logic for WinEditA.xaml
    /// </summary>
    public partial class WinEditA : Window
    {
        public ObservableCollection<AuthorViewModel> Author { get; private set; }

        public WinEditA(ObservableCollection<AuthorViewModel> author)
        {
            InitializeComponent();
            Author = author;
            this.DataContext = new ViewModelEditA(author); 
        }
    }
}
