using Inventarisation.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Text.RegularExpressions;
using Syncfusion.Windows.Shared;
using Inventarisation.Models;
using HandyControl.Themes;

namespace Inventarisation


{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserTextBlock.Text = Properties.Settings.Default.CurrentUser;




            MainFrame.Navigate(new AuthPage()
            { 
                //DataContext = new MainPageViewModel()
            });
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {

        }

        private void NomenclatureBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private  void WriteOffBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        //private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    this.DragMove();
        //}

        private void MinimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) { MainFrame.GoBack(); }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите закрыть программу?", "AHTUNG", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Тогда зачем нажимал?", "AHTUNG", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void MaximizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)   
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
            
        }

      
    }
}
