using Inventarisation.Models;

using Inventarisation.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
            sfDataGrid.ItemsSource = invList;

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(InventoryDataGrid.ItemsSource);
            //view.Filter = UserFilter;
        }

        //private bool UserFilter(object item)
        //{
        //    //if (String.IsNullOrEmpty(SearchTBox.Text))
        //    //    return true;
        //    //else
        //    //    return ((item as Inventory).inv_num.IndexOf(SearchTBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //}

        private void AddButtonWindows_Click(object sender, RoutedEventArgs e)
        {
            AddWindow win = new AddWindow();
            if (win.ShowDialog() == true)
            {
                //TestText.Text = Properties.Settings.Default.MKBCode;
                //Console.WriteLine("--" + Properties.Settings.Default.MKBCode + "--");
                Console.WriteLine("sdsd");
            }
        }

        private void SearchTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //CollectionViewSource.GetDefaultView(InventoryDataGrid.ItemsSource).Refresh();

            

        }
    }
}
