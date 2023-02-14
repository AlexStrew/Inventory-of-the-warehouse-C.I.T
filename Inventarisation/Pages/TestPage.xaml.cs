using Inventarisation.Api.ApiModel;
using Inventarisation.Api.Controllers;
using Inventarisation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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

namespace Inventarisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        //List<Inventory> invList = new List<Inventory>();
        public TestPage()
        {
            
            InitializeComponent();

            //invList = ApiInventory.GetInventory();
            //.ItemsSource = invList;

            //IEnumerable<Inventory> inv = JsonConvert.DeserializeObject<IEnumerable<Inventory>>(resultAsJson);
            //InventoryListView.ItemsSource = inv;


        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("DarkTheme.xaml", UriKind.Relative)
            });
            MessageBox.Show("Тёмная тема");
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("LightTheme.xaml", UriKind.Relative)
            });
            MessageBox.Show("Светлая тема");
        }

    }
}
