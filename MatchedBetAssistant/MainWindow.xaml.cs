using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MatchedBetAssistant.ViewModel;
namespace MatchedBetAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel main;

        public MainWindow()
        {
            InitializeComponent();

            main = new MainWindowViewModel();

            this.DataContext = main;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.main.Dispose();
        }
    }
}
