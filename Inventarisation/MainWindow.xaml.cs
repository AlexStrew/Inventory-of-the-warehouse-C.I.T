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
using Inventarisation.Models;
using HandyControl.Themes;
using Inventarisation.Views;
using Syncfusion.Windows.Shared;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows;
using Inventarisation.Properties;

namespace Inventarisation


{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VisualStyles currentTheme;
        public MainWindow()
        {
            InitializeComponent();
            




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
            NomenclatureWindow win = new NomenclatureWindow(currentTheme);
            
            if (win.ShowDialog() == true)
            {
                
                //TestText.Text = Properties.Settings.Default.MKBCode;
                //Console.WriteLine("--" + Properties.Settings.Default.MKBCode + "--");
                Console.WriteLine("sdsd");
            }
           
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

        private void QR_Click(object sender, RoutedEventArgs e)
        {
            QRWindow win = new QRWindow();
            
            if (win.ShowDialog() == true)
            {
                Console.WriteLine("sdsd");
            }
        }
        
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleButton.IsChecked.Value)
            {
                SfSkinManager.SetVisualStyle(this, VisualStyles.Windows11Dark);
                //Environment.SetEnvironmentVariable("CurrentTheme", "Windows11Dark");
                //SfSkinManager.SetVisualStyle(new OtherWindow(), VisualStyles.Windows11Dark);
                currentTheme = VisualStyles.Windows11Dark;

            }
            else
            {
                SfSkinManager.SetVisualStyle(this, VisualStyles.Windows11Light);
                //SfSkinManager.DefaultVisualStyle = VisualStyles.Windows11Light;
                //Environment.SetEnvironmentVariable("CurrentTheme", "Windows11Light");\
                currentTheme = VisualStyles.Windows11Light;

            }
        }

        
    }
}
