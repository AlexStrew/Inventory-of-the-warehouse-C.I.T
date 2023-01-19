using Inventarisation.Models;
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

namespace Inventarisation.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        Core db = new Core();
        List<Inventory> invList;

        public MainPage()
        {
            InitializeComponent();
            invList = db.context.Inventory.ToList();
            
            InventoryDataGrid.ItemsSource = invList;
           


            //InventoryListView.ItemsSource = invList;

        }

        //private void InventoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
